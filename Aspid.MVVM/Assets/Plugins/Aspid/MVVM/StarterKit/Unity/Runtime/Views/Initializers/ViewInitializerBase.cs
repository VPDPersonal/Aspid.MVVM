using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract class ViewInitializerBase : MonoBehaviour
    { 
        [SerializeField] private bool _isDisposeViewOnDestroy = true;
        [SerializeField] private InitializeComponent<IView>[] _viewComponents;
        
        private IView[] _views;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private Zenject.DiContainer _zenjectContainer;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [VContainer.Inject] 
        private VContainer.IObjectResolver _vcontainerContainer; 
#endif
        
        public IView[] Views
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    return GetViews();
                }
#endif

                return _views ??= GetViews();

                IView[] GetViews()
                {
                    var views = new IView[_viewComponents.Length];
                    
                    for (var i = 0; i < views.Length; i++)
                    {
                        var view = GetFromInitializeComponent(_viewComponents[i]);
                
                        if (view is IComponentInitializable viewInitializable)
                            viewInitializable.Initialize();
                    
                        views[i] = view;
                    }

                    return views;
                }
            }
        }
        
        public abstract IViewModel ViewModel { get; }
        
        public bool IsInitialized { get; protected set; }

        protected bool IsDisposeViewOnDestroy => _isDisposeViewOnDestroy;

        protected virtual void OnValidate()
        {
            if (_viewComponents is not null)
            {
                foreach (var viewComponent in _viewComponents)
                    viewComponent?.Validate();
            }
        }

        protected T GetFromInitializeComponent<T>(InitializeComponent<T> initializeComponent)
            where T : class
        {
            switch (initializeComponent.Resolve)
            {
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case InitializeComponent.ResolveType.Di:
#if ASPID_MVVM_ZENJECT_INTEGRATION
                    var zenjectResult = _zenjectContainer?.TryResolve(initializeComponent.Type);
                    if (zenjectResult is T specificZenjectResult) return specificZenjectResult;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
                    if (_vcontainerContainer?.TryResolve(initializeComponent.Type, out var specificVContainerResult) ?? false)
                        return specificVContainerResult as T;
#endif
                    throw new Exception("Unknown initialize component type: " + initializeComponent.Type);
#endif
                case InitializeComponent.ResolveType.Mono: return initializeComponent.Mono as T;
                case InitializeComponent.ResolveType.References: return initializeComponent.References;
                case InitializeComponent.ResolveType.ScriptableObject: return initializeComponent.Scriptable as T;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}