using Microsoft.AspNetCore.Identity;

namespace Kupa.Api.Models
{
    public class User : IdentityUser<int>
    {
        public UserProfile Profile { get; set; }
    }
}
