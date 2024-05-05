using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kupa.Api.Models
{
    public class EventSurveyAnswer
    {
        [Key]
        public int Id { get; set; }

        public int RegistrationId { get; set; }

        [ForeignKey("EventSurveyQuestion")]
        public int EventSurveyQuestionId { get; set; }

        [ForeignKey("User")]
        public int CreatedByUserId { get; set; }

        public required string Answer {  get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public virtual EventSurveyQuestion EventSurveyQuestion { get; set; }

        public virtual EventRegistration Registration { get; set; }

        public virtual User User { get; set; }
    }
}
