using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Aspid.Collections.Observable
{
    public sealed class ObservableQueue<T> : IReadOnlyObservableCollection<T>
    {
        private readonly Queue<T> _queue;

        public ObservableQueue()
        {
            _queue = new Queue<T>();
        }

        public ObservableQueue(int capacity)
        {
            _queue = new Queue<T>(capacity);
        }

        public ObservableQueue(IEnumerable<T> collection)
        {
            _queue = new Queue<T>(collection);
        }

        public ObservableQueue(Queue<T> queue)
        {
            _queue = queue;
        }

        public event NotifyCollectionChangedEventHandler<T>? CollectionChanged;

        public int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _queue.Count;
                }
            }
        }
        
        public object SyncRoot { get; } = new();

        public void Enqueue(T item)
        {
            lock (SyncRoot)
            {
                var index = _queue.Count;
                _queue.Enqueue(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
            }
        }

        public void EnqueueRange(T[] items)
        {
            lock (SyncRoot)
            {
                var index = _queue.Count;
                foreach (var item in items)
                    _queue.Enqueue(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
            }
        }

        public void EnqueueRange(IReadOnlyList<T> items)
        {
            lock (SyncRoot)
            {
                var index = _queue.Count;
                foreach (var item in items)
                    _queue.Enqueue(item);
                
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
            }
        }

        public T Dequeue()
        {
            lock (SyncRoot)
            {
                var v = _queue.Dequeue();
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(v, 0));
                return v;
            }
        }

        public bool TryDequeue([MaybeNullWhen(false)] out T result)
        {
            lock (SyncRoot)
            {
                if (_queue.Count != 0)
                {
                    result = _queue.Dequeue();
                    CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
                    return true;
                }
                
                result = default;
                return false;
            }
        }

        public void DequeueRange(T[] dest)
        {
            lock (SyncRoot)
            {
                for (var i = 0; i < dest.Length; i++)
                    dest[i] = _queue.Dequeue();

                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(dest, 0));
            }
        }

        public T Peek()
        {
            lock (SyncRoot)
            {
                return _queue.Peek();
            }
        }

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            lock (SyncRoot)
            {
                if (_queue.Count != 0)
                {
                    result = _queue.Peek();
                    return true;
                }
                
                result = default;
                return false;
            }
        }

        public T[] ToArray()
        {
            lock (SyncRoot)
            {
                return _queue.ToArray();
            }
        }

        public void TrimExcess()
        {
            lock (SyncRoot)
            {
                _queue.TrimExcess();
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            lock (SyncRoot)
            {
                foreach (var item in _queue)
                    yield return item;
            }
        }
        
        public void Clear()
        {
            lock (SyncRoot)
            {
                _queue.Clear();
                CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset());
            }
        }
    }
}