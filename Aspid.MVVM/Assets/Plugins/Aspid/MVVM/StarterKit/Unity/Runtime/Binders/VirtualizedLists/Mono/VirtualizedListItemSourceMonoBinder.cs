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
    [AddBinderContextMenu(typeof(VirtualizedList))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/VirtualizedList/VirtualizedList Binder – ItemSource")]
    public sealed partial class VirtualizedListItemSourceMonoBinder : ComponentMonoBinder<VirtualizedList>, IBinder<IReadOnlyList<IViewModel>>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Filter _filter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Comparer _comparer;

        private FilteredList<IViewModel> _filteredList;

        protected override void OnUnbound() =>
            DisposeFilteredList();

        [BinderLog]
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

            CachedComponent.ItemsSource = list;
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }
    }
}