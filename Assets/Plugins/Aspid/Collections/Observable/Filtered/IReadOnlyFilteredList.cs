using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Filtered
{
    public interface IReadOnlyFilteredList<out T> : IReadOnlyList<T>
    {
        public event Action? CollectionChanged;
    }
}