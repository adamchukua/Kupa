using AutoMapper;
using Kupa.Api.Dtos;
using Kupa.Api.Models;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Interfaces;

namespace Kupa.Api.Services.Implementations
{
    public class UserService : CurrentUserService, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IProfileRepository profileRepository;
        private readonly IValidator validator;
        private readonly IMapper mapper;

        public UserService(
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository,
            IProfileRepository profileRepository,
            IValidator validator,
            IMapper mapper) : base(httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.profileRepository = profileRepository;
            this.validator = validator;
            this.mapper = mapper;
        }

        public async Task DeleteUserAndProfile(int id)
        {
            User user = await userRepository.GetByIdAsync(id);

            validator.ObjectNull(user, string.Empty, $"User with id {id} doesn't exist");
            validator.AuthorizedOrAdminAction(user.Id, (int)UserId, UserRole, "you can't delete this user");

            await userRepository.Delete(user);
        }

        public async Task UpdateUserProfile(int userId, UserProfileDto userProfileDto)
        {
            User user = await userRepository.GetByIdAsync(userId);

            validator.ObjectNull(user, string.Empty, $"User with id {id} doesn't exist");
            validator.AuthorizedAction(user.Id, (int)UserId, "you can't update this user");

            UserProfile profile = mapper.Map<UserProfile>(userProfileDto);
            profile.Id = user.Profile.Id;
            profile.UserId = userId;

            await profileRepository.UpdateUserProfile(profile);
        }
    }
}
