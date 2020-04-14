using System;
using System.ComponentModel.DataAnnotations;
using Shared;

namespace MasterDataService.Domain.Models
{
    public class Cow : ICow
    {
        [Key]
        public string EarTag { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
    }
}
