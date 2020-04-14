using System.ComponentModel.DataAnnotations;
using Shared;
using Shared.Interfaces;

namespace LocationEventService.Domain.Models
{
    public class CowLocation : ICowLocation, IEntity<string>
    {
        [Key]
        public string EarTag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
