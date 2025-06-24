using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
#if UNITY_2023_1_OR_NEWER
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactory<UnityEngine.Transform, Aspid.MVVM.Unity.MonoView>;
#else
using ViewFactory = Aspid.MVVM.StarterKit.Unity.IViewFactoryMonoView;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Observable Lists/Dynamic Observable List - ViewModel")]
    [AddComponentContextMenu(typeof(Component), "Add General Binder/Collection/Dynamic Observable List - ViewModel")]
    public class DynamicViewModelMonoObservableList : DynamicViewModelMonoObservableList<MonoView, ViewFactory> { }
    
    public abstract class DynamicViewModelMonoObservableList<T> : DynamicViewModelMonoObservableList<T, IViewFactory<Transform, T>> 
        where T : MonoBehaviour, IView { }
    
    public abstract class DynamicViewModelMonoObservableList<T, TViewFactory> : ObservableListMonoBinderBase<IViewModel>
        where T : MonoBehaviour, IView
        where TViewFactory : IViewFactory<Transform, T>
    {
        [SerializeField] private Transform _container;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private TViewFactory _viewFactory;
        
        [SerializeField] private bool _addNewElementOnTop;

        private List<T> _views;
        
        private List<T> Views => _views ??= new List<T>();

        protected sealed override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var view = GetNewView(newItem);
            
            if (_addNewElementOnTop)
                view.transform.SetAsFirstSibling();

            Views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            if (newItems is null) return;
            
            foreach (var item in newItems)
                OnAdded(item, newStartingIndex);
        }

        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            ReleaseView(Views[oldStartingIndex]);
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
                ReleaseView(view);
            
            Views.Clear();
        }

        protected virtual T GetNewView(IViewModel viewModel) =>
            _viewFactory.Create(viewModel, _container);
        
        protected virtual void ReleaseView(T view) => 
            _viewFactory.Release(view);
    }
}