using System;

namespace Aspid.Collections.Observable.Synchronizer
{
    public interface IReadOnlyObservableCollectionSync<out T> : IReadOnlyObservableCollection<T>, IDisposable { }
}