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
    /// <see cref="ObservableListBinder{T}"/> that creates a view per ViewModel in list order, with an optional filter and sort order.
    /// </summary>
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
        private FilteredList<IViewModel>? _filteredList;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected ObservableListViewModelBinder() { }

        /// <param name="viewFactory">The factory that creates and releases views.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewFactory"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public ObservableListViewModelBinder(IViewFactory<TView> viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        private List<TView> Views => _views ??= new List<TView>();

        /// <summary>
        /// Disposes the filtered view before the base class detaches from the list.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnUnbound()</c>.
        /// </remarks>
        protected override void OnUnbound()
        {
            DisposeFilteredList();
            base.OnUnbound();
        }

        /// <inheritdoc/>
        protected sealed override IReadOnlyFilteredList<IViewModel>? GetFilteredList(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();

            var filter = _filter is null ? null : new Predicate<IViewModel>(_filter.Matches);

            if (_order is not null || filter is not null)
                _filteredList = new FilteredList<IViewModel>(list, _order, filter);

            return _filteredList;
        }

        /// <inheritdoc/>
        protected sealed override void OnAdded(IViewModel? newItem, int index)
        {
            if (newItem is null) return;
            ObservableListViewModelBinderHelper.OnAdded(Views, _viewFactory, newItem, index);
        }

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
            ObservableListViewModelBinderHelper.OnRemoved(Views, _viewFactory, oldStartingIndex);

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
        protected sealed override void OnReset() =>
            ObservableListViewModelBinderHelper.OnReset(Views, _viewFactory);

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }
    }
}
