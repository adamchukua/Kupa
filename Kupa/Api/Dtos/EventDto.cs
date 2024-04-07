using Kupa.Api.Enums;

namespace Kupa.Api.Dtos
{
    public class EventDto
    {
        public string Title { get; set; }

        public EventStatusId StatusId { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LocationId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
