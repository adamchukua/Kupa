using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kupa.Api.Repositories.Implementations
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task Delete(User user)
        {
            await DeleteItemAsync(user);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await Where(u => u.Id == id)
                .Include(u => u.Profile)
                .FirstOrDefaultAsync();
        }
    }
}
