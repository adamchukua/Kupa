using AutoMapper;
using Kupa.Api.Data;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class EventService : CurrentUserService, IEventService
    {
        private readonly ApplicationDbContext context;
        private readonly IEventRepository eventRepository;
        private readonly IEventSurveyQuestionRepository surveyQuestionRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public EventService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IEventRepository eventRepository,
            IEventSurveyQuestionRepository surveyQuestionRepository,
            ILocationRepository locationRepository,
            IValidator validator,
            IMapper mapper) : base(httpContextAccessor) 
        {
            this.context = context;
            this.eventRepository = eventRepository;
            this.surveyQuestionRepository = surveyQuestionRepository;
            this.locationRepository = locationRepository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task CreateEventAsync(Event eventObject)
        {
            validator.AuthorizedUser(UserId);

            Location foundLocation = null;

            switch (eventObject.Location.TypeId)
            {
                case Enums.LocationTypeId.Offline:
                    foundLocation = await locationRepository.FindByAddressAsync(eventObject.Location.Address);
                    break;
                case Enums.LocationTypeId.Online:
                    foundLocation = await locationRepository.FindByUrlAsync(eventObject.Location.Url);
                    break;
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (foundLocation == null)
                    {
                        await locationRepository.AddAsync(eventObject.Location);
                    }
                    else
                    {
                        eventObject.LocationId = foundLocation.Id;
                        eventObject.Location = null;
                    }

                    eventObject.CreatedByUserId = (int)UserId;
                    await eventRepository.AddAsync(eventObject);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            Event eventObject = await eventRepository.GetByIdAsync(id);

            validator.ObjectNull(eventObject, string.Empty, $"Event with id {id} not found");
            validator.AuthorizedOrAdminAction(eventObject.CreatedByUserId, (int)UserId, UserRole, "you can't delete this event");

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (eventObject.EventSurveyQuestions != null)
                    {
                        context.RemoveRange(eventObject.EventSurveyQuestions);
                    }

                    if (eventObject.EventComments != null)
                    {
                        context.RemoveRange(eventObject.EventComments);
                    }

                    await eventRepository.DeleteAsync(eventObject);

                    Location location = await locationRepository.GetByIdAsync(eventObject.LocationId);
            
                    if (location == null)
                    {
                        throw new KeyNotFoundException($"Location with id {id} not found.");
                    }

                    bool eventsWithLocationExists = await eventRepository.ExistsByLocationIdAsync(location.Id);

                    if (!eventsWithLocationExists)
                    {
                        await locationRepository.DeleteAsync(location);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(bool createdByUser, bool participatedByUser)
        {
            if (UserId == null)
            {
                if (createdByUser || participatedByUser)
                {
                    throw new UnauthorizedAccessException("To get events you need authorize first");
                }

                return await eventRepository.GetAsync(0, false, false);
            } else
            {
                return await eventRepository.GetAsync((int)UserId, createdByUser, participatedByUser);
            }
        }

        public async Task<IEnumerable<Event>> SearchEventsAsync(string? keyword, int[] categories, int[] cities)
        {
            return await eventRepository.SearchAsync(keyword, categories, cities);
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await eventRepository.GetByIdAsync(id);
        }

        public async Task UpdateEventAsync(int id, UpdateEventDto updateEventDto)
        {
            Event eventObject = await eventRepository.GetByIdAsync(id);

            validator.ObjectNull(eventObject, string.Empty, $"Event with id {id} not found.");
            validator.AuthorizedAction(eventObject.CreatedByUserId, (int)UserId, "you can't update this event");

            Location location = await locationRepository.GetByIdAsync(eventObject.LocationId);

            validator.ObjectNull(location, string.Empty, $"Location with id {id} not found");

            mapper.Map(updateEventDto, eventObject);

            int eventsCountWithLocationId = await eventRepository.CountEventsByLocationIdAsync(location.Id);

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!EqualLocations(updateEventDto, location))
                    {
                        mapper.Map(updateEventDto, location);

                        if (eventsCountWithLocationId > 1)
                        {
                            await locationRepository.AddAsync(Location.CreateNewLocation(location));
                            eventObject.LocationId = (await locationRepository.FindByAddressOrUrlAsync(updateEventDto.Location)).Id;
                        }
                        else
                        {
                            await locationRepository.UpdateAsync(location);
                        }
                    }

                    IEnumerable<EventSurveyQuestion> eventSurveyQuestions = await surveyQuestionRepository.GetSurveyQuestionsByEventId(id);
                    if (eventSurveyQuestions != null)
                    {
                        await surveyQuestionRepository.DeleteRange(eventSurveyQuestions);
                    }
                    
                    eventObject.Location = null;
                    await eventRepository.UpdateAsync(eventObject);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private bool EqualLocations(UpdateEventDto updateEventDto, Location location)
        {
            return updateEventDto.LocationTypeId == location.TypeId &&
                (updateEventDto.Location == location.Address || updateEventDto.Location == location.Url);
        }
    }
}
