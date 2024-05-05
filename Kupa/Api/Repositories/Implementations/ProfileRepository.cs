using Kupa.Api.Data;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Base;
using Kupa.Api.Repositories.Interfaces;

namespace Kupa.Api.Repositories.Implementations
{
    public class ProfileRepository : RepositoryBase<UserProfile>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task CreateUserProfile(UserProfile profile)
        {
            profile.CreatedAt = DateTime.Now;
            profile.LastUpdatedAt = DateTime.Now;
            await AddItemAsync(profile);
        }
    }
}
