using UnityEngine;
using UnityEngine.Events;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that reports how many elements a bound collection holds, and whether it
    /// holds none.
    /// </summary>
    /// <remarks>
    /// An observable collection is subscribed to, so the count follows every insert and removal. A plain
    /// <see cref="IReadOnlyList{T}"/> is read once — there is nothing to listen to — and a <see langword="null"/>
    /// collection reports zero and empty rather than nothing at all.
    /// </remarks>
    /// <typeparam name="T">The element type of the bound collection.</typeparam>
    public abstract partial class CollectionCountMonoBinder<T> : MonoBinder,
        IBinder<IReadOnlyObservableList<T>>,
        IBinder<IReadOnlyFilteredList<T>>,
        IBinder<IReadOnlyList<T>>
    {
        [Tooltip("Invoked with the number of elements each time the collection changes.")]
        [SerializeField] private UnityEvent<int> _count;

        [Tooltip("Invoked with whether the collection is empty each time it changes.")]
        [SerializeField] private UnityEvent<bool> _isEmpty;

        private IReadOnlyList<T> _list;

        /// <summary>
        /// Binds an observable list and reports its count, following every change.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> for none.</param>
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> value) =>
            SetList(value);

        /// <summary>
        /// Binds a filtered list and reports its count, following every change.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> for none.</param>
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<T> value) =>
            SetList(value);

        /// <summary>
        /// Binds a plain list and reports its count once.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> for none.</param>
        [BinderLog]
        public void SetValue(IReadOnlyList<T> value) =>
            SetList(value);

        /// <summary>
        /// Called when the binder is unbound. Stops following the collection.
        /// </summary>
        protected override void OnUnbound() =>
            SetList(null);

        private void SetList(IReadOnlyList<T> list)
        {
            Unsubscribe();

            _list = list;

            Subscribe();
            Report();
        }

        private void Subscribe()
        {
            switch (_list)
            {
                // Different delegate shapes per list type; the counter doesn't care what changed, so both call Report.
                case IReadOnlyFilteredList<T> filtered: filtered.CollectionChanged += Report; break;
                case IReadOnlyObservableList<T> observable: observable.CollectionChanged += OnCollectionChanged; break;
            }
        }

        private void Unsubscribe()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<T> filtered: filtered.CollectionChanged -= Report; break;
                case IReadOnlyObservableList<T> observable: observable.CollectionChanged -= OnCollectionChanged; break;
            }
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> args) =>
            Report();

        private void Report()
        {
            var count = _list?.Count ?? 0;

            _count?.Invoke(count);
            _isEmpty?.Invoke(count is 0);
        }
    }
}
