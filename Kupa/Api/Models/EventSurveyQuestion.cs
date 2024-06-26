﻿using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class EventSurveyQuestion
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }

        [Required]
        public required string Question {  get; set; }

        public bool IsRequired { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public virtual Event Event { get; set; }

        public virtual IEnumerable<EventSurveyAnswer> EventSurveyAnswers { get; set; }
    }
}
