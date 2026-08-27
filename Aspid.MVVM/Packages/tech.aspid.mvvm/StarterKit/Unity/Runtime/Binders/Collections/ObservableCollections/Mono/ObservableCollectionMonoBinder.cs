using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that follows any <see cref="IObservableCollection{T}"/> — a set, a queue or
    /// a stack — and forwards its changes as hook calls.
    /// </summary>
    /// <remarks>
    /// What the collection already holds is replayed when it arrives, so a View built after the data still shows it.
    /// The hooks carry items only, with no index — a set, a queue and a stack have no index that survives a change.
    /// </remarks>
    /// <typeparam name="T">The element type of the bound collection.</typeparam>
    public abstract partial class ObservableCollectionMonoBinder<T> : MonoBinder, IBinder<IObservableCollection<T>>
    {
        /// <summary>
        /// Gets the collection the binder is following, or <see langword="null"/> when none is bound.
        /// </summary>
        protected IObservableCollection<T> Collection { get; private set; }

        /// <summary>
        /// Binds the collection, replaying what it already holds and following it from then on.
        /// </summary>
        /// <param name="value">The collection received from the ViewModel, or <see langword="null"/> to clear the binding.</param>
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
        /// Called when the binding is released. Detaches from the collection and clears everything built from it.
        /// </summary>
        protected override void OnUnbound() =>
            Deinitialize();

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
                        else foreach (var item in e.NewItems) OnAdded(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else foreach (var item in e.OldItems) OnRemoved(item);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (!e.IsSingleItem) goto case NotifyCollectionChangedAction.Reset;

                        OnRemoved(e.OldItem);
                        OnAdded(e.NewItem);
                    }
                    break;

                // No addressable order to update in place, so a move or an unspecified change triggers a full rebuild.
                case NotifyCollectionChangedAction.Move:
                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();

                        if (Collection is null) break;
                        foreach (var item in Collection) OnAdded(item);
                    }
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Called when an item has entered the collection.
        /// </summary>
        /// <param name="newItem">The item that entered.</param>
        protected abstract void OnAdded(T newItem);

        /// <summary>
        /// Called when an item has left the collection.
        /// </summary>
        /// <param name="oldItem">The item that left.</param>
        protected abstract void OnRemoved(T oldItem);

        /// <summary>
        /// Called when everything built from the collection should be cleared.
        /// </summary>
        protected abstract void OnReset();
    }
}
