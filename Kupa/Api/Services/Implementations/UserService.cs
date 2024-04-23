using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Kupa.Api.Services.Implementations
{
    public class UserService
    {
        public readonly int? UserId;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            if (int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                UserId = userId;
            }
        }
    }
}
