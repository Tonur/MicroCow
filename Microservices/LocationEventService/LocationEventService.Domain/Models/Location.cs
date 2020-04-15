using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Shared;
using Shared.Interfaces;

namespace LocationEventService.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Location : KeyedBaseModel<IEntity<string>, string>, ILocation, IEntity<string>
    {
        [Key]
        public override string EarTag { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
