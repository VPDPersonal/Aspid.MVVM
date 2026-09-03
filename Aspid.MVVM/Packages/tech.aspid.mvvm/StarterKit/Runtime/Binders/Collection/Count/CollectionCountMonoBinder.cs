using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that reports how many items a bound collection holds and whether it is empty.
    /// Observable and filtered lists are followed; a plain list is read once; <see langword="null"/> reports zero.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    public abstract partial class CollectionCountMonoBinder<T> : MonoBinder,
        IBinder<IReadOnlyList<T>>,
        IBinder<IReadOnlyFilteredList<T>>,
        IBinder<IReadOnlyObservableList<T>>
    {
        [Tooltip("Invoked with the item count on every change.")]
        [SerializeField] private UnityEvent<int> _count;

        [Tooltip("Invoked with whether the collection is empty on every change.")]
        [SerializeField] private UnityEvent<bool> _isEmpty;

        private IReadOnlyList<T> _list;

        /// <summary>
        /// Binds to a plain list and reports its count once.
        /// </summary>
        /// <param name="value">The list to bind, or <see langword="null"/> to report zero.</param>
        [BinderLog]
        public void SetValue(IReadOnlyList<T> value) =>
            SetList(value);

        /// <summary>
        /// Binds to a filtered list and follows its count.
        /// </summary>
        /// <param name="value">The list to bind, or <see langword="null"/> to report zero.</param>
        [BinderLog]
        public void SetValue(IReadOnlyFilteredList<T> value) =>
            SetList(value);

        /// <summary>
        /// Binds to an observable list and follows its count.
        /// </summary>
        /// <param name="value">The list to bind, or <see langword="null"/> to report zero.</param>
        [BinderLog]
        public void SetValue(IReadOnlyObservableList<T> value) =>
            SetList(value);

        /// <summary>
        /// Unsubscribes from the bound list and reports zero.
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
                case IReadOnlyFilteredList<T> filtered: filtered.CollectionChanged += Report; break;
                case IReadOnlyObservableList<T> observable: observable.CollectionChanged += Report; break;
            }
        }

        private void Unsubscribe()
        {
            switch (_list)
            {
                case IReadOnlyFilteredList<T> filtered: filtered.CollectionChanged -= Report; break;
                case IReadOnlyObservableList<T> observable: observable.CollectionChanged -= Report; break;
            }
        }

        private void Report(INotifyCollectionChangedEventArgs<T> e) =>
            Report();

        private void Report()
        {
            var count = _list?.Count ?? 0;

            _count?.Invoke(count);
            _isEmpty?.Invoke(count is 0);
        }
    }
}
