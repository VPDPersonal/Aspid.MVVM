using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Aspid.Collections.Observable
{
#if UNITY_2021_3_OR_NEWER
    [Serializable]
#endif
    public class ObservableList<T> : IList<T>, IReadOnlyObservableList<T>, IDisposable
    {
        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged;

#if UNITY_2021_3_OR_NEWER
        [UnityEngine.SerializeField] 
#endif
        private List<T> _list;

        public ObservableList()
        {
            _list = new List<T>();
        }

        public ObservableList(int capacity)
        {
            _list = new List<T>(capacity);
        }

        public ObservableList(IEnumerable<T> collection)
        {
            _list = collection.ToList();
        }

        public T this[int index]
        {
            get
            {
                lock (SyncRoot)
                {
                    return _list[index];
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    var oldValue = _list[index];
                    _list[index] = value;

                    OnReplaced(index, oldValue, value);
                    CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Replace(oldValue, value, index));
                }
            }
        }

        public int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _list.Count;
                }
            }
        }

        public bool IsReadOnly => false;
        
        public object SyncRoot { get; } = new();

        public void Add(T item)
        {
            lock (SyncRoot)
            {
                var index = _list.Count;
                
                _list.Add(item);
                OnAdded(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
            }
        }
        
        protected virtual void OnAdded(in T item) { }

        public void AddRange(T[] items)
        {
            lock (SyncRoot)
            {
                var index = _list.Count;
                
                _list.AddRange(items);
                OnAddedRange(items);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
            }
        }

        public void AddRange(IReadOnlyList<T> items)
        {
            lock (SyncRoot)
            {
                var index = _list.Count;

                _list.AddRange(items);
                OnAddedRange(items);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
            }
        }
        
        protected virtual void OnAddedRange(in IReadOnlyList<T> items) { }

        public void Insert(int index, T item)
        {
            lock (SyncRoot)
            {
                _list.Insert(index, item);
                OnAdded(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
            }
        }

        public void InsertRange(int index, T[] items)
        {
            lock (SyncRoot)
            {
                _list.InsertRange(index, items);
                OnAddedRange(items);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
            }
        }

        public void InsertRange(int index, IReadOnlyList<T> items)
        {
            lock (SyncRoot)
            {
                _list.InsertRange(index, items);
                OnAddedRange(items);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
            }
        }

        public bool Remove(T item)
        {
            lock (SyncRoot)
            {
                var index = _list.IndexOf(item);
                if (index < 0) return false;
                
                _list.RemoveAt(index);
                OnRemoved(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, index));
                return true;
            }
        }

        public void RemoveAt(int index)
        {
            lock (SyncRoot)
            {
                var item = _list[index];
                
                _list.RemoveAt(index);
                OnRemoved(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, index));
            }
        }
        
        protected virtual void OnRemoved(in T item) { }

        public void Move(int oldIndex, int newIndex)
        {
            lock (SyncRoot)
            {
                var removedItem = _list[oldIndex];
                
                _list.RemoveAt(oldIndex);
                _list.Insert(newIndex, removedItem);
                OnMoved(oldIndex, newIndex, removedItem);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Move(removedItem, newIndex, oldIndex));
            }
        }
        
        protected virtual void OnMoved(int oldIndex, int newIndex, in T item) { }
        
        protected virtual void OnReplaced(int index, in T oldItem, in T newItem) { }
        
        public int IndexOf(T item)
        {
            lock (SyncRoot)
            {
                return _list.IndexOf(item);
            }
        }
        
        public bool Contains(T item)
        {
            lock (SyncRoot)
            {
                return _list.Contains(item);
            }
        }
        
        public void ForEach(Action<T> action)
        {
            lock (SyncRoot)
            {
                foreach (var item in _list)
                    action(item);
            }
        }
        
        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (SyncRoot)
            {
                _list.CopyTo(array, arrayIndex);
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public IEnumerator<T> GetEnumerator()
        {
            lock (SyncRoot)
            {
                foreach (var item in _list)
                    yield return item;
            }
        }
        
        public void Clear()
        {
            lock (SyncRoot)
            {
                OnClearing();
                _list.Clear();
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset());
            }
        }
        
        protected virtual void OnClearing() { }

        public virtual void Dispose() =>
            Clear();
    }
}
