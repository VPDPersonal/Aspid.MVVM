using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that binds a <see cref="IReadOnlyList{T}"/>,
    /// an <see cref="IReadOnlyObservableList{T}"/>, or an <see cref="IReadOnlyFilteredList{T}"/>, and forwards its
    /// changes as hook calls.
    /// </summary>
    /// <typeparam name="T">The element type of the list.</typeparam>
    /// <remarks>
    /// A plain <see cref="IReadOnlyList{T}"/> is bound once and never updates; the observable and filtered variants
    /// additionally subscribe to <c>CollectionChanged</c> and dispatch add/remove/replace/move/reset hooks.
    /// </remarks>
    public abstract partial class ObservableListMonoBinder<T> : MonoBinder,
        IBinder<IReadOnlyObservableList<T>>, IBinder<IReadOnlyFilteredList<T>>, IBinder<IReadOnlyList<T>>
    {
        /// <summary>
        /// Gets the list the binder is following, or <see langword="null"/> when none is bound.
        /// </summary>
        protected IReadOnlyList<T> List { get; private set; }

        /// <summary>
        /// Binds the list, replaying what it already holds and following it from then on if it raises changes.
        /// </summary>
        /// <param name="list">The list received from the ViewModel, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IReadOnlyList<T> list) =>
            InitializeList(list);

        /// <inheritdoc cref="SetValue(IReadOnlyList{T})"/>
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<T> list) =>
            InitializeList(list);

        /// <inheritdoc cref="SetValue(IReadOnlyList{T})"/>
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> list) =>
            InitializeList(list);

        /// <summary>
        /// Called when the binding is released. Detaches from the list and clears everything built from it.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeList();

        private void InitializeList(IReadOnlyList<T> list)
        {
            DeinitializeList();

            List = list;
            if (List is null) return;
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
                        if (e.IsSingleItem) OnReplaced(e.OldItem, e.NewItem, e.OldStartingIndex);
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
                        OnMoved(e.OldItem, e.NewItem, e.OldStartingIndex, e.NewStartingIndex);
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Called while initializing a bound list. Override to wrap it in a filtered or sorted view.
        /// </summary>
        /// <param name="list">The list that was just bound.</param>
        /// <returns>The filtered list to follow instead, or <see langword="null"/> to follow <paramref name="list"/> as is.</returns>
        protected virtual IReadOnlyFilteredList<T> GetFilteredList(IReadOnlyList<T> list) => null;

        /// <summary>
        /// Called when one item has been added.
        /// </summary>
        /// <param name="newItem">The item that was added.</param>
        /// <param name="index">The index the item was added at.</param>
        protected abstract void OnAdded(T newItem, int index);

        /// <summary>
        /// Called when several items have been added at once.
        /// </summary>
        /// <param name="newItems">The items that were added.</param>
        /// <param name="index">The index the first item was added at.</param>
        protected abstract void OnAdded(IReadOnlyList<T> newItems, int index);

        /// <summary>
        /// Called when one item has been removed.
        /// </summary>
        /// <param name="oldItem">The item that was removed.</param>
        /// <param name="oldStartingIndex">The index the item was removed from.</param>
        protected abstract void OnRemoved(T oldItem, int oldStartingIndex);

        /// <summary>
        /// Called when several items have been removed at once.
        /// </summary>
        /// <param name="oldItems">The items that were removed.</param>
        /// <param name="oldStartingIndex">The index the first item was removed from.</param>
        protected abstract void OnRemoved(IReadOnlyList<T> oldItems, int oldStartingIndex);

        /// <summary>
        /// Called when an item has been replaced by another at the same index.
        /// </summary>
        /// <param name="oldItem">The item before the replacement.</param>
        /// <param name="newItem">The item after it.</param>
        /// <param name="index">The index of the replaced item.</param>
        protected abstract void OnReplaced(T oldItem, T newItem, int index);

        /// <summary>
        /// Called when an item has moved to a different index.
        /// </summary>
        /// <param name="oldItem">The item before the move.</param>
        /// <param name="newItem">The item after it.</param>
        /// <param name="oldStartingIndex">The index the item moved from.</param>
        /// <param name="newStartingIndex">The index the item moved to.</param>
        protected abstract void OnMoved(T oldItem, T newItem, int oldStartingIndex, int newStartingIndex);

        /// <summary>
        /// Called when everything built from the list should be cleared.
        /// </summary>
        protected abstract void OnReset();
    }
}