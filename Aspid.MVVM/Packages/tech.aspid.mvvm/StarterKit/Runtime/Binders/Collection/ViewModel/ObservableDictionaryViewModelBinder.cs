#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactoryWithKey<Aspid.MVVM.MonoView>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ObservableDictionaryViewModelBinder{TKey, TViewModel, TView}"/> over <see cref="MonoView"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TViewModel">The type of ViewModel stored as values.</typeparam>
    [Serializable]
    public class ObservableDictionaryViewModelBinder<TKey, TViewModel> : ObservableDictionaryViewModelBinder<TKey, TViewModel, MonoView>
        where TViewModel : IViewModel
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected ObservableDictionaryViewModelBinder() { }

        /// <inheritdoc/>
        public ObservableDictionaryViewModelBinder(ViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableDictionaryBinder{TKey, TValue}"/> that creates a view per entry through a keyed factory
    /// and releases it when the entry leaves. A replacement releases the old view and creates a new one.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary keys.</typeparam>
    /// <typeparam name="TViewModel">The type of ViewModel stored as values.</typeparam>
    /// <typeparam name="TView">The type of view created per entry.</typeparam>
    [Serializable]
    public class ObservableDictionaryViewModelBinder<TKey, TViewModel, TView> : ObservableDictionaryBinder<TKey, TViewModel>
        where TViewModel : IViewModel
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Creates a view per entry by key.")]
        [SerializeReference] private IViewFactoryWithKey<TView>? _viewFactory;

        private Dictionary<TKey, TView>? _views;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected ObservableDictionaryViewModelBinder() { }

        /// <param name="viewFactory">The factory that creates and releases views by key.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewFactory"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public ObservableDictionaryViewModelBinder(IViewFactoryWithKey<TView> viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        private Dictionary<TKey, TView> Views => _views ??= new Dictionary<TKey, TView>();

        /// <inheritdoc/>
        protected sealed override void OnAdded(KeyValuePair<TKey, TViewModel?> newItem)
        {
            if (newItem.Value is null) return;
            if (Views.ContainsKey(newItem.Key)) return;

            Views.Add(newItem.Key, _viewFactory.Create(newItem.Value, newItem.Key));
        }

        /// <inheritdoc/>
        protected sealed override void OnAdded(IReadOnlyList<KeyValuePair<TKey, TViewModel?>>? newItems)
        {
            if (newItems is null) return;

            foreach (var item in newItems)
                OnAdded(item);
        }

        /// <inheritdoc/>
        protected sealed override void OnRemoved(KeyValuePair<TKey, TViewModel?> oldItem)
        {
            if (!Views.TryGetValue(oldItem.Key, out var view)) return;

            view.Deinitialize();
            Views.Remove(oldItem.Key);
            _viewFactory.Release(view);
        }

        /// <inheritdoc/>
        protected sealed override void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TViewModel?>>? oldItems)
        {
            if (oldItems is null) return;

            foreach (var item in oldItems)
                OnRemoved(item);
        }

        /// <inheritdoc/>
        protected sealed override void OnReplaced(KeyValuePair<TKey, TViewModel?> oldItem, KeyValuePair<TKey, TViewModel?> newItem)
        {
            OnRemoved(oldItem);
            OnAdded(newItem);
        }

        /// <inheritdoc/>
        protected sealed override void OnReset()
        {
            if (_views is null) return;

            foreach (var view in _views.Values)
            {
                view.Deinitialize();
                _viewFactory.Release(view);
            }

            _views.Clear();
        }
    }
}
