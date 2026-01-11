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
    [Serializable]
    public sealed class VirtualizedListItemSourceBinder : TargetBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Filter _filter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Comparer _comparer;

        private FilteredList<IViewModel> _filteredList;

        protected override void OnUnbound() =>
            DisposeFilteredList();

        public VirtualizedListItemSourceBinder(VirtualizedList target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();
        }
        
        public VirtualizedListItemSourceBinder(VirtualizedList target, Filter filter, BindMode mode = BindMode.OneWay) :
            this(target, comparer: null, filter, mode) { }
        
        public VirtualizedListItemSourceBinder(VirtualizedList target, Filter filter, Comparer comparer, BindMode mode = BindMode.OneWay) :
            this(target, comparer, filter, mode) { }
        
        public VirtualizedListItemSourceBinder(VirtualizedList target, Comparer comparer, BindMode mode = BindMode.OneWay) :
            this(target, comparer, filter: null, mode) { }
        
        public VirtualizedListItemSourceBinder(VirtualizedList target, Comparer comparer, Filter filter, BindMode mode = BindMode.OneWay)
            : this(target, mode)
        {
            _filter = filter;
            _comparer = comparer;
        }

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