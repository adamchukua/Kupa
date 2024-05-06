using Kupa.Api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kupa.Api.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public EventStatusId StatusId { get; set; }

        [ForeignKey("User")]
        public int CreatedByUserId { get; set; }

        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int LocationId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public virtual EventStatus Status { get; set; }

        public virtual Location Location { get; set; }

        public virtual Category Category { get; set; }

        public virtual City City { get; set; }

        public virtual User User { get; set; }

        public virtual IEnumerable<EventSurveyQuestion> EventSurveyQuestions { get; set; }

        public virtual IEnumerable<EventComment> EventComments { get; set; }

        public virtual IEnumerable<EventRegistration> EventRegistrations { get; set; }
    }
}
