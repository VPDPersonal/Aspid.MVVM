using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that sets <see cref="VirtualizedList.ItemsSource"/>
    /// to the bound list, optionally filtered and sorted.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(VirtualizedList))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList Binder – ItemSource")]
    public sealed partial class VirtualizedListItemSourceMonoBinder : ComponentMonoBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [Tooltip("Optional filter; empty shows every item.")]
        [SerializeReference] private ICollectionFilter<IViewModel> _filter;

        [Tooltip("Optional sort order; empty keeps the list order.")]
        [SerializeReference] private ICollectionOrder<IViewModel> _order;

        private FilteredList<IViewModel> _filteredList;

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            CachedComponent.ItemsSource = null;
            DisposeFilteredList();
        }

        /// <summary>
        /// Sets <see cref="VirtualizedList.ItemsSource"/>, wrapping the list in a <see cref="FilteredList{T}"/>
        /// when a filter or order is configured.
        /// </summary>
        /// <param name="list">The list to display; <c>null</c> clears the item source.</param>
        [BinderLog]
        public void SetValue(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();

            if (list is not null)
            {
                var filter = _filter is null ? null : new Predicate<IViewModel>(_filter.Matches);

                if (_order is not null || filter is not null)
                {
                    _filteredList = new FilteredList<IViewModel>(list, _order, filter);
                    list = _filteredList;
                }
            }

            CachedComponent.ItemsSource = list;
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }
    }
}
