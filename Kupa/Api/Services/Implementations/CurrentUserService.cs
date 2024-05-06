using Kupa.Api.Models;
using Kupa.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Kupa.Api.Services.Implementations
{
    public class CurrentUserService
    {
        public readonly int? UserId;
        public readonly string? UserRole;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            ClaimsPrincipal? userClaims = httpContextAccessor.HttpContext?.User;

            if (int.TryParse(userClaims?.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                UserId = userId;
                UserRole = userClaims?.FindFirstValue(ClaimTypes.Role);
            }
        }
    }
}
