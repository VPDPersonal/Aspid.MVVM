using System;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Collections
{
    public readonly ref struct NotifyCollectionChangedEventArgs<T>
    {
        public readonly NotifyCollectionChangedAction Action;

        public readonly bool IsSingleItem;
        public readonly T OldItem;
        public readonly T NewItem;

        public readonly ReadOnlySpan<T> OldItems;
        public readonly ReadOnlySpan<T> NewItems;

        public readonly int OldStartingIndex;
        public readonly int NewStartingIndex;

        public NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction action,
            T oldItem = default!,
            T newItem = default!,
            int oldStartingIndex = -1,
            int newStartingIndex = -1)
            : this(action, true, oldItem, newItem, default, default, oldStartingIndex, newStartingIndex) { }
        
        public NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction action,
            ReadOnlySpan<T> oldItems = default, 
            ReadOnlySpan<T> newItems = default, 
            int oldStartingIndex = -1,
            int newStartingIndex = -1) 
            : this(action, false, default!, default!, oldItems, newItems, oldStartingIndex, newStartingIndex) { }
        
        public NotifyCollectionChangedEventArgs(
            NotifyCollectionChangedAction action,
            bool isSingleItem,
            T oldItem = default!, 
            T newItem = default!,
            ReadOnlySpan<T> oldItems = default, 
            ReadOnlySpan<T> newItems = default, 
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

        public static NotifyCollectionChangedEventArgs<T> Add(ReadOnlySpan<T> newItems, int newStartingIndex) =>
            new(NotifyCollectionChangedAction.Add, newItems: newItems, newStartingIndex: newStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Remove(T oldItem, int oldStartingIndex) =>
            new(NotifyCollectionChangedAction.Remove, oldItem, oldStartingIndex: oldStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Remove(ReadOnlySpan<T> oldItems, int oldStartingIndex) =>
            new(NotifyCollectionChangedAction.Remove, oldItems, oldStartingIndex: oldStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Replace(T oldItem, T newItem, int index) =>
            new(NotifyCollectionChangedAction.Replace, oldItem, newItem, index, index);

        public static NotifyCollectionChangedEventArgs<T> Replace(ReadOnlySpan<T> newItems, ReadOnlySpan<T> oldItems, int index) =>
            new(NotifyCollectionChangedAction.Replace, oldItems, newItems, index, index);

        public static NotifyCollectionChangedEventArgs<T> Move(T changedItem, int oldStartingIndex, int newStartingIndex) =>
            new(NotifyCollectionChangedAction.Move, changedItem, changedItem, oldStartingIndex, newStartingIndex);

        public static NotifyCollectionChangedEventArgs<T> Reset() => 
            new(NotifyCollectionChangedAction.Reset, true);
    }
}