using System;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Synchronizer
{
    public interface IReadOnlyObservableCollectionSync<out T> : IObservableCollection<T>, IDisposable { }
}