using System;

namespace Shared.Interfaces
{
    public interface IEntity<T>
    {
        public T EarTag { get; set; }
    }
}