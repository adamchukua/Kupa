using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kupa.Api.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Activity { get; set; }

        public string? TelegramUsername { get; set; }

        public ushort BirthYear { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public virtual User User { get; set; }

        public static UserProfile Create(int userId)
        {
            return new UserProfile { UserId = userId };
        }
    }
}
