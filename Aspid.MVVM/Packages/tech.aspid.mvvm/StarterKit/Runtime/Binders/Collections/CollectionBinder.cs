#nullable enable
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that receives a read-only collection and reflects its changes onto a View.
    /// Observable and filtered lists are followed through their change notifications.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    public abstract class CollectionBinder<T> : Binder, IBinder<IReadOnlyCollection<T>>
    {
        /// <summary>
        /// Gets the bound collection, or <see langword="null"/> when none is set.
        /// </summary>
        protected IReadOnlyCollection<T>? Collection { get; private set; }

        /// <param name="mode">The binding mode.</param>
        protected CollectionBinder(BindMode mode = BindMode.OneWay)
            : base(mode) { }

        /// <summary>
        /// Binds to <paramref name="collection"/>: resets the previous one, then forwards the existing items to <see cref="OnAdded(IReadOnlyCollection{T})"/>.
        /// </summary>
        /// <param name="collection">The collection to bind, or <see langword="null"/> to clear the binding.</param>
        public void SetValue(IReadOnlyCollection<T>? collection)
        {
            if (Collection is not null)
                OnReset();

            UnsubscribeFromCollection();

            Collection = collection;
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);

            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
        }
        
        /// <summary>
        /// Unsubscribes from the bound collection.
        /// </summary>
        protected override void OnUnbound() =>
            UnsubscribeFromCollection();

        /// <summary>
        /// Called with the whole collection on binding and after a filter reset.
        /// </summary>
        /// <param name="values">The items to show.</param>
        protected abstract void OnAdded(IReadOnlyCollection<T>? values);

        /// <summary>
        /// Called when one item was added.
        /// </summary>
        /// <param name="newItem">The added item.</param>
        protected abstract void OnAdded(T? newItem);

        /// <summary>
        /// Called when several items were added at once.
        /// </summary>
        /// <param name="newItems">The added items.</param>
        protected abstract void OnAdded(IReadOnlyList<T?>? newItems);

        /// <summary>
        /// Called when one item was removed.
        /// </summary>
        /// <param name="oldItem">The removed item.</param>
        protected abstract void OnRemoved(T? oldItem);

        /// <summary>
        /// Called when several items were removed at once.
        /// </summary>
        /// <param name="oldItems">The removed items.</param>
        protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems);

        /// <summary>
        /// Called when the item at <paramref name="index"/> was replaced.
        /// </summary>
        /// <param name="oldItem">The item before replacement.</param>
        /// <param name="newItem">The item after replacement.</param>
        /// <param name="index">The index of the replaced item.</param>
        protected abstract void OnReplaced(T? oldItem, T? newItem, int index);

        /// <summary>
        /// Called when an item was moved.
        /// </summary>
        /// <param name="oldItem">The item at <paramref name="oldStartingIndex"/> before the move.</param>
        /// <param name="newItem">The item at <paramref name="newStartingIndex"/> after the move.</param>
        /// <param name="oldStartingIndex">The index before the move.</param>
        /// <param name="newStartingIndex">The index after the move.</param>
        protected abstract void OnMoved(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex);

        /// <summary>
        /// Called when the collection was cleared or replaced; the View should drop every item.
        /// </summary>
        protected abstract void OnReset();

        private void UnsubscribeFromCollection()
        {
            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= OnCollectionChanged; break;
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
                        else OnAdded(e.NewItems);
                    } break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else OnRemoved(e.OldItems);
                    } break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    } break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem)
                        {
                            OnReplaced(e.OldItem, e.NewItem, e.OldStartingIndex);
                        }
                        else if (e.OldItems is not null && e.NewItems is not null)
                        {
                            var oldItems = e.OldItems;
                            var newItems = e.NewItems;
                            var startIndex = e.OldStartingIndex;

                            for (var i = 0; i < newItems.Count; i++)
                                OnReplaced(oldItems[i], newItems[i], startIndex + i);
                        }
                    } break;

                case NotifyCollectionChangedAction.Move:
                    {
                        OnMoved(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    } break;
            }
        }
    }
}
