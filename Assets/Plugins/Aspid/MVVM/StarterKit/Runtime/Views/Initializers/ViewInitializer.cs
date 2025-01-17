using System;
using UnityEngine;
using Aspid.MVVM.Mono;

#if ASPID_MVVM_ZENJECT_INTEGRATION
using Inject = Zenject.InjectAttribute;
using DIContainer = Zenject.DiContainer;
#elif ASPID_MVVM_VCONTAINER_INTEGRATION
using Inject = VContainer.InjectAttribute;
using DIContainer = VContainer.IObjectResolver;
#endif

namespace Aspid.MVVM.StarterKit.Views.Initializers
{
    [AddComponentMenu("MVVM/View Initializers/View Initializer")]
    public sealed class ViewInitializer : MonoViewInitializerBase
    {
        [SerializeField] private InitializeComponent<IView>[] _viewComponents;
        [SerializeField] private InitializeComponent<IViewModel> _viewModelComponent;
        
        private IView[] _views;
        private IViewModel _viewModel;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
        [Inject] private DIContainer _diContainer;
#endif

        protected override IView[] Views => _views;

        protected override IViewModel ViewModel => _viewModel;
        
        private void Constructor()
        {
            _views = new IView[_viewComponents.Length];
            
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

            _viewModel = _viewModelComponent.Resolve switch
            { 
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                InitializeComponent.Resolve.Di => _diContainer.Resolve(_viewModelComponent.Type) as IViewModel, 
#endif
                InitializeComponent.Resolve.Mono => _viewModelComponent.Mono as IViewModel,
                InitializeComponent.Resolve.References => _viewModelComponent.References,
                InitializeComponent.Resolve.ScriptableObject => _viewModelComponent.Scriptable as IViewModel,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void OnValidate()
        {
            if (_viewComponents is not null)
            {
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
            
            switch (_viewModelComponent?.Resolve)
            {
                case InitializeComponent.Resolve.Mono:
                    _viewModelComponent.Type = null;
                    _viewModelComponent.References = null;
                    _viewModelComponent.Scriptable = null;
                    break;
                
                case InitializeComponent.Resolve.References:
                    _viewModelComponent.Type = null;
                    _viewModelComponent.Mono = null;
                    _viewModelComponent.Scriptable = null;
                    break;
                
                case InitializeComponent.Resolve.ScriptableObject:
                    _viewModelComponent.Type = null;
                    _viewModelComponent.Mono = null;
                    _viewModelComponent.References = null;
                    break;
                
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    _viewModelComponent.Mono = null;
                    _viewModelComponent.References = null;
                    _viewModelComponent.Scriptable = null;
                    break;
#endif
            }
        }

        private void Awake()
        {
            Constructor();
            Initialize();
        }
    }
}