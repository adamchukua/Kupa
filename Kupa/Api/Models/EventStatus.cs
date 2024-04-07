using Kupa.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class EventStatus
    {
        [Key]
        public EventStatusId Id { get; set; }

        public string Name { get; set; }
    }
}
