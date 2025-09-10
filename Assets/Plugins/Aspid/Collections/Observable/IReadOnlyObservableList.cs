using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    public interface IReadOnlyObservableList<out T> : IObservableCollection<T>, IReadOnlyList<T> { }
}