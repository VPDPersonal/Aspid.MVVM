using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that follows an
    /// <see cref="IReadOnlyObservableDictionary{TKey, TValue}"/> and forwards its changes as hook calls.
    /// </summary>
    /// <remarks>
    /// The list domain had both a serializable binder and a MonoBehaviour one; the dictionary domain had only the
    /// serializable one, so a dictionary could be shown from a View's own field and not from a component dropped next to
    /// the objects it drives. This is that missing half, and it dispatches exactly what
    /// <see cref="ObservableDictionaryBinder{TKey, TValue}"/> dispatches.
    /// <para/>
    /// Existing entries are replayed through the add hook when the dictionary arrives, so a View built after the data
    /// shows it. <see cref="NotifyCollectionChangedAction.Move"/> is not supported: a dictionary has no order to move
    /// within, and the collection raises it only for a list.
    /// </remarks>
    /// <typeparam name="TKey">The type of the dictionary's keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary's values.</typeparam>
    public abstract partial class ObservableDictionaryMonoBinder<TKey, TValue> : MonoBinder,
        IBinder<IReadOnlyObservableDictionary<TKey, TValue>>
    {
        /// <summary>
        /// Gets the dictionary the binder is following, or <see langword="null"/> when none is bound.
        /// </summary>
        protected IReadOnlyObservableDictionary<TKey, TValue> Dictionary { get; private set; }

        /// <summary>
        /// Binds the dictionary, replaying what it already holds and following it from then on.
        /// </summary>
        /// <param name="value">The dictionary received from the ViewModel, or <see langword="null"/> to clear the binding.</param>
        [BinderLog]
        public void SetValue(IReadOnlyObservableDictionary<TKey, TValue> value)
        {
            Deinitialize();

            Dictionary = value;
            if (Dictionary is null) return;

            foreach (var pair in Dictionary)
                OnAdded(pair);

            Dictionary.CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Called when the binding is released. Detaches from the dictionary and clears everything built from it.
        /// </summary>
        protected override void OnUnbound() =>
            Deinitialize();

        private void Deinitialize()
        {
            if (Dictionary is null) return;

            OnReset();

            Dictionary.CollectionChanged -= OnCollectionChanged;
            Dictionary = null;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue>> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem);
                        else OnAdded(e.NewItems);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else OnRemoved(e.OldItems);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem);
                        else throw new NotImplementedException();
                    }
                    break;

                case NotifyCollectionChangedAction.Reset: OnReset(); break;
                case NotifyCollectionChangedAction.Move: throw new NotImplementedException();

                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Called when one pair has been added.
        /// </summary>
        /// <param name="newItem">The pair that was added.</param>
        protected abstract void OnAdded(KeyValuePair<TKey, TValue> newItem);

        /// <summary>
        /// Called when several pairs have been added at once.
        /// </summary>
        /// <param name="newItems">The pairs that were added.</param>
        protected abstract void OnAdded(IReadOnlyList<KeyValuePair<TKey, TValue>> newItems);

        /// <summary>
        /// Called when one pair has been removed.
        /// </summary>
        /// <param name="oldItem">The pair that was removed.</param>
        protected abstract void OnRemoved(KeyValuePair<TKey, TValue> oldItem);

        /// <summary>
        /// Called when several pairs have been removed at once.
        /// </summary>
        /// <param name="oldItems">The pairs that were removed.</param>
        protected abstract void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TValue>> oldItems);

        /// <summary>
        /// Called when a pair has been replaced by another under the same key.
        /// </summary>
        /// <param name="oldItem">The pair before the replacement.</param>
        /// <param name="newItem">The pair after it.</param>
        protected abstract void OnReplace(KeyValuePair<TKey, TValue> oldItem, KeyValuePair<TKey, TValue> newItem);

        /// <summary>
        /// Called when everything built from the dictionary should be cleared.
        /// </summary>
        protected abstract void OnReset();
    }
}
