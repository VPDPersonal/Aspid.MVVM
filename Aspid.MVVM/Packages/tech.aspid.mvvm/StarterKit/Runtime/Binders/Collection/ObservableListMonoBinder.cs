using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that follows a plain, observable or filtered list and reflects its
    /// add, remove, replace, move and reset changes onto a View.
    /// </summary>
    /// <typeparam name="T">The element type of the list.</typeparam>
    public abstract partial class ObservableListMonoBinder<T> : MonoBinder,
        IBinder<IReadOnlyList<T>>,
        IBinder<IReadOnlyFilteredList<T>>,
        IBinder<IReadOnlyObservableList<T>>
    {
        /// <summary>
        /// Gets the bound list, possibly wrapped by <see cref="GetFilteredList"/>, or <see langword="null"/> when none is set.
        /// </summary>
        protected IReadOnlyList<T> List { get; private set; }

        /// <summary>
        /// Binds to a plain list; changes to it are not observed.
        /// </summary>
        /// <param name="list">The list to bind, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IReadOnlyList<T> list) =>
            InitializeList(list);

        /// <summary>
        /// Binds to a filtered list; a filter change resets and replays the whole list.
        /// </summary>
        /// <param name="list">The list to bind, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<T> list) =>
            InitializeList(list);

        /// <summary>
        /// Binds to an observable list and follows its granular changes.
        /// </summary>
        /// <param name="list">The list to bind, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> list) =>
            InitializeList(list);

        /// <summary>
        /// Unsubscribes from the bound list and resets the View.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeList();

        /// <summary>
        /// Called on binding to optionally wrap the list in a filtered view. Override to add a filter.
        /// </summary>
        /// <param name="list">The bound list.</param>
        /// <returns>The filtered view, or <see langword="null"/> to use <paramref name="list"/> as-is.</returns>
        protected virtual IReadOnlyFilteredList<T> GetFilteredList(IReadOnlyList<T> list) => null;

        /// <summary>
        /// Called when one item was added.
        /// </summary>
        /// <param name="newItem">The added item.</param>
        /// <param name="index">The index it was inserted at.</param>
        protected abstract void OnAdded(T newItem, int index);

        /// <summary>
        /// Called when several items were added at once, including the whole list on binding.
        /// </summary>
        /// <param name="newItems">The added items.</param>
        /// <param name="index">The index of the first added item.</param>
        protected abstract void OnAdded(IReadOnlyList<T> newItems, int index);

        /// <summary>
        /// Called when one item was removed.
        /// </summary>
        /// <param name="oldItem">The removed item.</param>
        /// <param name="oldStartingIndex">The index it was removed from.</param>
        protected abstract void OnRemoved(T oldItem, int oldStartingIndex);

        /// <summary>
        /// Called when several items were removed at once.
        /// </summary>
        /// <param name="oldItems">The removed items.</param>
        /// <param name="oldStartingIndex">The index of the first removed item.</param>
        protected abstract void OnRemoved(IReadOnlyList<T> oldItems, int oldStartingIndex);

        /// <summary>
        /// Called when the item at <paramref name="index"/> was replaced.
        /// </summary>
        /// <param name="oldItem">The item before replacement.</param>
        /// <param name="newItem">The item after replacement.</param>
        /// <param name="index">The index of the replaced item.</param>
        protected abstract void OnReplaced(T oldItem, T newItem, int index);

        /// <summary>
        /// Called when an item was moved.
        /// </summary>
        /// <param name="oldItem">The item at <paramref name="oldStartingIndex"/> before the move.</param>
        /// <param name="newItem">The item at <paramref name="newStartingIndex"/> after the move.</param>
        /// <param name="oldStartingIndex">The index before the move.</param>
        /// <param name="newStartingIndex">The index after the move.</param>
        protected abstract void OnMoved(T oldItem, T newItem, int oldStartingIndex, int newStartingIndex);

        /// <summary>
        /// Called when the list was cleared or replaced; the View should drop every item.
        /// </summary>
        protected abstract void OnReset();

        private void InitializeList(IReadOnlyList<T> list)
        {
            DeinitializeList();

            List = list;
            if (list is null) return;
            List = GetFilteredList(list) ?? list;

            OnAdded(List, index: 0);

            switch (List)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += OnCollectionChanged; break;
            }
        }

        private void DeinitializeList()
        {
            if (List is null) return;

            switch (List)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= OnCollectionChanged; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= OnCollectionChanged; break;
            }

            List = null;
            OnReset();
        }

        private void OnCollectionChanged()
        {
            OnReset();
            OnAdded(List, index: 0);
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem, e.NewStartingIndex);
                        else OnAdded(e.NewItems, e.NewStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem, e.OldStartingIndex);
                        else OnRemoved(e.OldItems, e.OldStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem)
                        {
                            OnReplaced(e.OldItem, e.NewItem, e.OldStartingIndex);
                        }
                        else if (e.OldItems is not null && e.NewItems is not null)
                        {
                            for (var i = 0; i < e.NewItems.Count; i++)
                                OnReplaced(e.OldItems[i], e.NewItems[i], e.OldStartingIndex + i);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        OnMoved(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
