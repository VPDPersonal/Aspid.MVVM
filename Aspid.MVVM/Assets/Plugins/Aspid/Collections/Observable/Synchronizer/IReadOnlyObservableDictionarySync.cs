using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable.Synchronizer
{
    public interface IReadOnlyObservableDictionarySync<TKey, TValue> : IReadOnlyObservableDictionary<TKey, TValue>, IReadOnlyObservableCollectionSync<KeyValuePair<TKey, TValue>> { }
}