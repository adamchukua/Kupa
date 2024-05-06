using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class EventRegistration
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Event Event { get; set; }

        public virtual IEnumerable<EventSurveyAnswer> EventSurveyAnswers { get; set; }

        public virtual User User { get; set; }

        public static EventRegistration CreateRegistration(int eventId, int userId)
        {
            return new EventRegistration { UserId = userId, EventId = eventId };
        }
    }
}
