using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public interface IReadOnlyObservableDictionary<TKey, TValue> : IObservableCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue> { }
}