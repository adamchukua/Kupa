using Kupa.Api.Dtos;
using Kupa.Api.Models;

namespace Kupa.Api.Services.Interfaces
{
    public interface IEventService
    {
        Task<Event> GetEventByIdAsync(int id);

        Task<IEnumerable<Event>> GetAllEventsAsync();

        Task CreateEventAsync(Event eventObject);

        Task UpdateEventAsync(int id, UpdateEventDto eventDto);

        Task DeleteEventAsync(int id);
    }
}
