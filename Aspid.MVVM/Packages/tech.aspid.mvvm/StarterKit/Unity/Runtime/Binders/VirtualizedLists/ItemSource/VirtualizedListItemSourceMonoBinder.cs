using System;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Order = Aspid.MVVM.StarterKit.ICollectionOrder<Aspid.MVVM.IViewModel>;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{VirtualizedList}"/> that sets the item source of a <see cref="VirtualizedList"/>
    /// to the bound <see cref="IReadOnlyList{IViewModel}"/> value.
    /// Supports optional filtering and sorting via <see cref="ICollectionFilter{IViewModel}"/> and
    /// <see cref="ICollectionOrder{IViewModel}"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(VirtualizedList))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList Binder – ItemSource")]
    public sealed partial class VirtualizedListItemSourceMonoBinder : ComponentMonoBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [Tooltip("Optional filter for which items are shown. Leave empty to show all.")]
        [SerializeReference] private Filter _filter;
        
        [Tooltip("Optional sort order. Leave empty to keep the collection's own order.")]
        [FormerlySerializedAs("_comparer")]
        [SerializeReference] private Order _order;

        private FilteredList<IViewModel> _filteredList;

        /// <summary>
        /// Called when the binder is unbound. Clears the item source and disposes the filtered list if one was created.
        /// </summary>
        protected override void OnUnbound()
        {
            CachedComponent.ItemsSource = null;
            DisposeFilteredList();
        }

        /// <summary>
        /// Sets <see cref="VirtualizedList.ItemsSource"/> to the specified list,
        /// wrapping it in a <see cref="FilteredList{IViewModel}"/> when a filter or comparer is configured.
        /// </summary>
        /// <param name="list">The collection received from the ViewModel.</param>
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