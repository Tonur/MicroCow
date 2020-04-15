using System;

namespace Shared.Interfaces
{
    public interface ICow
    {
        string EarTag { get; set; }
        string Name { get; set; }
        DateTime Birthday { get; set; }
    }
}
