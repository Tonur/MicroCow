using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface ILocation
    {
        string Name { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
