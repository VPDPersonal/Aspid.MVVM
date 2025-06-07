using System.Collections;
using System.Collections.Generic;

namespace Aspid.Collections.Observable
{
    // can not implements ISet<T> because set operation can not get added/removed values.
    public sealed partial class ObservableHashSet<T> : IReadOnlyObservableCollection<T>
        where T : notnull
    {
        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged;
        
        private readonly HashSet<T> _set;

        public ObservableHashSet()
        {
            _set = new HashSet<T>();
        }
        
        public ObservableHashSet(int capacity)
        {
            _set = new HashSet<T>(capacity);
        }

        public ObservableHashSet(IEqualityComparer<T>? comparer)
        {
            _set = new HashSet<T>(comparer: comparer);
        }
        
        public ObservableHashSet(IEnumerable<T> collection)
        {
            _set = new HashSet<T>(collection: collection);
        }

        public ObservableHashSet(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            _set = new HashSet<T>(collection: collection, comparer: comparer);
        }

        public int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _set.Count;
                }
            }
        }

        public bool IsReadOnly => false;
        
        public object SyncRoot { get; } = new();

        public bool Add(T item)
        {
            lock (SyncRoot)
            {
                if (!_set.Add(item)) return false;
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, -1));
                return true;
            }
        }

        public bool Remove(T item)
        {
            lock (SyncRoot)
            {
                if (!_set.Remove(item)) return false;
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, -1));
                
                return true;
            }
        }

        public void Clear()
        {
            lock (SyncRoot)
            {
                _set.Clear();
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset());
            }
        }

        public bool Contains(T item)
        {
            lock (SyncRoot)
            {
                return _set.Contains(item);
            }
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            lock (SyncRoot)
            {
                return _set.IsProperSubsetOf(other);
            }
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            lock (SyncRoot)
            {
                return _set.IsProperSupersetOf(other);
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            lock (SyncRoot)
            {
                return _set.IsSubsetOf(other);
            }
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            lock (SyncRoot)
            {
                return _set.IsSupersetOf(other);
            }
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            lock (SyncRoot)
            {
                return _set.Overlaps(other);
            }
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            lock (SyncRoot)
            {
                return _set.SetEquals(other);
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            lock (SyncRoot)
            {
                foreach (var item in _set)
                    yield return item;
            }
        }

        public IEqualityComparer<T> Comparer
        {
            get
            {
                lock (SyncRoot)
                {
                    return _set.Comparer;
                }
            }
        }
    }
}