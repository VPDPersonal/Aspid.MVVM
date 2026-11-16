#if ASPID_MVVM_HAS_ASPID_COLLECTIONS
#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ObservableListViewModelBinder{TView}"/> over <see cref="MonoView"/>.
    /// </summary>
    [Serializable]
    public class ObservableListViewModelBinder : ObservableListViewModelBinder<MonoView>
    {
        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected ObservableListViewModelBinder() { }

        /// <inheritdoc/>
        public ObservableListViewModelBinder(IViewFactory<MonoView> viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableListBinder{T}"/> that creates a view per list slot, with an optional filter and sort order.
    /// </summary>
    /// <remarks>A <see langword="null"/> item retains a deinitialized, inactive view at its index.</remarks>
    /// <typeparam name="TView">The type of view created per item.</typeparam>
    [Serializable]
    public class ObservableListViewModelBinder<TView> : ObservableListBinder<IViewModel>
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Creates a view per item.")]
        [SerializeReference] private IViewFactory<TView>? _viewFactory;

        [Tooltip("Optional filter; empty shows every item.")]
        [SerializeReference] private ICollectionFilter<IViewModel>? _filter;

        [Tooltip("Optional sort order; empty keeps the list order.")]
        [SerializeReference] private ICollectionOrder<IViewModel>? _order;

        private List<TView>? _views;
        private IViewFactory<TView>? _activeFactory;
        private FilteredList<IViewModel>? _filteredList;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected ObservableListViewModelBinder() { }

        /// <param name="viewFactory">The factory that creates and releases views.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewFactory"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The mode is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public ObservableListViewModelBinder(IViewFactory<TView> viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.EnsureForwardOnly();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        private List<TView> Views => _views ??= new List<TView>();

        private IViewFactory<TView> Factory =>
            _activeFactory ??= _viewFactory ?? throw new InvalidOperationException("No view factory is assigned.");

        /// <inheritdoc/>
        protected override void OnListDetached() =>
            DisposeFilteredList();

        /// <inheritdoc/>
        protected sealed override IReadOnlyFilteredList<IViewModel>? GetFilteredList(IReadOnlyList<IViewModel> list)
        {
            var filter = _filter is null ? null : new Predicate<IViewModel>(_filter.Matches);

            if (_order is not null || filter is not null)
                _filteredList = new FilteredList<IViewModel>(list, _order, filter);

            return _filteredList;
        }

        /// <inheritdoc/>
        protected sealed override void OnAdded(IViewModel? newItem, int index) =>
            ObservableListViewModelBinderHelper.OnAdded(Views, Factory, newItem, index);

        /// <inheritdoc/>
        protected sealed override void OnAdded(IReadOnlyList<IViewModel?>? newItems, int index)
        {
            if (newItems is null) return;

            var offset = 0;

            foreach (var item in newItems)
                OnAdded(item, index + offset++);
        }

        /// <inheritdoc/>
        protected sealed override void OnRemoved(IViewModel? oldItem, int oldStartingIndex) =>
            ObservableListViewModelBinderHelper.OnRemoved(Views, Factory, oldStartingIndex);

        /// <inheritdoc/>
        protected sealed override void OnRemoved(IReadOnlyList<IViewModel?>? oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;

            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        /// <inheritdoc/>
        protected sealed override void OnReplaced(IViewModel? oldItem, IViewModel? newItem, int index) =>
            ObservableListViewModelBinderHelper.OnReplaced(Views, newItem, index);

        /// <inheritdoc/>
        protected sealed override void OnMoved(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex) =>
            ObservableListViewModelBinderHelper.OnMoved(Views, oldStartingIndex, newStartingIndex);

        /// <inheritdoc/>
        protected sealed override void OnReset()
        {
            try
            {
                if (_views is not null && _views.Count > 0)
                    ObservableListViewModelBinderHelper.OnReset(_views, Factory);
            }
            finally
            {
                _activeFactory = null;
            }
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }
    }
}
#endif
