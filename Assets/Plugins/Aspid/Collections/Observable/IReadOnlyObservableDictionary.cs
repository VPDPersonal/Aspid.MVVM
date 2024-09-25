using System.Collections.Generic;

namespace Aspid.Collections.Observable
{
    public interface IReadOnlyObservableDictionary<TKey, TValue> : IObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue> { }
}