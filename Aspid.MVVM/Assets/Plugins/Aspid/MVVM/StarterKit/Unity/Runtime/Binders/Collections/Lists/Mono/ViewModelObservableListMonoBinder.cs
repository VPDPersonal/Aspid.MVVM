using UnityEngine;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
#if UNITY_2023_1_OR_NEWER
using Filter = Aspid.MVVM.StarterKit.ICollectionFilter<Aspid.MVVM.IViewModel>;
using Comparer = Aspid.MVVM.StarterKit.ICollectionComparer<Aspid.MVVM.IViewModel>;
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactory<Aspid.MVVM.MonoView>;
#else
using ViewFactory = Aspid.MVVM.StarterKit.IViewFactoryMonoView;
using Filter = Aspid.MVVM.StarterKit.IViewModelCollectionFilter;
using Comparer = Aspid.MVVM.StarterKit.IViewModelCollectionComparer;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Observable List Binder – ViewModel")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Collections/Observable List Binder – ViewModel")]
    public class ViewModelObservableListMonoBinder : ViewModelObservableListMonoBinder<MonoView, ViewFactory> { }

#if UNITY_2023_1_OR_NEWER
    public abstract class ViewModelObservableListMonoBinder<T> : ViewModelObservableListMonoBinder<T, IViewFactory<T>> 
        where T : MonoBehaviour, IView { }
#endif
    
    public abstract class ViewModelObservableListMonoBinder<T, TViewFactory> : ObservableListMonoBinder<IViewModel>
        where T : MonoBehaviour, IView
        where TViewFactory : IViewFactory<T>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private TViewFactory _viewFactory;

        [SerializeReferenceDropdown]
        [SerializeReference] private Filter _filter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Comparer _comparer;

        private List<T> _views;
        private FilteredList<IViewModel> _filteredList;
        
        private List<T> Views => _views ??= new List<T>();

        protected override void OnUnbound()
        {
            DisposeFilteredList();
            base.OnUnbound();
        }

        protected sealed override IReadOnlyFilteredList<IViewModel> GetFilterList(IReadOnlyList<IViewModel> list)
        {
            DisposeFilteredList();

            var comparer = _comparer?.Get();
            var filter = _filter?.Get();

            if (comparer is not null || filter is not null)
                _filteredList = new FilteredList<IViewModel>(list, comparer, filter);

            return _filteredList;
        }

        private void DisposeFilteredList()
        {
            _filteredList?.Dispose();
            _filteredList = null;
        }

        protected sealed override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var view = _viewFactory.Create(newItem);
            Views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            if (newItems is null) return;
            
            var index = 0;
            
            foreach (var item in newItems)
                OnAdded(item, newStartingIndex: newStartingIndex + index++);
        }

        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            _viewFactory.Release(Views[oldStartingIndex]);
            Views.RemoveAt(oldStartingIndex);
        }

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;
            
            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            Views[newStartingIndex].Deinitialize();
            
            if (newItem is not null)
                Views[newStartingIndex].Initialize(newItem);
        }

        protected sealed override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
        {
            var oldSiblingIndex = Views[oldStartingIndex].transform.GetSiblingIndex();
            var newSiblingIndex = Views[newStartingIndex].transform.GetSiblingIndex();
            
            Views[oldSiblingIndex].transform.SetSiblingIndex(newSiblingIndex);
            Views[newSiblingIndex].transform.SetSiblingIndex(oldSiblingIndex);
        }

        protected sealed override void OnReset()
        {
            foreach (var view in Views)
                _viewFactory.Release(view);
            
            Views.Clear();
        }
    }
}