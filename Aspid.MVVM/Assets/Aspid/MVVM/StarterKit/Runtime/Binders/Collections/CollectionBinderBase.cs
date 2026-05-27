using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that receives a read-only collection of <typeparamref name="T"/> items
    /// and reflects add/reset operations onto a target View component.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <remarks>
    /// When a new collection is assigned via <see cref="SetValue"/>, the previously held collection is reset
    /// first via <see cref="OnReset"/>, then the new items are passed to <see cref="OnAdded"/> if the collection
    /// is non-empty.
    /// The class also implements <see cref="IDisposable"/>; disposing it calls <see cref="OnReset"/>.
    /// </remarks>
    public abstract class CollectionBinderBase<T> : Binder, 
        IBinder<IReadOnlyCollection<T>>,
        IDisposable
    {
        /// <summary>
        /// Gets the currently bound collection, or <see langword="null"/> if no collection is set.
        /// </summary>
        protected IReadOnlyCollection<T>? Collection { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="CollectionBinderBase{T}"/> with the specified binding mode.
        /// </summary>
        /// <param name="mode">The binding mode to use.</param>
        protected CollectionBinderBase(BindMode mode = BindMode.OneWay)
            : base(mode) { }

        /// <summary>
        /// Binds to <paramref name="collection"/>, resetting any previously bound collection first.
        /// Items already present in the new collection are immediately forwarded to <see cref="OnAdded"/>.
        /// </summary>
        /// <param name="collection">
        /// The new collection to bind to, or <see langword="null"/> to clear the current binding.
        /// </param>
        public void SetValue(IReadOnlyCollection<T>? collection)
        {
            if (Collection is not null)
                OnReset();
            
            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= OnCollectionChanged; break;
            }
            
            Collection = collection;
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);
            
            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
        }
        
        private void OnCollectionChanged()
        {
            OnReset();
            
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);
        }
        
        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T?> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem);
                        else OnAdded(e.NewItems!);
                    } break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else OnRemoved(e.OldItems!);
                    } break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    } break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem)
                        {
                            OnReplace(e.OldItem, e.NewItem, e.OldStartingIndex);
                        }
                        else if (e.OldItems is not null && e.NewItems is not null)
                        {
                            var oldItems = e.OldItems;
                            var newItems = e.NewItems;
                            var startIndex = e.OldStartingIndex;

                            for (var i = 0; i < newItems.Count; i++)
                                OnReplace(oldItems[i], newItems[i], startIndex + i);
                        }
                    } break;
                
                case NotifyCollectionChangedAction.Move:
                    {
                        OnMove(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    } break;
            }
        }

        /// <summary>
        /// Called when one or more items have been added to the collection and need to be reflected in the View.
        /// </summary>
        /// <param name="values">The items that were added.</param>
        protected abstract void OnAdded(IReadOnlyCollection<T> values);

        /// <summary>
        /// Called when a single item has been added to the collection via a granular change notification.
        /// </summary>
        /// <param name="newItem">The item that was added, or <see langword="null"/> if the slot was empty.</param>
        protected abstract void OnAdded(T? newItem);

        /// <summary>
        /// Called when multiple items have been added to the collection in a single batch via a granular change notification.
        /// </summary>
        /// <param name="newItems">The items that were added.</param>
        protected abstract void OnAdded(IReadOnlyList<T?> newItems);

        /// <summary>
        /// Called when a single item has been removed from the collection via a granular change notification.
        /// </summary>
        /// <param name="oldItem">The item that was removed, or <see langword="null"/> if the slot was empty.</param>
        protected abstract void OnRemoved(T? oldItem);

        /// <summary>
        /// Called when multiple items have been removed from the collection in a single batch via a granular change notification.
        /// </summary>
        /// <param name="oldItems">The items that were removed.</param>
        protected abstract void OnRemoved(IReadOnlyList<T?> oldItems);

        /// <summary>
        /// Called when a single item in the bound collection has been replaced and the change
        /// must be reflected in the View.
        /// </summary>
        /// <param name="oldItem">The item that was replaced, or <see langword="null"/> if the previous slot was empty.</param>
        /// <param name="newItem">The replacement item, or <see langword="null"/> if the slot is now empty.</param>
        /// <param name="newStartingIndex">The index of the replaced item in the collection.</param>
        protected abstract void OnReplace(T? oldItem, T? newItem, int newStartingIndex);

        /// <summary>
        /// Called when an item in the bound collection has been moved from one index to another and
        /// the change must be reflected in the View.
        /// </summary>
        /// <param name="oldItem">The item at <paramref name="oldStartingIndex"/> before the move.</param>
        /// <param name="newItem">The item at <paramref name="newStartingIndex"/> after the move.</param>
        /// <param name="oldStartingIndex">The index of the item before the move.</param>
        /// <param name="newStartingIndex">The index of the item after the move.</param>
        protected abstract void OnMove(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex);

        /// <summary>
        /// Called when the collection is cleared or replaced, signaling that the View representation should be reset.
        /// </summary>
        protected abstract void OnReset();

        /// <summary>
        /// Resets the binder by calling <see cref="OnReset"/>.
        /// </summary>
        public virtual void Dispose() =>
            OnReset();
    }
}