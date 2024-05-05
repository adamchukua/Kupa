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
        private readonly IUserService _userSerice;

        public UserController(IUserService userService)
        {
            _userSerice = userService;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAndProfile(int id)
        {
            await _userSerice.DeleteUserAndProfile(id);
            return NoContent();
        }

        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(int userId, UserProfileDto userProfileDto)
        {
            await _userSerice.UpdateUserProfile(userId, userProfileDto);
            return NoContent();
        }
    }
}
