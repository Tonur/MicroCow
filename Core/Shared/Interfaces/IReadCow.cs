using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IReadCow
    {
        string Name { get; set; }
        DateTime Birthday { get; set; }
    }
}
