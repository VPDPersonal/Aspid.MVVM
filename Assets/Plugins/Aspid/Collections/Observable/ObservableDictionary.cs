using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aspid.Collections.Observable
{
    public class ObservableDictionary<TKey, TValue> :
        IDictionary<TKey, TValue>,
        IReadOnlyObservableDictionary<TKey, TValue>,
        IDisposable
        where TKey : notnull
    {
        public event NotifyCollectionChangedEventHandler<KeyValuePair<TKey, TValue>>? CollectionChanged;
        
        private readonly Dictionary<TKey, TValue> _dictionary;

        public ObservableDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
        }
        
        public ObservableDictionary(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
        }
        
        public ObservableDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer: comparer);
        }

        public ObservableDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer: comparer);
        }

        public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer = null)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer: comparer);
            foreach (var item in collection)
            {
                _dictionary.Add(item.Key, item.Value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary[key];
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    if (_dictionary.TryGetValue(key, out var oldValue))
                    {
                        _dictionary[key] = value;

                        var oldItem = new KeyValuePair<TKey, TValue>(key, oldValue);
                        var newItem = new KeyValuePair<TKey, TValue>(key, value);
                        
                        OnReplaced(oldItem, newItem);
                        CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Replace(oldItem, newItem, -1));
                    }
                    else
                    {
                        Add(key, value);
                    }
                }
            }
        }

        public int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary.Count;
                }
            }
        }

        public bool IsReadOnly => false;
        
        public object SyncRoot { get; } = new();
        
        // for lock synchronization, hide keys and values.
        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary.Keys;
                }
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary.Values;
                }
            }
        }
        
        public IEnumerable<TKey> Keys
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary.Keys;
                }
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary.Values;
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (SyncRoot)
            {
                _dictionary.Add(key, value);
                OnAdded(key, value);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Add(new KeyValuePair<TKey, TValue>(key, value), -1));
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item) =>
            Add(item.Key, item.Value);
        
        protected virtual void OnAdded(in TKey key, in TValue value) { }

        public bool ContainsKey(TKey key)
        {
            lock (SyncRoot)
            {
                return ((IDictionary<TKey, TValue>)_dictionary).ContainsKey(key);
            }
        }

        public bool Remove(TKey key)
        {
            lock (SyncRoot)
            {
                if (!_dictionary.Remove(key, out var value)) return false;
               
                OnRemoved(key, value);
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Remove(new KeyValuePair<TKey, TValue>(key, value), -1));
                return true;
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (SyncRoot)
            {
                if (!_dictionary.TryGetValue(item.Key, out var value)) return false;
                if (!EqualityComparer<TValue>.Default.Equals(value, item.Value)) return false;
                if (!_dictionary.Remove(item.Key, out var value2)) return false;

                OnRemoved(item.Key, item.Value);
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Remove(new KeyValuePair<TKey, TValue>(item.Key, value2), -1));
                return true;
            }
        }

        protected virtual void OnRemoved(in TKey key, in TValue value) { }
        
        protected virtual void OnReplaced(in KeyValuePair<TKey, TValue> oldItem, in KeyValuePair<TKey, TValue> newItem) { }

#pragma warning disable CS8767
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
#pragma warning restore CS8767
        {
            lock (SyncRoot)
            {
                return _dictionary.TryGetValue(key, out value);
            }
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            lock (SyncRoot)
            {
                return ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);
            }
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            lock (SyncRoot)
            {
                ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
            }
        }
        
        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                lock (SyncRoot)
                {
                    return _dictionary.Comparer;
                }
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            lock (SyncRoot)
            {
                foreach (var item in _dictionary)
                    yield return item;
            }
        }
        
        public void Clear()
        {
            lock (SyncRoot)
            {
                OnClearing();
                _dictionary.Clear();
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>>.Reset());
            }
        }
        
        protected virtual void OnClearing() { }

        public virtual void Dispose() =>
            Clear();
    }
}