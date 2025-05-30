using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Aspid.Collections.Observable
{
    internal sealed class ObservableCollectionEvents<T> : IObservableEvents<T>
    {
        public event Action? Reset
        {
            add => _reset += value;
            remove => _reset -= value;
        }
        
        public event Action<IReadOnlyList<T>, int>? Added
        {
            add => _added += value;
            remove => _added -= value;
        }
        
        public event Action<IReadOnlyList<T>, int>? Removed
        {
            add => _removed += value;
            remove => _removed -= value;
        }
        
        public event Action<IReadOnlyList<T>, int, int>? Moved
        {
            add => _moved += value;
            remove => _moved -= value;
        }
        
        public event Action<IReadOnlyList<T>, IReadOnlyList<T?>, int>? Replaced
        {
            add => _replaced += value;
            remove => _replaced -= value;
        }

        private Action? _reset;
        private Action<IReadOnlyList<T>, int>? _added;
        private Action<IReadOnlyList<T>, int>? _removed;
        private Action<IReadOnlyList<T>, int, int>? _moved;
        private Action<IReadOnlyList<T>, IReadOnlyList<T?>, int>? _replaced;
        
        public ObservableCollectionEvents(
            IObservableCollection<T> list,
            Action<IReadOnlyList<T?>, int>? added = null, 
            Action<IReadOnlyList<T?>, int>? removed = null, 
            Action<IReadOnlyList<T?>, int, int>? moved = null,
            Action<IReadOnlyList<T?>, IReadOnlyList<T?>, int>? replaced = null,
            Action? reset = null)
        {
            _reset = reset;
            _added = added;
            _moved = moved;
            _removed = removed;
            _replaced = replaced;
            list.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _added?.Invoke(e.IsSingleItem 
                        ? new[] { e.NewItem! } : e.NewItems!,
                        e.NewStartingIndex);
                    break;
                
                case NotifyCollectionChangedAction.Move:
                    _moved?.Invoke(e.IsSingleItem 
                        ? new[] { e.NewItem! } : e.NewItems!,
                        e.OldStartingIndex, e.NewStartingIndex);
                    break;
                
                case NotifyCollectionChangedAction.Remove:
                    _removed?.Invoke(e.IsSingleItem 
                        ? new[] { e.OldItem }! : e.OldItems!, 
                        e.OldStartingIndex);
                    break;
                
                case NotifyCollectionChangedAction.Replace: 
                    _replaced?.Invoke(
                        e.IsSingleItem ? new[] { e.OldItem }! : e.OldItems!,
                        e.IsSingleItem ? new[] { e.NewItem }! : e.NewItems!, 
                        e.NewStartingIndex);
                    break;
                
                case NotifyCollectionChangedAction.Reset:
                    _reset?.Invoke();
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public void Dispose()
        {
            _reset = null;
            _added = null;
            _moved = null;
            _removed = null;
            _replaced = null;
        }
    }
}