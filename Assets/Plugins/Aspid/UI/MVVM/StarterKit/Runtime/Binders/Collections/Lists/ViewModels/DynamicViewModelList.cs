using System;
using UnityEngine;
using Aspid.UI.MVVM.Mono.Views;
using Aspid.UI.MVVM.ViewModels;
using System.Collections.Generic;
using Aspid.UI.MVVM.Mono.Views.Extensions;
using Object = UnityEngine.Object;

namespace Aspid.UI.MVVM.StarterKit.Binders.Collections.Lists.ViewModels
{
    [Serializable]
    public class DynamicViewModelList : DynamicViewModelList<MonoView>
    {
        public DynamicViewModelList() { }
        
        public DynamicViewModelList(MonoView prefab, Transform container) 
            : base(prefab, container) { }
    }
    
    [Serializable]
    public class DynamicViewModelList<T> : ListBinderBase<IViewModel>
        where T : MonoView
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _container;

        private readonly List<T> _views = new();
        
        public T Prefab => _prefab;
        
        public Transform Container => _container;

        public DynamicViewModelList() { }
        
        public DynamicViewModelList(T prefab, Transform container)
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
            foreach (var item in newItems)
                OnAdded(item, newStartingIndex);
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
            Object.Instantiate(Prefab, Container);
        
        protected virtual void ReleaseView(T view) => view.DestroyView();
    }
}