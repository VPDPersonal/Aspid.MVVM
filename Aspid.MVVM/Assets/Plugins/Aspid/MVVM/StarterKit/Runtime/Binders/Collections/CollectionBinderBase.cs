using System;
using System.Collections.Generic;

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

            Collection = collection;
            if (Collection is null) return;
            if (Collection.Count > 0) OnAdded(Collection);
        }

        /// <summary>
        /// Called when one or more items have been added to the collection and need to be reflected in the View.
        /// </summary>
        /// <param name="values">The items that were added.</param>
        protected abstract void OnAdded(IReadOnlyCollection<T> values);

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