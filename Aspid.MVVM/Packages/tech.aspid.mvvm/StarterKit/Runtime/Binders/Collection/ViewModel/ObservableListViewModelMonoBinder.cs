using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Order = Aspid.MVVM.StarterKit.ICollectionOrder<Aspid.MVVM.IViewModel>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ObservableListViewModelMonoBinder{TView}"/> over <see cref="MonoView"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collection/Observable List Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collection/Observable List Binder – ViewModel")]
    public class ObservableListViewModelMonoBinder : ObservableListViewModelMonoBinder<MonoView> { }

    /// <summary>
    /// <see cref="ObservableListMonoBinder{T}"/> that creates a view per ViewModel in list order, with an optional filter and sort order.
    /// </summary>
    /// <typeparam name="TView">The type of view created per item.</typeparam>
    public abstract class ObservableListViewModelMonoBinder<TView> : ObservableListMonoBinder<IViewModel>
        where TView : MonoBehaviour, IView
    {
        [Tooltip("Creates a view per item. Required.")]
        [SerializeReference] private IViewFactory<TView> _viewFactory;

        [Tooltip("Optional filter; empty shows every item.")]
        [SerializeReference] private Filter _filter;

        [Tooltip("Optional sort order; empty keeps the list order.")]
        [SerializeReference] private Order _order;

        private List<TView> _views;
        private FilteredList<IViewModel> _filteredList;

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
        protected sealed override IReadOnlyFilteredList<IViewModel> GetFilteredList(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();

            var filter = _filter is null ? null : new Predicate<IViewModel>(_filter.Matches);

            if (_order is not null || filter is not null)
                _filteredList = new FilteredList<IViewModel>(list, _order, filter);

            return _filteredList;
        }

        /// <inheritdoc/>
        protected sealed override void OnAdded(IViewModel newItem, int index)
        {
            if (!HasFactory()) return;
            ObservableListViewModelBinderHelper.OnAdded(Views, _viewFactory, newItem, index);
        }

        /// <inheritdoc/>
        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int index)
        {
            if (newItems is null) return;

            var offset = 0;

            foreach (var item in newItems)
                OnAdded(item, index + offset++);
        }

        /// <inheritdoc/>
        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            if (!HasFactory()) return;
            ObservableListViewModelBinderHelper.OnRemoved(Views, _viewFactory, oldStartingIndex);
        }

        /// <inheritdoc/>
        protected sealed override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;

            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        /// <inheritdoc/>
        protected sealed override void OnReplaced(IViewModel oldItem, IViewModel newItem, int index) =>
            ObservableListViewModelBinderHelper.OnReplaced(Views, newItem, index);

        /// <inheritdoc/>
        protected sealed override void OnMoved(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex) =>
            ObservableListViewModelBinderHelper.OnMoved(Views, oldStartingIndex, newStartingIndex);

        /// <inheritdoc/>
        protected sealed override void OnReset()
        {
            if (_views is null || _views.Count is 0) return;
            if (!HasFactory()) return;

            ObservableListViewModelBinderHelper.OnReset(Views, _viewFactory);
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }

        private bool HasFactory()
        {
            if (_viewFactory is not null) return true;

            this.LogError(
                problem: "no view factory is assigned",
                consequence: "No view is created for the list.");

            return false;
        }
    }
}
