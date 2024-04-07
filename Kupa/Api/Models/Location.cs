using Kupa.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public LocationTypeId TypeId { get; set; }

        public string? Address { get; set; }

        public string? Url { get; set; }

        public ICollection<Event> Events { get; set; }

        public static Location CreateNewLocation(Location location)
        {
            return new Location 
            { 
                TypeId = location.TypeId,
                Address = location.Address,
                Url = location.Url,
            };
        }
    }
}
