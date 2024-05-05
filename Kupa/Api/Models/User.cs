using Microsoft.AspNetCore.Identity;

namespace Kupa.Api.Models
{
    public class User : IdentityUser<int>
    {
        public virtual UserProfile Profile { get; set; }
    }
}
