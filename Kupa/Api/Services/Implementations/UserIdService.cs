using System.Security.Claims;

namespace Kupa.Api.Services.Implementations
{
    public class UserIdService
    {
        public readonly int? UserId;

        public UserIdService(IHttpContextAccessor httpContextAccessor)
        {
            if (int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
            {
                UserId = userId;
            }
        }
    }
}
