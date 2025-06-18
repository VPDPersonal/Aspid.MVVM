#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public class DynamicViewModelObservableList : DynamicViewModelObservableList<MonoView>
    {
        public DynamicViewModelObservableList(MonoView prefab, BindMode mode) 
            : this(prefab, null, mode) { }
        
        public DynamicViewModelObservableList(MonoView prefab, Transform? container, BindMode mode = BindMode.OneWay) 
            : base(prefab, container) { }
    }
    
    [Serializable]
    public class DynamicViewModelObservableList<T> : ObservableListBinderBase<IViewModel>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform? _container;
        [SerializeField] private bool _addNewElementOnTop;

        private List<T>? _views;
        
        public T Prefab => _prefab;
        
        public Transform? Container => _container;

        private List<T> Views => _views ??= new List<T>();
        
        public DynamicViewModelObservableList(T prefab, BindMode mode)
            : this(prefab, null, mode) { }
        
        public DynamicViewModelObservableList(T prefab, Transform? container = null, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            
            _container = container;
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        }

        protected sealed override void OnAdded(IViewModel? newItem, int newStartingIndex)
        {
            var view = GetNewView();
            
            if (_addNewElementOnTop)
                view.transform.SetAsFirstSibling();
            
            if (newItem is not null) 
                view.Initialize(newItem);

            Views.Insert(newStartingIndex, view);
        }

        protected sealed override void OnAdded(IReadOnlyList<IViewModel?>? newItems, int newStartingIndex)
        {
            if (newItems is null) return;
            
            foreach (var item in newItems)
                OnAdded(item, newStartingIndex);
        }

        protected sealed override void OnRemoved(IViewModel? oldItem, int oldStartingIndex)
        {
            ReleaseView(Views[oldStartingIndex]);
            Views.RemoveAt(oldStartingIndex);
        }

        protected sealed override void OnRemoved(IReadOnlyList<IViewModel?>? oldItems, int oldStartingIndex)
        {
            if (oldItems is null) return;
            
            foreach (var item in oldItems)
                OnRemoved(item, oldStartingIndex);
        }

        protected sealed override void OnReplace(IViewModel? oldItem, IViewModel? newItem, int newStartingIndex)
        {
            Views[newStartingIndex].Deinitialize();
            
            if (newItem is not null)
                Views[newStartingIndex].Initialize(newItem);
        }

        protected sealed override void OnMove(IViewModel? oldItem, IViewModel? newItem, int oldStartingIndex, int newStartingIndex)
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
        
        protected virtual T GetNewView() => 
            Object.Instantiate(Prefab, Container);
        
        protected virtual void ReleaseView(T view) => view.DestroyView();
    }
}