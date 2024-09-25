using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public sealed partial class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyObservableDictionary<TKey, TValue>
        where TKey : notnull
    {
        public event NotifyCollectionChangedEventHandler<KeyValuePair<TKey, TValue>> CollectionChanged;
        
        private readonly IDictionary<TKey, TValue> _dictionary;

        public int Count => _dictionary.Count;

        public bool IsReadOnly => _dictionary.IsReadOnly;

        public ICollection<TKey> Keys => _dictionary.Keys;
        
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;
        
        public ICollection<TValue> Values => _dictionary.Values;
        
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
        
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                if (_dictionary.TryGetValue(key, out var oldValue))
                {
                    _dictionary[key] = value;
                    CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Replace(
                        new KeyValuePair<TKey, TValue>(key, value),
                        new KeyValuePair<TKey, TValue>(key, oldValue!),
                        -1));
                }
                else Add(key, value);
            }
        }
        
        public ObservableDictionary()
            : this(new Dictionary<TKey, TValue>()) { }
        
        public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) :
            this(new Dictionary<TKey, TValue>(collection)) { }
        
        public ObservableDictionary(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public bool TryGetValue(TKey key, out TValue value) =>
            _dictionary.TryGetValue(key, out value);
        
        public void Add(KeyValuePair<TKey, TValue> item) =>
            Add(item.Key, item.Value);
        
        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Add(new KeyValuePair<TKey, TValue>(key, value), -1));
        }
        
        public bool Remove(TKey key)
        {
            if (!_dictionary.Remove(key, out var value)) return false;
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Remove(new KeyValuePair<TKey, TValue>(key, value), -1));
            return true;
        }
        
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var isSuccess = _dictionary.Remove(item);
            if (!isSuccess) return false;
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Remove(new KeyValuePair<TKey, TValue>(item.Key, item.Value), -1));
            return true;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            _dictionary.CopyTo(array, arrayIndex);

        public bool Contains(KeyValuePair<TKey, TValue> item) =>
            _dictionary.Contains(item);
        
        public bool ContainsKey(TKey key) =>
            _dictionary.ContainsKey(key);
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        // ReSharper disable once NotDisposedResourceIsReturned
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            _dictionary.GetEnumerator();
        
        public void Clear()
        {
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Reset(_dictionary.ToList()));
            _dictionary.Clear();
        }
    }
}