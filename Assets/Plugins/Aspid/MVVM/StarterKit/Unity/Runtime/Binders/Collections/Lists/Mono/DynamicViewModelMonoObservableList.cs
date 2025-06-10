using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/Collections/Observable Lists/Dynamic Observable List - ViewModel")]
    [AddComponentContextMenu(typeof(Component), "Add Collection Binder/Dynamic Observable List - ViewModel")]
    public class DynamicViewModelMonoObservableList : DynamicViewModelMonoObservableList<MonoView> { }
    
    public abstract class DynamicViewModelMonoObservableList<T> : ObservableListMonoBinderBase<IViewModel>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _container;

        private readonly List<T> _views = new();
        
        public T Prefab => _prefab;
        
        public Transform Container => _container;

        protected sealed override void OnAdded(IViewModel newItem, int newStartingIndex)
        {
            var view = GetNewView();
            view.Initialize(newItem);
            
            _views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel> newItems, int newStartingIndex)
        {
            var i = 0;
            foreach (var item in newItems)
            {
                OnAdded(item, newStartingIndex + i);
                i++;
            }
        }

        protected sealed override void OnRemoved(IViewModel oldItem, int oldStartingIndex)
        {
            ReleaseView(_views[oldStartingIndex]);
            _views.RemoveAt(oldStartingIndex);
        }

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel> oldItems, int oldStartingIndex)
        {
            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplace(IViewModel oldItem, IViewModel newItem, int newStartingIndex)
        {
            _views[newStartingIndex].Deinitialize();
            _views[newStartingIndex].Initialize(newItem);
        }

        protected sealed override void OnMove(IViewModel oldItem, IViewModel newItem, int oldStartingIndex, int newStartingIndex)
        {
            var oldSiblingIndex = _views[oldStartingIndex].transform.GetSiblingIndex();
            var newSiblingIndex = _views[newStartingIndex].transform.GetSiblingIndex();
            
            _views[oldSiblingIndex].transform.SetSiblingIndex(newSiblingIndex);
            _views[newSiblingIndex].transform.SetSiblingIndex(oldSiblingIndex);
        }

        protected sealed override void OnReset()
        {
            foreach (var view in _views)
                ReleaseView(view);
            
            _views.Clear();
        }
        
        protected virtual T GetNewView() => 
            Instantiate(Prefab, Container);
        
        protected virtual void ReleaseView(T view) => view.DestroyView();
    }
}