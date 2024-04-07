using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public EventService(
            IEventRepository eventRepository,
            ILocationRepository locationRepository,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task CreateEventAsync(Event eventObject)
        {
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

            if (foundLocation == null)
            {
                await _locationRepository.AddAsync(eventObject.Location);
            }
            else
            {
                eventObject.LocationId = foundLocation.Id;
                eventObject.Location = null;
            }

            await _eventRepository.AddAsync(eventObject);
        }

        public async Task DeleteEventAsync(int id)
        {
            Event eventObject = await _eventRepository.GetByIdAsync(id);

            if (eventObject == null)
            {
                throw new KeyNotFoundException($"Event with id {id} not found.");
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
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllAsync();
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

            Location location = await _locationRepository.GetByIdAsync(eventObject.LocationId);

            if (location == null)
            {
                throw new KeyNotFoundException($"Location with id {id} not found.");
            }

            _mapper.Map(updateEventDto, eventObject);

            int eventsCountWithLocationId = await _eventRepository.CountEventsByLocationIdAsync(location.Id);

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

            eventObject.Location = null;

            await _eventRepository.UpdateAsync(eventObject);
        }

        private bool EqualLocations(UpdateEventDto updateEventDto, Location location)
        {
            return updateEventDto.LocationTypeId == location.TypeId &&
                (updateEventDto.Location == location.Address || updateEventDto.Location == location.Url);
        }
    }
}
