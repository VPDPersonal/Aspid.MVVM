using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
using Aspid.Collections.Observable.Filtered;
#if UNITY_2023_1_OR_NEWER
using FilterFactory = Aspid.MVVM.StarterKit.IFilterFactory<Aspid.MVVM.IViewModel>;
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactory<Aspid.MVVM.Unity.MonoView>;
#else
using FilterFactory = Aspid.MVVM.StarterKit.IViewModelFilterFactory;
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactoryMonoView;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Observable List Binder - ViewModel")]
    [AddComponentContextMenu(typeof(Component), "Add General Binder/Collections/Observable List Binder - ViewModel")]
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
        [SerializeReference] private FilterFactory _filterFactory;

        private List<T> _views;
        
        private List<T> Views => _views ??= new List<T>();

        protected override void OnUnbound()
        {
            _filterFactory?.Release();
            base.OnUnbound();
        }

        protected sealed override IReadOnlyFilteredList<IViewModel> GetFilter(IReadOnlyList<IViewModel> list) =>
            _filterFactory?.Create(list);

        protected sealed override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var view = _viewFactory.Create(newItem);
            Views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            if (newItems is null) return;

            for (var i = 0; i < newItems.Count; i++)
                OnAdded(newItems[i], newStartingIndex + i);
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