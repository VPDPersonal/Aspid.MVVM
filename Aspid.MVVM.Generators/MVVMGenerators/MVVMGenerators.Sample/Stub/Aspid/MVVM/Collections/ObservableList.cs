using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public sealed partial class ObservableList<T> : IList<T>, IReadOnlyObservableList<T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;
        
        private readonly IList<T> _list;

        public int Count => _list.Count;

        public bool IsReadOnly => _list.IsReadOnly;
        
        public T this[int index]
        {
            get => _list[index];
            set
            {
                var oldValue = _list[index];
                _list[index] = value;
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Replace(oldValue, value, index));
            }
        }
        
        public ObservableList() 
            : this(new List<T>()) { }
        
        public ObservableList(int capacity) 
            : this(new List<T>(capacity)) { }
        
        public ObservableList(IEnumerable<T> collection)
            : this(collection.ToList()) { }

        public ObservableList(IList<T> list)
        {
            _list = list; 
        }
        
        public void Add(T item)
        {
            var index = Count;
            _list.Add(item);
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
        }

        // public void AddRange(IEnumerable<T> items)
        // {
        //     
        // }

        // public void AddRange(params T[] items)
        // {
        //     
        // }

        // public void AddRange(ReadOnlySpan<T> items)
        // {
        //     
        // }
        
        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
        }
        
        // public void InsertRange(int index, T[] items)
        // {
        //
        // }

        // public void InsertRange(int index, IEnumerable<T> items)
        // {
        //
        // }

        // public void InsertRange(int index, ReadOnlySpan<T> items)
        // {
        //
        // }

        public void Move(int oldIndex, int newIndex)
        {
            var removedItem = _list[oldIndex];
            _list.RemoveAt(oldIndex);
            _list.Insert(newIndex, removedItem);
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Move(removedItem, oldIndex, newIndex));
        }
        
        public bool Remove(T item)
        {
            var index = _list.IndexOf(item);
            if (index < 0) return false;
            
            _list.RemoveAt(index); 
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, index));
            return true;
        }
        
        // public void RemoveRange(int index, int count)
        // {
        //     
        // }
        
        public void RemoveAt(int index)
        {
            var item = _list[index];
            _list.RemoveAt(index);
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, index));
        }

        public void CopyTo(T[] array, int arrayIndex) =>
            _list.CopyTo(array, arrayIndex);

        public int IndexOf(T item) =>
            _list.IndexOf(item);
        
        public bool Contains(T item) =>
            _list.Contains(item);

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        
        // ReSharper disable once NotDisposedResourceIsReturned
        public IEnumerator<T> GetEnumerator() => 
            _list.GetEnumerator();
        
        public void Clear()
        {
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset(_list.ToList()));
            _list.Clear();
        }
    }
}