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
        private readonly ApplicationDbContext _context;
        private readonly IEventRepository _eventRepository;
        private readonly IEventSurveyQuestionRepository _surveyQuestionRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public EventService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IEventRepository eventRepository,
            IEventSurveyQuestionRepository surveyQuestionRepository,
            ILocationRepository locationRepository,
            IMapper mapper) : base(httpContextAccessor) 
        {
            _context = context;
            _eventRepository = eventRepository;
            _surveyQuestionRepository = surveyQuestionRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task CreateEventAsync(Event eventObject)
        {
            if (UserId == null)
            {
                throw new UnauthorizedAccessException("Authorize for this action.");
            }

            Location foundLocation = null;

            switch (eventObject.Location.TypeId)
            {
                case Enums.LocationTypeId.Offline:
                    foundLocation = await _locationRepository.FindByAddressAsync(eventObject.Location.Address);
                    break;
                case Enums.LocationTypeId.Online:
                    foundLocation = await _locationRepository.FindByUrlAsync(eventObject.Location.Url);
                    break;
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (foundLocation == null)
                    {
                        await _locationRepository.AddAsync(eventObject.Location);
                    }
                    else
                    {
                        eventObject.LocationId = foundLocation.Id;
                        eventObject.Location = null;
                    }

                    eventObject.CreatedByUserId = (int)UserId;
                    await _eventRepository.AddAsync(eventObject);
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
            Event eventObject = await _eventRepository.GetByIdAsync(id);

            if (eventObject == null)
            {
                throw new KeyNotFoundException($"Event with id {id} not found.");
            }

            if (eventObject.CreatedByUserId != UserId && UserRole != "Admin")
            {
                throw new UnauthorizedAccessException("You don't have access to delete this event.");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (eventObject.EventSurveyQuestions != null)
                    {
                        _context.RemoveRange(eventObject.EventSurveyQuestions);
                    }

                    if (eventObject.EventComments != null)
                    {
                        _context.RemoveRange(eventObject.EventComments);
                    }

                    await _eventRepository.DeleteAsync(eventObject);

                    Location location = await _locationRepository.GetByIdAsync(eventObject.LocationId);
            
                    if (location == null)
                    {
                        throw new KeyNotFoundException($"Location with id {id} not found.");
                    }

                    bool eventsWithLocationExists = await _eventRepository.ExistsByLocationIdAsync(location.Id);

                    if (!eventsWithLocationExists)
                    {
                        await _locationRepository.DeleteAsync(location);
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

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Event>> SearchEventsAsync(string? keyword, int[] categories, int[] cities)
        {
            return await _eventRepository.SearchAsync(keyword, categories, cities);
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task UpdateEventAsync(int id, UpdateEventDto updateEventDto)
        {
            Event eventObject = await _eventRepository.GetByIdAsync(id);

            if (eventObject == null)
            {
                throw new KeyNotFoundException($"Event with id {id} not found.");
            }

            if (eventObject.CreatedByUserId != UserId)
            {
                throw new UnauthorizedAccessException("You don't have access to update this event.");
            }

            Location location = await _locationRepository.GetByIdAsync(eventObject.LocationId);

            if (location == null)
            {
                throw new KeyNotFoundException($"Location with id {id} not found.");
            }

            _mapper.Map(updateEventDto, eventObject);

            int eventsCountWithLocationId = await _eventRepository.CountEventsByLocationIdAsync(location.Id);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (!EqualLocations(updateEventDto, location))
                    {
                        _mapper.Map(updateEventDto, location);

                        if (eventsCountWithLocationId > 1)
                        {
                            await _locationRepository.AddAsync(Location.CreateNewLocation(location));
                            eventObject.LocationId = (await _locationRepository.FindByAddressOrUrlAsync(updateEventDto.Location)).Id;
                        }
                        else
                        {
                            await _locationRepository.UpdateAsync(location);
                        }
                    }

                    IEnumerable<EventSurveyQuestion> eventSurveyQuestions = await _surveyQuestionRepository.GetSurveyQuestionsByEventId(id);
                    if (eventSurveyQuestions != null)
                    {
                        await _surveyQuestionRepository.DeleteRange(eventSurveyQuestions);
                    }
                    
                    eventObject.Location = null;
                    await _eventRepository.UpdateAsync(eventObject);
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
