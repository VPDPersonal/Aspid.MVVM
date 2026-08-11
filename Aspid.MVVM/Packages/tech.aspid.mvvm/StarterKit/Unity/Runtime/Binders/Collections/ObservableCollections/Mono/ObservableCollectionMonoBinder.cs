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
    /// The package bound observable lists and dictionaries and left the other three types of the collections library
    /// unbound, though a set of owned ids, a queue of pending requests and a stack of open screens are all things a View
    /// shows. One binder covers all three because that is exactly what they have in common: membership that changes, and
    /// no index worth binding to.
    /// <para/>
    /// Deliberately not built on the list binder. A list binder's hooks carry an index, and none of these three has one
    /// that survives a change — a queue renumbers on every dequeue. The hooks here carry items only, which is the honest
    /// contract for a set, and it is why a reset is the answer to any change a collection reports without detail.
    /// <para/>
    /// What the collection already holds is replayed when it arrives, so a View built after the data still shows it.
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

                // Порядок в множестве, очереди и стеке не адресуется, поэтому перемещение и любое
                // изменение без деталей сводятся к пересбору: это честнее, чем угадывать индексы.
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
