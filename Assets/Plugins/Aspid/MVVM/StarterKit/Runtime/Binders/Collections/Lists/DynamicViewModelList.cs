#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class DynamicViewModelList : DynamicViewModelList<MonoView>
    {
        public DynamicViewModelList(MonoView prefab, Transform container) 
            : base(prefab, container) { }
    }
    
    [Serializable]
    public class DynamicViewModelList<T> : ListBinderBase<IViewModel>
        where T : MonoView
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform? _container;

        private readonly List<T> _views = new();
        
        public T Prefab => _prefab;
        
        public Transform? Container => _container;
        
        public DynamicViewModelList(T prefab, Transform? container = null)
        {
            _container = container;
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        }

        protected sealed override void OnAdded(IViewModel? newItem, int newStartingIndex)
        {
            var view = GetNewView();
            
            if (newItem is not null) 
                view.Initialize(newItem);

            _views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel?>? newItems, int newStartingIndex)
        {
            if (newItems is null) return;
            
            foreach (var item in newItems)
                OnAdded(item, newStartingIndex);
        }

        protected sealed override void OnRemoved(IViewModel? oldItem, int oldStartingIndex)
        {
            ReleaseView(_views[oldStartingIndex]);
            _views.RemoveAt(oldStartingIndex);
        }

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel?>? oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;
            
            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplace(IViewModel? oldItem, IViewModel? newItem, int newStartingIndex)
        {
            _views[newStartingIndex].Deinitialize();
            
            if (newItem is not null)
                _views[newStartingIndex].Initialize(newItem);
        }

        protected sealed override void OnMove(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex)
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