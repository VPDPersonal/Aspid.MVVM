using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AspidUI.MVVM.Collections
{
    public sealed partial class ObservableStack<T> : ICollection, IReadOnlyObservableCollection<T>
    {
        public event NotifyCollectionChangedEventHandler<T> CollectionChanged;
        
        private readonly Stack<T> _stack;

        public int Count => _stack.Count;

        object ICollection.SyncRoot => ((ICollection)_stack).SyncRoot;
        
        bool ICollection.IsSynchronized => ((ICollection)_stack).IsSynchronized;
        
        public ObservableStack() :
            this(new Stack<T>()) { }

        public ObservableStack(int capacity) :
            this(new Stack<T>(capacity)) { }

        public ObservableStack(IEnumerable<T> collection) :
            this(new Stack<T>(collection)) { }
        
        public ObservableStack(Stack<T> stack)
        {
            _stack = stack;
        }
        
        public void Push(T item)
        {
            _stack.Push(item);
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(item, 0));
        }

        // public void PushRange(IEnumerable<T> items)
        // {
        //     
        // }

        public void PushRange(T[] items)
        {
            foreach (var item in items)
                _stack.Push(item);
                
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Add(items, 0));
        }

        // public void PushRange(ReadOnlySpan<T> items)
        // {
        //     
        // }

        public T Pop()
        {
            var item = _stack.Pop();
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(item, 0));
            return item;
        }

        public bool TryPop(out T result)
        {
            var isSuccess = _stack.TryPop(out result);
            if (!isSuccess) return false;
            
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Remove(result, 0));
            return true;
        }

        // public void PopRange(int count)
        // {
        //     
        // }

        // public void PopRange(Span<T> dest)
        // {
        //     
        // }

        public T Peek() =>
            _stack.Peek();

        public bool TryPeek(out T result) =>
            _stack.TryPeek(out result);

        public T[] ToArray() =>
            _stack.ToArray();

        public void TrimExcess() =>
            _stack.TrimExcess();

        public void CopyTo(T[] array, int arrayIndex) =>
            _stack.CopyTo(array, arrayIndex);
        
        void ICollection.CopyTo(System.Array array, int arrayIndex) =>
            ((ICollection)_stack).CopyTo(array, arrayIndex);
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public IEnumerator<T> GetEnumerator() =>
            _stack.GetEnumerator();
        
        public void Clear()
        {
            CollectionChanged?.Invoke(NotifyCollectionChangedEventArgs<T>.Reset(_stack.ToList()));
            _stack.Clear();
        }
    }
}