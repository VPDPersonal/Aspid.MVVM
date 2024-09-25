using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public sealed partial class ObservableSet<T> : IReadOnlyObservableCollection<T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;

        private readonly ISet<T> _set;
        
        public int Count => _set.Count;
        
        public bool IsReadOnly => _set.IsReadOnly;

        public ObservableSet() 
            : this(new HashSet<T>()) { }

        public ObservableSet(int capacity)
            : this(new HashSet<T>(capacity)) { }

        public ObservableSet(IEnumerable<T> collection) 
            : this(new HashSet<T>(collection)) { }
        
        public ObservableSet(ISet<T> set)
        {
            _set = set;
        }
        
        public bool Add(T item)
        {
            if (!_set.Add(item)) return false;
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, -1));
            return true;
        }
        
        // public void AddRange(IEnumerable<T> items)
        // {
        //     
        // }

        // public void AddRange(T[] items) =>
        //     AddRange(items.AsSpan());

        // public void AddRange(ReadOnlySpan<T> items)
        // {
        //     
        // }

        public bool Remove(T item)
        {
            if (!_set.Remove(item)) return false;
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, -1));
            return true;
        }
        
        // public void RemoveRange(IEnumerable<T> items)
        // {
        //
        // }

        // public void RemoveRange(T[] items) =>
        //     RemoveRange(items.AsSpan());

        // public void RemoveRange(ReadOnlySpan<T> items)
        // {
        //
        // }
        
        // public void ExceptWith(IEnumerable<T> other)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void IntersectWith(IEnumerable<T> other)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public bool Overlaps(IEnumerable<T> other)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void UnionWith(IEnumerable<T> other)
        // {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void SymmetricExceptWith(IEnumerable<T> other)
        // {
        //     throw new System.NotImplementedException();
        // }
        
        public bool IsProperSubsetOf(IEnumerable<T> other) =>
            _set.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) =>
            _set.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) =>
            _set.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) =>
            _set.IsSupersetOf(other);

        public bool Contains(T item) =>
            _set.Contains(item);

        public bool SetEquals(IEnumerable<T> other) =>
            _set.SetEquals(other);

        public void CopyTo(T[] array, int arrayIndex) =>
            _set.CopyTo(array, arrayIndex);
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        // ReSharper disable once NotDisposedResourceIsReturned
        public IEnumerator<T> GetEnumerator() =>
            _set.GetEnumerator();

        public void Clear()
        {
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset(_set.ToList()));
            _set.Clear();
        }
    }
}