using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that subscribes to an observable or filtered list and forwards
    /// granular change notifications — add, remove, replace, move, and reset — to a target View component.
    /// </summary>
    /// <typeparam name="T">The element type of the list.</typeparam>
    /// <remarks>
    /// Accepts three list variants via overloaded <see cref="SetValue(IReadOnlyObservableList{T})"/> methods:
    /// <list type="bullet">
    ///   <item><see cref="IReadOnlyObservableList{T}"/> — standard observable list with fine-grained events.</item>
    ///   <item><see cref="IReadOnlyFilteredList{T}"/> — filtered view; resets and replays on every change.</item>
    ///   <item><see cref="IReadOnlyList{T}"/> — plain read-only list, bound without change notifications.</item>
    /// </list>
    /// When a filtered or observable list is bound, the binder subscribes to its
    /// <see cref="IReadOnlyObservableList{T}.CollectionChanged"/> event. On unbinding (<see cref="OnUnbound"/>), the subscription
    /// is released and <see cref="OnReset"/> is called.
    /// Subclasses may override <see cref="GetFilterList"/> to wrap an incoming list in a custom
    /// <see cref="IReadOnlyFilteredList{T}"/> before processing.
    /// </remarks>
    public abstract class ObservableListBinder<T> : Binder,
        IBinder<IReadOnlyList<T>>,
        IBinder<IReadOnlyFilteredList<T>>,
        IBinder<IReadOnlyObservableList<T>>
    {
        /// <summary>
        /// Gets the currently bound list (which may be a filtered view), or <see langword="null"/> if unbound.
        /// </summary>
        protected IReadOnlyList<T?>? List { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ObservableListBinder{T}"/> with the specified binding mode.
        /// </summary>
        /// <param name="mode">The binding mode to use.</param>
        protected ObservableListBinder(BindMode mode = BindMode.OneWay)
            : base(mode) { }

        /// <summary>
        /// Binds to a plain read-only list without change-notification support.
        /// </summary>
        /// <param name="list">The list to bind to, or <see langword="null"/> to clear the current binding.</param>
        public void SetValue(IReadOnlyList<T>? list) =>
            InitializeList(list);

        /// <summary>
        /// Binds to a filtered list. Any change to the filter resets and replays the full list.
        /// </summary>
        /// <param name="list">The filtered list to bind to, or <see langword="null"/> to clear the current binding.</param>
        public void SetValue(IReadOnlyFilteredList<T>? list) =>
            InitializeList(list);

        /// <summary>
        /// Binds to an observable list with fine-grained add/remove/replace/move/reset notifications.
        /// </summary>
        /// <param name="list">The observable list to bind to, or <see langword="null"/> to clear the current binding.</param>
        public void SetValue(IReadOnlyObservableList<T>? list) =>
            InitializeList(list);

        /// <summary>
        /// Called after the binder is unbound from the ViewModel.
        /// Releases any change-notification subscriptions on the previously bound list and calls <see cref="OnReset"/>.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeList();

        private void InitializeList(IReadOnlyList<T>? list)
        {
            DeinitializeList();

            List = list;
            if (List is null) return;
            List = GetFilterList(list!) ?? list;

            OnAdded(List, newStartingIndex: 0);

            switch (list)
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
            OnAdded(List, newStartingIndex: 0);
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T?> e)
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
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem, e.OldStartingIndex);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    {
                        OnMove(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Optionally wraps the incoming list in a custom <see cref="IReadOnlyFilteredList{T}"/>.
        /// </summary>
        /// <param name="list">The original list.</param>
        /// <returns>
        /// A filtered view of <paramref name="list"/>, or <see langword="null"/> to use the original list as-is.
        /// </returns>
        protected virtual IReadOnlyFilteredList<T>? GetFilterList(IReadOnlyList<T> list) => null;

        /// <summary>
        /// Called when a single item has been added to the list.
        /// </summary>
        /// <param name="newItem">The item that was added.</param>
        /// <param name="newStartingIndex">The index at which the item was inserted.</param>
        protected abstract void OnAdded(T? newItem, int newStartingIndex);

        /// <summary>
        /// Called when multiple items have been added to the list in a single batch, or on initial population.
        /// </summary>
        /// <param name="newItems">The items that were added.</param>
        /// <param name="newStartingIndex">The starting index at which the items were inserted.</param>
        protected abstract void OnAdded(IReadOnlyList<T?>? newItems, int newStartingIndex);

        /// <summary>
        /// Called when a single item has been removed from the list.
        /// </summary>
        /// <param name="oldItem">The item that was removed.</param>
        /// <param name="oldStartingIndex">The index from which the item was removed.</param>
        protected abstract void OnRemoved(T? oldItem, int oldStartingIndex);

        /// <summary>
        /// Called when multiple items have been removed from the list in a single batch.
        /// </summary>
        /// <param name="oldItems">The items that were removed.</param>
        /// <param name="oldStartingIndex">The starting index from which the items were removed.</param>
        protected abstract void OnRemoved(IReadOnlyList<T?>? oldItems, int oldStartingIndex);

        /// <summary>
        /// Called when an existing item at <paramref name="newStartingIndex"/> has been replaced.
        /// </summary>
        /// <param name="oldItem">The item before replacement.</param>
        /// <param name="newItem">The item after replacement.</param>
        /// <param name="newStartingIndex">The index of the replaced item.</param>
        protected abstract void OnReplace(T? oldItem, T? newItem, int newStartingIndex);

        /// <summary>
        /// Called when an item has moved from one position to another within the list.
        /// </summary>
        /// <param name="oldItem">The item at the old position before the move.</param>
        /// <param name="newItem">The item at the new position after the move.</param>
        /// <param name="oldStartingIndex">The index from which the item was moved.</param>
        /// <param name="newStartingIndex">The index to which the item was moved.</param>
        protected abstract void OnMove(T? oldItem, T? newItem, int oldStartingIndex, int newStartingIndex);

        /// <summary>
        /// Called when the list has been reset and the View representation should be cleared.
        /// </summary>
        protected abstract void OnReset();
    }
}