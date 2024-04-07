using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int id);

        Task<IEnumerable<Event>> GetAllAsync();

        Task AddAsync(Event eventObject);

        Task UpdateAsync(Event eventObject);

        Task DeleteAsync(Event eventObject);

        Task ExistsAsync(int id);
    }
}
