using System;

namespace Shared.Interfaces
{
    public interface IEntity<TKey>
    {
        public TKey EarTag { get; set; }
    }
}