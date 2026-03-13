using System;
using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
#if UNITY_2023_1_OR_NEWER
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Comparer = Aspid.MVVM.StarterKit.ICollectionComparer<Aspid.MVVM.IViewModel>;
#else
using Filter = Aspid.MVVM.StarterKit.IViewModelCollectionFilter;
using Comparer = Aspid.MVVM.StarterKit.IViewModelCollectionComparer;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{VirtualizedList}"/> that sets the item source of a <see cref="VirtualizedList"/>
    /// to the bound <see cref="IReadOnlyList{IViewModel}"/> value.
    /// Supports optional filtering and sorting via <see cref="ICollectionFilter{IViewModel}"/> and
    /// <see cref="ICollectionComparer{IViewModel}"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-VirtualizedList-ItemSource-1.1.0.xml" path="doc//member[@name='VirtualizedListItemSourceBinder']/*" />
    [Serializable]
    public sealed class VirtualizedListItemSourceBinder : TargetBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Filter _filter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Comparer _comparer;

        private FilteredList<IViewModel> _filteredList;

        /// <summary>
        /// Called when the binder is unbound. Clears the item source and disposes the filtered list if one was created.
        /// </summary>
        protected override void OnUnbound()
        {
            Target.ItemsSource = null;
            DisposeFilteredList();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="VirtualizedListItemSourceBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="VirtualizedList"/> to bind.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public VirtualizedListItemSourceBinder(VirtualizedList target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }
        
        /// <inheritdoc/>
        public VirtualizedListItemSourceBinder(VirtualizedList target, Filter filter, BindMode mode = BindMode.OneWay) :
            this(target, comparer: null, filter, mode) { }
        
        /// <inheritdoc/>
        public VirtualizedListItemSourceBinder(VirtualizedList target, Filter filter, Comparer comparer, BindMode mode = BindMode.OneWay) :
            this(target, comparer, filter, mode) { }
        
        /// <inheritdoc/>
        public VirtualizedListItemSourceBinder(VirtualizedList target, Comparer comparer, BindMode mode = BindMode.OneWay) :
            this(target, comparer, filter: null, mode) { }
        
        /// <summary>
        /// Initializes a new instance of <see cref="VirtualizedListItemSourceBinder"/> with a comparer and filter.
        /// </summary>
        /// <param name="target">The <see cref="VirtualizedList"/> to bind.</param>
        /// <param name="comparer">The comparer used to sort items, or <see langword="null"/> to use source order.</param>
        /// <param name="filter">The filter used to exclude items, or <see langword="null"/> to include all items.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/>.</param>
        public VirtualizedListItemSourceBinder(VirtualizedList target, Comparer comparer, Filter filter, BindMode mode = BindMode.OneWay)
            : this(target, mode)
        {
            _filter = filter;
            _comparer = comparer;
        }

        /// <summary>
        /// Sets <see cref="VirtualizedList.ItemsSource"/> to the specified list,
        /// wrapping it in a <see cref="FilteredList{IViewModel}"/> when a filter or comparer is configured.
        /// </summary>
        public void SetValue(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();
            
            if (list is not null)
            {
                var comparer = _comparer?.Get();
                var filter = _filter?.Get();
            
                if (comparer is not null || filter is not null)
                {
                    _filteredList = new FilteredList<IViewModel>(list, comparer, filter);
                    list = _filteredList;
                }   
            }

            Target.ItemsSource = list;
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }
    }
}