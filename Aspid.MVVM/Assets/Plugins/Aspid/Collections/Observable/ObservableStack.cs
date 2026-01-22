using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.Collections.Observable
{
    public class ObservableStack<T> : CollectionChangedEvent<T>, IObservableCollection<T>
    {
        private readonly Stack<T> _stack;

        public ObservableStack()
        {
            _stack = new Stack<T>();
        }

        public ObservableStack(int capacity)
        {
            _stack = new Stack<T>(capacity);
        }

        public ObservableStack(IEnumerable<T> collection)
        {
            _stack = new Stack<T>(collection);
        }

        public int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _stack.Count;
                }
            }
        }
        
        public object SyncRoot { get; } = new();

        public void Push(T item)
        {
            lock (SyncRoot)
            {
                _stack.Push(item);
                OnPushed(item);
                
                Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, 0));
            }
        }
        
        protected virtual void OnPushed(T item) { }

        public void PushRange(T[] items)
        {
            lock (SyncRoot)
            {
                foreach (var item in items)
                    _stack.Push(item);
                
                OnPushedRange(items);
                Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, 0));
            }
        }

        public void PushRange(IReadOnlyList<T> items)
        {
            lock (SyncRoot)
            {
                foreach (var item in items)
                    _stack.Push(item);

                OnPushedRange(items);
                Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, 0));
            }
        }
        
        protected virtual void OnPushedRange(in IReadOnlyList<T> items) { }

        public T Pop()
        {
            lock (SyncRoot)
            {
                var result = _stack.Pop();
                OnPopped(result);
                
                Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
                return result;
            }
        }

        public bool TryPop([MaybeNullWhen(false)] out T result)
        {
            lock (SyncRoot)
            {
                if (_stack.Count != 0)
                {
                    result = _stack.Pop();
                    OnPopped(result);
                    
                    Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
                    return true;
                }

                result = default;
                return false;
            }
        }
        
        protected virtual void OnPopped(T item) { }

        public void PopRange(T[] dest)
        {
            lock (SyncRoot)
            {
                for (var i = 0; i < dest.Length; i++)
                    dest[i] = _stack.Pop();

                OnPoppedRange(dest);
                Invoke(NotifyCollectionChangedEventArgs<T>.Remove(dest, 0));
            }
        }

        protected virtual void OnPoppedRange(in IReadOnlyList<T> dest) { }

        public T Peek()
        {
            lock (SyncRoot)
            {
                return _stack.Peek();
            }
        }

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            lock (SyncRoot)
            {
                if (_stack.Count != 0)
                {
                    result = _stack.Peek();
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
                return _stack.ToArray();
            }
        }

        public void TrimExcess()
        {
            lock (SyncRoot)
            {
                _stack.TrimExcess();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (SyncRoot)
            {
                foreach (var item in _stack)
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public void Clear()
        {
            lock (SyncRoot)
            {
                OnClearing();
                _stack.Clear();
                
                Invoke(NotifyCollectionChangedEventArgs<T>.Reset());
            }
        }

        protected virtual void OnClearing() { }

        public override void Dispose()
        {
            Clear();
            base.Dispose();
        }
    }
}