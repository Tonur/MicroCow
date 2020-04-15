﻿using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using Shared;
using Shared.Interfaces;

namespace MasterDataService.Domain.Models
{
    [BsonIgnoreExtraElements]
    public class Cow : KeyedBaseModel<IEntity<string>, string>, ICow, IEntity<string>
    {
        [Key]
        public override string EarTag { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
