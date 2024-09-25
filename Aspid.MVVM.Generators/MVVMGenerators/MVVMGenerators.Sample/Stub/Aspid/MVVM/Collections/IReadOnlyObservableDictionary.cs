using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public interface IReadOnlyObservableDictionary<TKey, TValue> : IObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue> { }
}