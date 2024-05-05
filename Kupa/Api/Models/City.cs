using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Region { get; set; }

        public string Hromada { get; set; }

        public string Name { get; set; }

        public virtual IEnumerable<Location> Locations { get; set; }

        public virtual IEnumerable<Event> Events { get; set; }
    }
}
