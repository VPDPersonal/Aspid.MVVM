namespace Aspid.Collections.Observable;

public class ObservableQueue<T> : CollectionChangedEvent<T>, IObservableCollection<T>, IDisposable
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
            OnEnqueued(item);
                
            Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, index));
        }
    }
        
    protected virtual void OnEnqueued(T item) { }

    public void EnqueueRange(T[] items)
    {
        lock (SyncRoot)
        {
            var index = _queue.Count;
            foreach (var item in items)
                _queue.Enqueue(item);
                
            OnEnqueuedRange(items);
                
            Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
        }
    }

    public void EnqueueRange(IReadOnlyList<T> items)
    {
        lock (SyncRoot)
        {
            var index = _queue.Count;
            foreach (var item in items)
                _queue.Enqueue(item);

            OnEnqueuedRange(items);
                
            Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, index));
        }
    }
        
    protected virtual void OnEnqueuedRange(in IReadOnlyList<T> items) { }

    public T Dequeue()
    {
        lock (SyncRoot)
        {
            var result = _queue.Dequeue();
            OnDequeued(result);
                
            Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
            return result;
        }
    }

    public bool TryDequeue([MaybeNullWhen(false)] out T result)
    {
        lock (SyncRoot)
        {
            if (_queue.Count != 0)
            {
                result = _queue.Dequeue();
                OnDequeued(result);
                    
                Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
                return true;
            }
                
            result = default;
            return false;
        }
    }

    protected virtual void OnDequeued(T item) { }
        
    public void DequeueRange(in T[] dest)
    {
        lock (SyncRoot)
        {
            for (var i = 0; i < dest.Length; i++)
                dest[i] = _queue.Dequeue();

            OnDequeuedRange(dest);
            Invoke(NotifyCollectionChangedEventArgs<T>.Remove(dest, 0));
        }
    }

    protected virtual void OnDequeuedRange(in IReadOnlyList<T> dest) { }

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
            OnClearing();
            _queue.Clear();
                
            Invoke(NotifyCollectionChangedEventArgs<T>.Reset());
        }
    }
        
    protected virtual void OnClearing() { }

    public virtual void Dispose() =>
        Clear();
}