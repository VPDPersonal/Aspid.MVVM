using System;
using UnityEngine;
#if ASPID_MVVM_ZENJECT_INTEGRATION
using Inject = Zenject.InjectAttribute;
using DIContainer = Zenject.DiContainer;
#elif ASPID_MVVM_VCONTAINER_INTEGRATION
using Inject = VContainer.InjectAttribute;
using DIContainer = VContainer.IObjectResolver;
#endif

namespace Aspid.MVVM.StarterKit.Views
{
    [AddComponentMenu("MVVM/View Initializers/View Initializer Manual")]
    public sealed class ViewInitializerManual : ViewInitializerBase
    {
        [SerializeField] private bool _isDisposeViewOnDestroy;
        [SerializeField] private InitializeComponent<IView>[] _viewComponents;
        
        private IView[] _views;
        private IViewModel _viewModel;
        private bool _isConstructed;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
        [Inject] private DIContainer _diContainer;
#endif

        public bool IsInitialized { get; private set; }
        
        private void Constructor()
        {
            if (_isConstructed) return;
            
            for (var i = 0; i < _views.Length; i++)
            {
                var viewComponent = _viewComponents[i];
                
                _views[i] = viewComponent.Resolve switch
                { 
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                    InitializeComponent.Resolve.Di => _diContainer.Resolve(viewComponent.Type) as IView, 
#endif
                    InitializeComponent.Resolve.Mono => viewComponent.Mono as IView,
                    InitializeComponent.Resolve.References => viewComponent.References,
                    InitializeComponent.Resolve.ScriptableObject => viewComponent.Scriptable as IView,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            _isConstructed = true;
        }

        public void Initialize(IViewModel viewModel)
        {
            if (IsInitialized)
                throw new Exception($"{nameof(ViewInitializerManual)} can't be initialized twice");

            Constructor();
            
            foreach (var view in _views)
                view.Initialize(viewModel);

            IsInitialized = true;
        }

        public void Deinitialize()
        {
            if (!IsInitialized) return;

            foreach (var view in _views)
                view.Deinitialize();

            IsInitialized = true;
        }

        private void OnValidate()
        {
            if (_viewComponents is null) return;
            
            foreach (var viewComponent in _viewComponents)
            {
                switch (viewComponent?.Resolve)
                {
                    case InitializeComponent.Resolve.Mono:
                        viewComponent.Type = null;
                        viewComponent.References = null;
                        viewComponent.Scriptable = null;
                        break;

                    case InitializeComponent.Resolve.References:
                        viewComponent.Type = null;
                        viewComponent.Mono = null;
                        viewComponent.Scriptable = null;
                        break;

                    case InitializeComponent.Resolve.ScriptableObject:
                        viewComponent.Type = null;
                        viewComponent.Mono = null;
                        viewComponent.References = null;
                        break;

#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                    case InitializeComponent.Resolve.Di:
                        viewComponent.Mono = null;
                        viewComponent.References = null;
                        viewComponent.Scriptable = null;
                        break;
#endif
                }
            }
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