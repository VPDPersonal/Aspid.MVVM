using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public sealed partial class ObservableQueue<T> : ICollection, IReadOnlyObservableCollection<T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;

        private readonly Queue<T> _queue;
        
        public int Count => _queue.Count;
        
        object ICollection.SyncRoot => ((ICollection)_queue).SyncRoot;
        
        bool ICollection.IsSynchronized => ((ICollection)_queue).IsSynchronized;

        public ObservableQueue()
            : this(new Queue<T>()) { }
        
        public ObservableQueue(int capacity) 
            : this(new Queue<T>(capacity)) { }

        public ObservableQueue(IEnumerable<T> collection)
            : this(new Queue<T>(collection)) { }
        
        public ObservableQueue(Queue<T> queue)
        {
            _queue = queue;
        }

        public void Enqueue(T item)
        {
            var index = _queue.Count;
            _queue.Enqueue(item);
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
        }

        // public void EnqueueRange(IEnumerable<T> items)
        // {
        //     
        // }

        public void EnqueueRange(T[] items)
        {
            var index = _queue.Count;
            foreach (var item in items)
                _queue.Enqueue(item);
                
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
        }

        // public void EnqueueRange(ReadOnlySpan<T> items)
        // {
        //     
        // }

        public T Dequeue()
        {
            var item = _queue.Dequeue();
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, 0));
            return item;
        }

        public bool TryDequeue(out T result)
        {
            var isSuccess = _queue.TryDequeue(out result);
            if (!isSuccess) return false;
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
            return true;
        }

        // public void DequeueRange(int count)
        // {
        //
        // }

        // public void DequeueRange(Span<T> dest)
        // {
        //     
        // }

        public T Peek() =>
            _queue.Peek();

        public bool TryPeek(out T result) =>
            _queue.TryPeek(out result);

        public T[] ToArray() =>
            _queue.ToArray();

        public void TrimExcess() =>
            _queue.TrimExcess();
        
        public void CopyTo(T[] array, int arrayIndex) =>
            _queue.CopyTo(array, arrayIndex);
        
        void ICollection.CopyTo(System.Array array, int arrayIndex) =>
            ((ICollection)_queue).CopyTo(array, arrayIndex);
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<T> GetEnumerator() =>
            _queue.GetEnumerator();
        
        public void Clear()
        {
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset(_queue.ToList()));
            _queue.Clear();
        }
    }
}