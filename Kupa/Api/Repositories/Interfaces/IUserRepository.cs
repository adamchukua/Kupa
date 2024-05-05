using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetByIdAsync(int id);

        public Task Delete(User user);
    }
}
