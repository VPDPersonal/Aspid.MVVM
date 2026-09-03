using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Order = Aspid.MVVM.StarterKit.ICollectionOrder<Aspid.MVVM.IViewModel>;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactory<Aspid.MVVM.MonoView>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Concrete <see cref="ObservableListViewModelBinder{T, TViewFactory}"/> that uses <see cref="MonoView"/> as the view type
    /// and the default <see cref="IViewFactory{T}"/> as the factory.
    /// </summary>
    [Serializable]
    public class ObservableListViewModelBinder : ObservableListViewModelBinder<MonoView, ViewFactory>
    {
        /// <inheritdoc/>
        public ObservableListViewModelBinder(ViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableListViewModelBinder{T, TViewFactory}"/> that uses <see cref="IViewFactory{T}"/> as the factory type.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="MonoBehaviour"/> view created for each item in the list.</typeparam>
    [Serializable]
    public class ObservableListViewModelBinder<T> : ObservableListViewModelBinder<T, IViewFactory<T>>
        where T : MonoBehaviour, IView
    {
        /// <inheritdoc/>
        public ObservableListViewModelBinder(IViewFactory<T> viewFactory, BindMode mode = BindMode.OneWay)
            : base(viewFactory, mode) { }
    }

    /// <summary>
    /// <see cref="ObservableListBinder{T}"/> that instantiates and releases <typeparamref name="T"/> view objects
    /// for each <see cref="IViewModel"/> in a bound observable list,
    /// with optional filtering and sorting support.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="MonoBehaviour"/> view created for each item in the list.</typeparam>
    /// <typeparam name="TViewFactory">The factory type used to create and release view instances.</typeparam>
    [Serializable]
    public class ObservableListViewModelBinder<T, TViewFactory> : ObservableListBinder<IViewModel>
        where T : MonoBehaviour, IView
        where TViewFactory : IViewFactory<T>
    {
        [Tooltip("Creates and releases a view for each list item.")]
        [SerializeReference] private TViewFactory _viewFactory;

        [Tooltip("Optional filter for which items are shown. Leave empty to show all.")]
        [SerializeReference] private Filter _filter;

        [Tooltip("Optional sort order. Leave empty to keep the collection's own order.")]
        [FormerlySerializedAs("_comparer")]
        [SerializeReference] private Order _order;

        private List<T> _views;
        private FilteredList<IViewModel> _filteredList;

        private List<T> Views => _views ??= new List<T>();

        /// <param name="viewFactory">The factory used to create and release view instances for each list item.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public ObservableListViewModelBinder(TViewFactory viewFactory, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _viewFactory = viewFactory ?? throw new ArgumentNullException(nameof(viewFactory));
        }

        /// <summary>
        /// Called when the binding is released. Disposes the filtered view of the list before the base class
        /// detaches from it.
        /// </summary>
        /// <remarks>
        /// The order matters: the filtered list subscribes to the source, so disposing it after the base class
        /// has dropped the source would leave the subscription behind. When overriding, always call the base
        /// implementation.
        /// </remarks>
        protected override void OnUnbound()
        {
            DisposeFilteredList();
            base.OnUnbound();
        }

        protected sealed override IReadOnlyFilteredList<IViewModel> GetFilteredList(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();

            var filter = _filter is null ? null : new Predicate<IViewModel>(_filter.Matches);

            if (_order is not null || filter is not null)
                _filteredList = new FilteredList<IViewModel>(list, _order, filter);

            return _filteredList;
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }

        protected sealed override void OnAdded(IViewModel newItem, int index) =>
            ObservableListViewModelBinderHelper.OnAdded(Views, _viewFactory, newItem, index);

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int index)
        {
            if (newItems is null) return;

            var offset = 0;

            foreach (var item in newItems)
                OnAdded(item, index + offset++);
        }

        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex) =>
            ObservableListViewModelBinderHelper.OnRemoved(Views, _viewFactory, oldStartingIndex);

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;

            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplaced(IViewModel oldItem, IViewModel newItem, int index) =>
            ObservableListViewModelBinderHelper.OnReplaced(Views, newItem, index);

        protected sealed override void OnMoved(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex) =>
            ObservableListViewModelBinderHelper.OnMoved(Views, oldStartingIndex, newStartingIndex);

        protected sealed override void OnReset() =>
            ObservableListViewModelBinderHelper.OnReset(Views, _viewFactory);
    }
}
