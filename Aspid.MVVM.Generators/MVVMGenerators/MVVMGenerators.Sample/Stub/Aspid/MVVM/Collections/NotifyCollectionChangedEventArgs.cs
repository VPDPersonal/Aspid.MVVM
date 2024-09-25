using System.Collections.Generic;
using System.Collections.Specialized;

namespace AspidUI.MVVM.Collections
{
    public readonly struct NotifyCollectionChangedEventArgs<T> : INotifyCollectionChangedEventArgs<T>
    {
        public NotifyCollectionChangedAction Action { get; }

        public bool IsSingleItem { get; }
        
        public T OldItem { get; }
        
        public T NewItem { get; }

        public IReadOnlyList<T> OldItems { get; }
        
        public IReadOnlyList<T> NewItems { get; }

        public int OldStartingIndex { get; }
        
        public int NewStartingIndex { get; }

        public NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction action,
            T oldItem = default!,
            T newItem = default!,
            int oldStartingIndex = -1,
            int newStartingIndex = -1)
            : this(action, true, oldItem, newItem, default, default, oldStartingIndex, newStartingIndex) { }
        
        public NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction action,
            IReadOnlyList<T> oldItems = default, 
            IReadOnlyList<T> newItems = default, 
            int oldStartingIndex = -1,
            int newStartingIndex = -1) 
            : this(action, false, default!, default!, oldItems, newItems, oldStartingIndex, newStartingIndex) { }
        
        public NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction action,
            bool isSingleItem,
            T oldItem = default!, 
            T newItem = default!,
            IReadOnlyList<T> oldItems = default, 
            IReadOnlyList<T> newItems = default, 
            int oldStartingIndex = -1,
            int newStartingIndex = -1) 
        {
            Action = action;
            
            IsSingleItem = isSingleItem;
            OldItem = oldItem;
            NewItem = newItem;
            
            OldItems = oldItems;
            NewItems = newItems;
            
            OldStartingIndex = oldStartingIndex;
            NewStartingIndex = newStartingIndex;
        }
        
        public static NotifyCollectionChangedEventArgs<T> Add(T newItem, int newStartingIndex) =>
            new(NotifyCollectionChangedAction.Add, newItem: newItem, newStartingIndex: newStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Add(IReadOnlyList<T> newItems, int newStartingIndex) =>
            new(NotifyCollectionChangedAction.Add, newItems: newItems, newStartingIndex: newStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Remove(T oldItem, int oldStartingIndex) =>
            new(NotifyCollectionChangedAction.Remove, oldItem, oldStartingIndex: oldStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Remove(IReadOnlyList<T> oldItems, int oldStartingIndex) =>
            new(NotifyCollectionChangedAction.Remove, oldItems, oldStartingIndex: oldStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Replace(T oldItem, T newItem, int index) =>
            new(NotifyCollectionChangedAction.Replace, oldItem, newItem, index, index);

        public static NotifyCollectionChangedEventArgs<T> Replace(IReadOnlyList<T> newItems, IReadOnlyList<T> oldItems, int index) =>
            new(NotifyCollectionChangedAction.Replace, oldItems, newItems, index, index);

        public static NotifyCollectionChangedEventArgs<T> Move(T changedItem, int oldStartingIndex, int newStartingIndex) =>
            new(NotifyCollectionChangedAction.Move, changedItem, changedItem, oldStartingIndex, newStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Reset(IReadOnlyList<T> oldItems) => 
            new(NotifyCollectionChangedAction.Reset, true, oldItems: oldItems);
    }
}