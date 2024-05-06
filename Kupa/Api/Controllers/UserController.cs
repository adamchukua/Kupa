using Kupa.Api.Dtos;
using Kupa.Api.Repositories.Interfaces;
using Kupa.Api.Services.Implementations;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kupa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userSerice;
        private readonly IValidator validator;

        public UserController(IUserService userService, IValidator validator)
        {
            userSerice = userService;
            this.validator = validator;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAndProfile(int id)
        {
            validator.PositiveInt(id, nameof(id));

            await userSerice.DeleteUserAndProfile(id);
            return NoContent();
        }

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(int userId, UserProfileDto userProfileDto)
        {
            validator.PositiveInt(userId, nameof(userId));
            validator.ObjectNull(userProfileDto, nameof(userProfileDto));

            await userSerice.UpdateUserProfile(userId, userProfileDto);
            return NoContent();
        }
    }
}
