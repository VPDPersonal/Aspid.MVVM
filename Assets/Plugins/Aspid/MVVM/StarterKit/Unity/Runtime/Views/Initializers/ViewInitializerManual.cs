using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer Manual")]
    public sealed class ViewInitializerManual : ViewInitializerBase
    {
        [SerializeField] private bool _isDisposeViewOnDestroy = true;
        [SerializeField] private InitializeComponent<IView>[] _viewComponents;
        
        private IView[] _views;
        private bool _isConstructed;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private Zenject.DiContainer _zenjectContainer;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [VContainer.Inject] 
        private VContainer.IObjectResolver _vcontainerContainer; 
#endif
        
        private void Constructor()
        {
            if (_isConstructed) return;

            _views = new IView[_viewComponents.Length];
            
            for (var i = 0; i < _views.Length; i++)
                _views[i] = Get(_viewComponents[i]);

            _isConstructed = true;
            return;

            T Get<T>(InitializeComponent<T> initializeComponent) 
                where T : class
            {
                switch (initializeComponent.Resolve)
                {
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                    case InitializeComponent.ResolveType.Di:
#if ASPID_MVVM_ZENJECT_INTEGRATION
                        var result = _zenjectContainer?.Resolve(initializeComponent.Type);
                        if (result is T specificResult) return specificResult;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
                        return _vcontainerContainer?.Resolve(initializeComponent.Type) as T;
#endif
#endif
                    
                    case InitializeComponent.ResolveType.Mono: return initializeComponent.Mono as T;
                    case InitializeComponent.ResolveType.References: return initializeComponent.References;
                    case InitializeComponent.ResolveType.ScriptableObject: return initializeComponent.Scriptable as T;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Initialize(IViewModel viewModel)
        {
            if (IsInitialized)
                throw new Exception($"{nameof(ViewInitializerManual)} can't be initialized twice");
            
            if (viewModel is null)
	            if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));

            Constructor();

            ViewModel = viewModel;
            foreach (var view in _views)
                view.Initialize(viewModel);
            
            IsInitialized = true;
        }

        public void Deinitialize()
        {
            if (!IsInitialized) return;

            foreach (var view in _views)
                view.Deinitialize();

            ViewModel = null;
            IsInitialized = false;
        }

        private void OnValidate()
        {
            if (_viewComponents is null) return;
            
            foreach (var viewComponent in _viewComponents)
                viewComponent?.Validate();
        }

        private void OnDestroy()
        {
            if (_isDisposeViewOnDestroy)
            {
                foreach (var view in _views)
                    view.DisposeView();
            }
        }
    }
}