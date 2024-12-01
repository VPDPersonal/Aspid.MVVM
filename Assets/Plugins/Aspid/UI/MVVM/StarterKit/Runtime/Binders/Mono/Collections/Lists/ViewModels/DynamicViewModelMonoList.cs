using UnityEngine;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;
using System.Collections.Generic;
using Aspid.UI.MVVM.Mono.Views.Extensions;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Collections/Lists/Dynamic List - ViewModel")]
    public class DynamicViewModelMonoList : ListMonoBinderBase<IViewModel>
    {
        [SerializeField] private MonoView _prefab;
        [SerializeField] private Transform _container;

        private readonly List<MonoView> _views = new();
        
        public MonoView Prefab => _prefab;
        
        public Transform Container => _container;

        public DynamicViewModelMonoList() { }
        
        public DynamicViewModelMonoList(MonoView prefab, Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

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
        
        protected virtual MonoView GetNewView() => 
            Instantiate(Prefab, Container);
        
        protected virtual void ReleaseView(MonoView view) => view.DestroyView();
    }
}