using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Shared;
using Shared.Interfaces;

namespace LocationQueryService.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class CowLocation : KeyedBaseModel<IEntity<string>, string>, ICowLocation, IEntity<string>
    {
        [Key] 
        public override string EarTag { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
