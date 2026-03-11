using System;
using System.Collections.Generic;
using Aspid.Collections.Observable;
using System.Collections.Specialized;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that subscribes to an <see cref="IReadOnlyObservableDictionary{TKey,TValue}"/>
    /// and forwards granular change notifications to a target View component.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the observable dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the observable dictionary.</typeparam>
    /// <remarks>
    /// On binding, existing entries are replayed through <see cref="OnAdded(KeyValuePair{TKey, TValue?})"/>
    /// so that the initial state is reflected in the View.
    /// Subsequent <see cref="NotifyCollectionChangedAction.Add"/>,
    /// <see cref="NotifyCollectionChangedAction.Remove"/>,
    /// <see cref="NotifyCollectionChangedAction.Replace"/>, and
    /// <see cref="NotifyCollectionChangedAction.Reset"/> events are dispatched to the corresponding
    /// abstract hook methods.
    /// <see cref="NotifyCollectionChangedAction.Move"/> is not supported and will throw
    /// <see cref="NotImplementedException"/>.
    /// When the binder is unbound, the dictionary subscription is released and <see cref="OnReset"/> is called.
    /// </remarks>
    public abstract class ObservableDictionaryBinder<TKey, TValue> : Binder, IBinder<IReadOnlyObservableDictionary<TKey, TValue?>>
    {
        /// <summary>
        /// Gets the currently bound observable dictionary, or <see langword="null"/> if no dictionary is set.
        /// </summary>
        protected IReadOnlyObservableDictionary<TKey, TValue?>? Dictionary { get; private set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ObservableDictionaryBinder{TKey,TValue}"/>
        /// with the specified binding mode.
        /// </summary>
        /// <param name="mode">The binding mode to use.</param>
        protected ObservableDictionaryBinder(BindMode mode)
            : base(mode) { }

        /// <summary>
        /// Binds to <paramref name="dictionary"/>, unsubscribing from any previously bound dictionary first.
        /// Existing entries are replayed and a collection-changed subscription is established.
        /// </summary>
        /// <param name="dictionary">
        /// The observable dictionary to bind to, or <see langword="null"/> to clear the current binding.
        /// </param>
        public void SetValue(IReadOnlyObservableDictionary<TKey, TValue?>? dictionary)
        {
            DeinitializeDictionary();

            Dictionary = dictionary;

            if (dictionary is null) return;
            if (dictionary.Count > 0)
            {
                foreach (var pair in dictionary)
                    OnAdded(pair);
            }

            InitializeDictionary();
        }

        /// <summary>
        /// Called after the binder is unbound from the ViewModel.
        /// Releases the change-notification subscription on the previously bound dictionary and calls <see cref="OnReset"/>.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeDictionary();

        private void InitializeDictionary() =>
            Dictionary!.CollectionChanged += OnCollectionChanged;

        private void DeinitializeDictionary()
        {
            if (Dictionary is null) return;

            OnReset();
            Dictionary!.CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(INotifyCollectionChangedEventArgs<KeyValuePair<TKey, TValue?>> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        if (e.IsSingleItem) OnAdded(e.NewItem);
                        else OnAdded(e.NewItems!);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    {
                        if (e.IsSingleItem) OnRemoved(e.OldItem);
                        else OnRemoved(e.OldItems!);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    {
                        if (e.IsSingleItem) OnReplace(e.OldItem, e.NewItem);
                        else throw new NotImplementedException();
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

        /// <summary>
        /// Called when a single key-value pair has been added to the dictionary.
        /// </summary>
        /// <param name="newItem">The newly added key-value pair.</param>
        protected abstract void OnAdded(KeyValuePair<TKey, TValue?> newItem);

        /// <summary>
        /// Called when multiple key-value pairs have been added to the dictionary in a single batch.
        /// </summary>
        /// <param name="newItems">The list of newly added key-value pairs.</param>
        protected abstract void OnAdded(IReadOnlyList<KeyValuePair<TKey, TValue?>> newItems);

        /// <summary>
        /// Called when a single key-value pair has been removed from the dictionary.
        /// </summary>
        /// <param name="oldItem">The key-value pair that was removed.</param>
        protected abstract void OnRemoved(KeyValuePair<TKey, TValue?> oldItem);

        /// <summary>
        /// Called when multiple key-value pairs have been removed from the dictionary in a single batch.
        /// </summary>
        /// <param name="oldItems">The list of key-value pairs that were removed.</param>
        protected abstract void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TValue?>> oldItems);

        /// <summary>
        /// Called when an existing key-value pair has been replaced by a new one.
        /// </summary>
        /// <param name="oldItem">The key-value pair before replacement.</param>
        /// <param name="newItem">The key-value pair after replacement.</param>
        protected abstract void OnReplace(KeyValuePair<TKey, TValue?> oldItem, KeyValuePair<TKey, TValue?> newItem);

        /// <summary>
        /// Called when the dictionary has been reset and the View representation should be cleared.
        /// </summary>
        protected abstract void OnReset();
    }
}