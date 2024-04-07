using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class EventSurveyQuestion
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        [Required]
        public string Question {  get; set; }

        public bool IsRequired { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public Event Event { get; set; }
    }
}
