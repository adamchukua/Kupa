using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class UserService : CurrentUserService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IProfileRepository profileRepository,
            IMapper mapper) : base(httpContextAccessor)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task DeleteUserAndProfile(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new ArgumentException($"User with id {id} doesn't exist");
            }

            if (user.Id != UserId && UserRole != "Admin")
            {
                throw new UnauthorizedAccessException($"You can't delete user with id {id}");
            }

            await _userRepository.Delete(user);
        }

        public async Task UpdateUserProfile(int userId, UserProfileDto userProfileDto)
        {
            User user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException($"User with id {userId} doesn't exist");
            }

            if (user.Id != UserId)
            {
                throw new UnauthorizedAccessException($"You can't update user with id {userId}");
            }

            UserProfile profile = _mapper.Map<UserProfile>(userProfileDto);
            profile.Id = user.Profile.Id;
            profile.UserId = userId;

            await _profileRepository.UpdateUserProfile(profile);
        }
    }
}
