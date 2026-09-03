using System;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that follows any <see cref="IObservableCollection{T}"/> (a set, a queue, a stack)
    /// and reflects its changes onto a View. The hooks carry items only: these collections have no stable index.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    public abstract partial class ObservableCollectionMonoBinder<T> : MonoBinder, IBinder<IObservableCollection<T>>
    {
        /// <summary>
        /// Gets the bound collection, or <see langword="null"/> when none is set.
        /// </summary>
        protected IObservableCollection<T> Collection { get; private set; }

        /// <summary>
        /// Binds to <paramref name="value"/>: resets the previous one, then forwards the existing items to <see cref="OnAdded"/>.
        /// </summary>
        /// <param name="value">The collection to bind, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IObservableCollection<T> value)
        {
            Deinitialize();

            Collection = value;
            if (Collection is null) return;

            foreach (var item in Collection)
                OnAdded(item);

            Collection.CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Resets the View and unsubscribes from the bound collection.
        /// </summary>
        protected override void OnUnbound() =>
            Deinitialize();

        /// <summary>
        /// Called when an item was added.
        /// </summary>
        /// <param name="newItem">The added item.</param>
        protected abstract void OnAdded(T newItem);

        /// <summary>
        /// Called when an item was removed.
        /// </summary>
        /// <param name="oldItem">The removed item.</param>
        protected abstract void OnRemoved(T oldItem);

        /// <summary>
        /// Called when the collection was cleared or reordered; the View should drop every item.
        /// </summary>
        protected abstract void OnReset();

        private void Deinitialize()
        {
            if (Collection is null) return;

            OnReset();

            Collection.CollectionChanged -= OnCollectionChanged;
            Collection = null;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<T> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem);
                        else if (e.NewItems is not null)
                        {
                            foreach (var item in e.NewItems)
                                OnAdded(item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else if (e.OldItems is not null)
                        {
                            foreach (var item in e.OldItems)
                                OnRemoved(item);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (!e.IsSingleItem) goto case NotifyCollectionChangedAction.Reset;

                        OnRemoved(e.OldItem);
                        OnAdded(e.NewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();

                        if (Collection is null) break;
                        foreach (var item in Collection)
                            OnAdded(item);
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
