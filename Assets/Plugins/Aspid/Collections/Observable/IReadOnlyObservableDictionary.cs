using System.Collections.Generic;

namespace Aspid.Collections.Observable
{
    public interface IReadOnlyObservableDictionary<TKey, TValue> : 
        IReadOnlyObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue> { }
}