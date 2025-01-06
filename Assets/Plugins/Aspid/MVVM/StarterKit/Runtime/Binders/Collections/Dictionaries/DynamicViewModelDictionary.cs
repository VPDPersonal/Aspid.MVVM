#nullable enable
using System;
using UnityEngine;
using Aspid.MVVM.Mono;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class DynamicViewModelDictionary<TKey, TViewModel> : DynamicViewModelDictionary<TKey, TViewModel, MonoView>
        where TViewModel : IViewModel
    {
        public DynamicViewModelDictionary(MonoView prefab, Transform? container = null)
            : base(prefab, container) { }
    }
    
    [Serializable]
    public class DynamicViewModelDictionary<TKey, TViewModel, TView> : DictionaryBinderBase<TKey, TViewModel>
        where TView : MonoView
        where TViewModel : IViewModel
    {
        [SerializeField] private TView _prefab;
        [SerializeField] private Transform? _container;

        private readonly Dictionary<TKey, TView> _views = new();
        
        public TView Prefab => _prefab;
        
        public Transform? Container => _container;
        
        public DynamicViewModelDictionary(TView prefab, Transform? container = null)
        {
            _container = container;
            _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        }

        protected sealed override void OnAdded(KeyValuePair<TKey, TViewModel?> newItem)
        {
            var view = GetView(newItem.Key);
            
            if (newItem.Value is not null)
            {
                view.Initialize(newItem.Value);
            }
        }

        protected sealed override void OnAdded(IReadOnlyList<KeyValuePair<TKey, TViewModel?>> newItems)
        {
            foreach (var item in newItems)
                OnAdded(item);
        }

        protected sealed override void OnRemoved(KeyValuePair<TKey, TViewModel?> oldItem)
        {
            ReleaseView(GetView(oldItem.Key));
            _views.Remove(oldItem.Key);
        }

        protected sealed override void OnRemoved(IReadOnlyList<KeyValuePair<TKey, TViewModel?>> oldItems)
        {
            foreach (var item in oldItems)
                OnRemoved(item);
        }

        protected sealed override void OnReplace(KeyValuePair<TKey, TViewModel?> oldItem, KeyValuePair<TKey, TViewModel?> newItem)
        {
            _views[oldItem.Key].Deinitialize();
            
            if (newItem.Value is not null)
            {
                _views[oldItem.Key].Initialize(newItem.Value);
            }
        }

        protected sealed override void OnReset()
        {
            foreach (var view in _views.Values)
                ReleaseView(view);
            
            _views.Clear();
        }

        private TView GetView(TKey key)
        {
            if (_views.TryGetValue(key, out var view)) return view;

            view = GetNewView();
            _views.Add(key, view);

            return view;
        }

        protected virtual TView GetNewView() =>
            Object.Instantiate(Prefab, Container);
        
        protected virtual void ReleaseView(TView view) =>
            view.DestroyView();
    }
}