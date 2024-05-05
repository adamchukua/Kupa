using Kupa.Api.Dtos;

namespace Kupa.Api.Services.Interfaces
{
    public interface IUserService
    {
        public Task DeleteUserAndProfile(int id);

        public Task UpdateUserProfile(int userId, UserProfileDto userProfileDto);
    }
}
