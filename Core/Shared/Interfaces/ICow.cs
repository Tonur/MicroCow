using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface ICow
    {
        string EarTag { get; set; }
        string Name { get; set; }
        DateTime Birthday { get; set; }
    }
}
