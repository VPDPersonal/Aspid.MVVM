#nullable enable
using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that follows an <see cref="IReadOnlyObservableDictionary{TKey,TValue}"/>
    /// and reflects its changes onto a View.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary values.</typeparam>
    /// <remarks>
    /// <see cref="NotifyCollectionChangedAction.Move"/> throws <see cref="NotImplementedException"/>: a dictionary has no order.
    /// </remarks>
    public abstract class ObservableDictionaryBinder<TKey, TValue> : Binder, IBinder<IReadOnlyObservableDictionary<TKey, TValue?>>
    {
        /// <summary>
        /// Gets the bound dictionary, or <see langword="null"/> when none is set.
        /// </summary>
        protected IReadOnlyObservableDictionary<TKey, TValue?>? Dictionary { get; private set; }

        /// <param name="mode">The binding mode.</param>
        protected ObservableDictionaryBinder(BindMode mode = BindMode.OneWay)
            : base(mode) { }

        /// <summary>
        /// Binds to <paramref name="dictionary"/>: resets the previous one, then forwards the existing entries to <see cref="OnAdded(KeyValuePair{TKey, TValue})"/>.
        /// </summary>
        /// <param name="dictionary">The dictionary to bind, or <see langword="null"/> to clear the binding.</param>
        public void SetValue(IReadOnlyObservableDictionary<TKey, TValue?>? dictionary)
        {
            DeinitializeDictionary(Dictionary);
            Dictionary = dictionary;

            if (dictionary is null) return;
            if (dictionary.Count > 0)
            {
                foreach (var pair in dictionary)
                    OnAdded(pair);
            }

            InitializeDictionary(dictionary);
        }

        /// <summary>
        /// Resets the View and unsubscribes from the bound dictionary.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeDictionary(Dictionary);

        /// <summary>
        /// Called when one entry was added.
        /// </summary>
        /// <param name="newItem">The added entry.</param>
        protected abstract void OnAdded(KeyValuePair<TKey, TValue?> newItem);

        /// <summary>
        /// Called when several entries were added at once.
        /// </summary>
        /// <param name="newItems">The added entries.</param>
        protected abstract void OnAdded(IReadOnlyList<KeyValuePair<TKey, TValue?>>? newItems);

        /// <summary>
        /// Called when one entry was removed.
        /// </summary>
        /// <param name="oldItem">The removed entry.</param>
        protected abstract void OnRemoved(KeyValuePair<TKey, TValue?> oldItem);

        /// <summary>
        /// Called when several entries were removed at once.
        /// </summary>
        /// <param name="oldItems">The removed entries.</param>
        protected abstract void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TValue?>>? oldItems);

        /// <summary>
        /// Called when an entry was replaced.
        /// </summary>
        /// <param name="oldItem">The entry before replacement.</param>
        /// <param name="newItem">The entry after replacement.</param>
        protected abstract void OnReplaced(KeyValuePair<TKey, TValue?> oldItem, KeyValuePair<TKey, TValue?> newItem);

        /// <summary>
        /// Called when the dictionary was cleared; the View should drop every entry.
        /// </summary>
        protected abstract void OnReset();

        private void InitializeDictionary(IReadOnlyObservableDictionary<TKey, TValue?> dictionary) =>
            dictionary.CollectionChanged += OnCollectionChanged;

        private void DeinitializeDictionary(IReadOnlyObservableDictionary<TKey, TValue?>? dictionary)
        {
            if (dictionary is null) return;

            OnReset();
            dictionary.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue?>> e)
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
                        if (e.IsSingleItem)
                        {
                            OnReplaced(e.OldItem, e.NewItem);
                        }
                        else if (e.OldItems is not null && e.NewItems is not null)
                        {
                            for (var i = 0; i < e.NewItems.Count; i++)
                                OnReplaced(e.OldItems[i], e.NewItems[i]);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    {
                        OnReset();
                    }
                    break;

                case NotifyCollectionChangedAction.Move: throw new NotImplementedException();
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
