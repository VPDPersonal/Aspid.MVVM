using System.Collections.Generic;
using Aspid.Collections.Observable;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that receives a read-only collection and reflects it onto a View.
    /// Observable and filtered lists are rebuilt on every change: <see cref="OnReset"/>, then <see cref="OnAdded"/> with the current items.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    public abstract partial class CollectionMonoBinder<T> : MonoBinder, IBinder<IReadOnlyCollection<T>>
    {
        /// <summary>
        /// Gets the bound collection, or <see langword="null"/> when none is set.
        /// </summary>
        protected IReadOnlyCollection<T> Collection { get; private set; }

        /// <summary>
        /// Binds to <paramref name="collection"/>: resets the previous one, then forwards the existing items to <see cref="OnAdded"/>.
        /// </summary>
        /// <param name="collection">The collection to bind, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IReadOnlyCollection<T> collection)
        {
            if (Collection is not null)
                OnReset();

            Unsubscribe();

            Collection = collection;
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);

            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += Rebuild; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += Rebuild; break;
            }
        }

        /// <summary>
        /// Unsubscribes from the bound collection.
        /// </summary>
        protected override void OnUnbound() =>
            Unsubscribe();

        /// <summary>
        /// Called with the whole collection on binding and after every change.
        /// </summary>
        /// <param name="values">The items to show.</param>
        protected abstract void OnAdded(IReadOnlyCollection<T> values);

        /// <summary>
        /// Called before the items are shown again; the View should drop every item.
        /// </summary>
        protected abstract void OnReset();

        private void Rebuild(INotifyCollectionChangedEventArgs<T> e) =>
            Rebuild();

        private void Rebuild()
        {
            OnReset();

            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);
        }

        private void Unsubscribe()
        {
            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= Rebuild; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= Rebuild; break;
            }
        }
    }
}
