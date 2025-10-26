using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    public interface IReadOnlyObservableDictionary<TKey, TValue> : 
        IObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue> { }
}