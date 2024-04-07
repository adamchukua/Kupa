using Kupa.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public EventStatusId StatusId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int LocationId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public Location Location { get; set; }
    }
}
