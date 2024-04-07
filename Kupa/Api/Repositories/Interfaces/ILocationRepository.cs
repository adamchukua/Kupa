using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<Location> GetByIdAsync(int id);

        Task AddAsync(Location location);

        Task UpdateAsync(Location location);

        Task DeleteAsync(Location location);

        Task<Location> FindByAddressAsync(string address);

        Task<Location> FindByUrlAsync(string url);

        Task<Location> FindByAddressOrUrlAsync(string location);
    }
}
