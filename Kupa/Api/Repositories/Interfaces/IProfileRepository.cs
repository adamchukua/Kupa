using Kupa.Api.Models;

namespace Kupa.Api.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task CreateUserProfile(UserProfile profile);

        Task UpdateUserProfile(UserProfile profile);
    }
}
