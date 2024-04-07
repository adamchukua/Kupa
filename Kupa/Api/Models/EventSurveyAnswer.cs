using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class EventSurveyAnswer
    {
        [Key]
        public int Id { get; set; }

        public int EventSurveyQuestionId { get; set; }

        public required string Answer {  get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public required EventSurveyQuestion EventSurveyQuestion { get; set; }

        public required User User { get; set; }
    }
}
