using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int id);

        Task<IEnumerable<Event>> GetAsync(int userId, bool createdByUser, bool participatedByUser);

        Task<IEnumerable<Event>> SearchAsync(string keyword, int[] categories, int[] cities);

        Task AddAsync(Event eventObject);

        Task UpdateAsync(Event eventObject);

        Task DeleteAsync(Event eventObject);

        Task<bool> ExistsByIdAsync(int id);

        Task<bool> ExistsByLocationIdAsync(int locationId);

        Task<int> CountEventsByLocationIdAsync(int locationId);
    }
}
