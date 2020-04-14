using System.ComponentModel.DataAnnotations;
using Shared;

namespace LocationQueryService.Domain.Models
{
    public class Location : ILocation
    {
        [Key]
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
