#nullable enable
using Aspid.Collections.Observable;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that receives a read-only collection of <typeparamref name="T"/> items
    /// and reflects its contents onto a target View component.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <remarks>
    /// When the bound collection raises <c>CollectionChanged</c>, the View is rebuilt: <see cref="OnReset"/> followed
    /// by <see cref="OnAdded"/> for the current contents. The serializable twin
    /// <see cref="CollectionBinderBase{T}"/> additionally exposes granular add/remove/replace/move hooks; this class
    /// deliberately keeps the coarse rebuild so that adding change tracking does not force a breaking change on
    /// existing subclasses.
    /// </remarks>
    public abstract partial class CollectionMonoBinder<T> : MonoBinder, IBinder<IReadOnlyCollection<T>>
    {
        /// <summary>
        /// Gets the currently bound collection, or <see langword="null"/> if no collection is set.
        /// </summary>
        protected IReadOnlyCollection<T>? Collection { get; private set; }

        /// <summary>
        /// Binds to <paramref name="collection"/>, resetting any previously bound collection first and subscribing
        /// to its change notifications when it provides them.
        /// </summary>
        /// <param name="collection">
        /// The new collection to bind to, or <see langword="null"/> to clear the current binding.
        /// </param>
        [BinderLog]
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
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged += Rebuild; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged += Rebuild; break;
            }
        }

        /// <summary>
        /// Called after unbinding. Unsubscribes from the bound collection to prevent handler leaks.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnUnbound()</c> to preserve unsubscription.
        /// </remarks>
        protected override void OnUnbound() =>
            UnsubscribeFromCollection();

        /// <summary>
        /// Called when one or more items must be reflected in the View.
        /// </summary>
        /// <param name="values">The items currently in the collection.</param>
        protected abstract void OnAdded(IReadOnlyCollection<T> values);

        /// <summary>
        /// Called when the View representation must be cleared.
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

        private void UnsubscribeFromCollection()
        {
            switch (Collection)
            {
                case IReadOnlyFilteredList<T> filteredList: filteredList.CollectionChanged -= Rebuild; break;
                case IReadOnlyObservableList<T> observableList: observableList.CollectionChanged -= Rebuild; break;
            }
        }
    }
}
