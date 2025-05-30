using System.Collections.Generic;

namespace Aspid.Collections.Observable.Synchronizer
{
    public interface IReadOnlyObservableDictionarySync<TKey, TValue> : IReadOnlyObservableDictionary<TKey, TValue>, IReadOnlyObservableCollectionSync<KeyValuePair<TKey, TValue>> { }
}