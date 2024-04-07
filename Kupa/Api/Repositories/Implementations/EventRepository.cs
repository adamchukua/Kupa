using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await GetAllItemsAsync();
        }

        public async Task AddAsync(Event eventObject)
        {
            eventObject.CreatedAt = DateTime.Now;
            eventObject.LastUpdatedAt = DateTime.Now;
            await AddItemAsync(eventObject);
        }

        public async Task UpdateAsync(Event eventObject)
        {
            eventObject.LastUpdatedAt = DateTime.Now;
            await UpdateItemAsync(eventObject);
        }

        public async Task DeleteAsync(Event eventObject)
        {
            await DeleteItemAsync(eventObject);
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await ExistsAsync(e => e.Id == id);
        }

        public async Task<bool> ExistsByLocationIdAsync(int locationId)
        {
            return await ExistsAsync(e => e.LocationId == locationId);
        }

        public async Task<int> CountEventsByLocationIdAsync(int locationId)
        {
            return await Where(e =>e.LocationId == locationId)
                .CountAsync();
        }
    }
}
