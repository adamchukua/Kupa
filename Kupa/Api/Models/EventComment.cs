using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kupa.Api.Models
{
    public class EventComment
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        [ForeignKey("User")]
        public int CreatedByUserId { get; set; }

        [Required]
        public required string Comment { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public virtual Event Event { get; set; }

        public virtual User User { get; set; }
    }
}
