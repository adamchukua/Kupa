using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Location location)
        {
            await AddItemAsync(location);
        }

        public async Task DeleteAsync(Location location)
        {
            await DeleteItemAsync(location);
        }

        public async Task<Location> FindByAddressAsync(string address)
        {
            return await Where(l => l.Address == address)
                .FirstOrDefaultAsync();
        }

        public async Task<Location> FindByAddressOrUrlAsync(string location)
        {
            return await Where(l => l.Address == location || l.Url == location)
                .FirstOrDefaultAsync();
        }

        public async Task<Location> FindByUrlAsync(string url)
        {
            return await Where(l => l.Url == url)
                .FirstOrDefaultAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await Where(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            await UpdateItemAsync(location);
        }
    }
}
