using System;
using UnityEngine;
using Aspid.MVVM.Mono;

#if ASPID_MVVM_ZENJECT_INTEGRATION
using DIContainer = Zenject.DiContainer;
#elif ASPID_MVVM_VCONTAINER_INTEGRATION
using DIContainer = VContainer.IObjectResolver;
#endif

namespace Aspid.MVVM.StarterKit.Views.Initializers
{
    [AddComponentMenu("MVVM/View Initializers/View Initializer")]
    public sealed class ViewInitializer : MonoViewInitializerBase
    {
        [Header("View")]
        [SerializeField] private InitializeComponent<IView> _viewComponent;
        
        [Header("View Model")]
        [SerializeField] private InitializeComponent<IViewModel> _viewModelComponent;
        
        private IView _view;
        private IViewModel _viewModel;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
        [VContainer.Inject] private DIContainer _diContainer;
#endif

        protected override IView View => _view;

        protected override IViewModel ViewModel => _viewModel;
        
        private void Constructor()
        {
            _view = _viewComponent.Resolve switch
            { 
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                InitializeComponent.Resolve.Di => _diContainer.Resolve(_viewComponent.Type) as IView, 
#endif
                InitializeComponent.Resolve.Mono => _viewComponent.Mono as IView,
                InitializeComponent.Resolve.References => _viewComponent.References,
                InitializeComponent.Resolve.ScriptableObject => _viewComponent.Scriptable as IView,
                _ => throw new ArgumentOutOfRangeException()
            };

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
            switch (_viewComponent?.Resolve)
            {
                case InitializeComponent.Resolve.Mono:
                    _viewComponent.Type = null;
                    _viewComponent.References = null;
                    _viewComponent.Scriptable = null;
                    break;
                
                case InitializeComponent.Resolve.References:
                    _viewComponent.Type = null;
                    _viewComponent.Mono = null;
                    _viewComponent.Scriptable = null;
                    break;
                
                case InitializeComponent.Resolve.ScriptableObject:
                    _viewComponent.Type = null;
                    _viewComponent.Mono = null;
                    _viewComponent.References = null;
                    break;
                
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    _viewComponent.Mono = null;
                    _viewComponent.References = null;
                    _viewComponent.Scriptable = null;
                    break;
#endif
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