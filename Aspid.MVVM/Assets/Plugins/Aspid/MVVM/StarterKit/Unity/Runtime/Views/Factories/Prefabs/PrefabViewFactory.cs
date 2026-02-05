using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class PrefabViewFactory : PrefabViewFactory<MonoView>, IViewFactoryMonoView
    {
        [Obsolete("For Unity Inspector", true)]
        public PrefabViewFactory() { }
        
        public PrefabViewFactory(MonoView prefab, bool overrideSibling = false, int siblingIndex = 0) :
            this(prefab, null, overrideSibling, siblingIndex) { }
        
        public PrefabViewFactory(MonoView prefab, Transform container, bool overrideSibling = false, int siblingIndex = 0)
            : base(prefab, container, overrideSibling, siblingIndex) { }
    }
    
    [Serializable]
    public class PrefabViewFactory<T> : IViewFactory<T>
        where T : MonoBehaviour, IView
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _container;
        
        [FormerlySerializedAs("_addNewElementOnTop")] 
        [SerializeField] private bool _overrideSibling;
        
        [SerializeField] private int _siblingIndex;
        
        [Obsolete("For Unity Inspector", true)]
        public PrefabViewFactory() { }
        
        public PrefabViewFactory(T prefab, bool overrideSibling = false, int siblingIndex = 0) :
            this(prefab, null, overrideSibling, siblingIndex) { }
        
        public PrefabViewFactory(T prefab, Transform container, bool overrideSibling = false, int siblingIndex = 0)
        {
            _prefab = prefab;
            _container = container;
            _siblingIndex = siblingIndex;
            _overrideSibling = overrideSibling;
        }

        public virtual T Create(IViewModel viewModel)
        {
            var view = Object.Instantiate(_prefab, _container);
            OnCreate(viewModel, view);

            return view;
        }

        protected virtual void OnCreate(IViewModel viewModel, T view)
        {
            SetSibling(view);
            
            if (viewModel is not null)
                view.Initialize(viewModel);
        }

        public virtual void Release(T view) =>
            view.DestroyView();

        protected void SetSibling(T view)
        {
            if (_overrideSibling) view.transform.SetSiblingIndex(_siblingIndex);
            else view.transform.SetAsLastSibling();
        }
    }
}