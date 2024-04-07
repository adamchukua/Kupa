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
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task CreateEventAsync(Event eventObject)
        {
            await _eventRepository.AddAsync(eventObject);
        }

        public async Task DeleteEventAsync(int id)
        {
            Event? eventObject = await _eventRepository.GetByIdAsync(id);
            if (eventObject != null)
            {
                await _eventRepository.DeleteAsync(eventObject);
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

        public async Task UpdateEventAsync(int id, EventDto eventDto)
        {
            Event eventObject = await _eventRepository.GetByIdAsync(id);

            if (eventObject == null)
            {
                throw new KeyNotFoundException($"Event with id {id} not found.");
            }

            _mapper.Map(eventDto, eventObject);

            await _eventRepository.UpdateAsync(eventObject);
        }
    }
}
