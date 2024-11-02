using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Initializers;

namespace Aspid.UI.MVVM.StarterKit.Views.Initializers
{
    [AddComponentMenu("UI/View Initializers/View Initializer")]
    public sealed class ViewInitializer : MonoViewInitializerBase
    {
        [Header("View")]
        [SerializeField] private InitializeComponent<IView> _viewComponent;
        
        [Header("View Model")]
        [SerializeField] private InitializeComponent<IViewModel> _viewModelComponent;
        
        private IView _view;
        private IViewModel _viewModel;
        
#if ASPID_UI_ZENJECT_INTEGRATION
        [Zenject.Inject] private Zenject.DiContainer _diContainer;
#elif ASPID_UI_VCONTAINER_INTEGRATION
        [VContainer.Inject] private VContainer.IObjectResolver _diContainer;
#endif

        protected override IView View => _view;

        protected override IViewModel ViewModel => _viewModel;
        
        private void Constructor()
        {
            _view = _viewComponent.Resolve switch
            { 
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                InitializeComponent.Resolve.Di => _diContainer.Resolve(_viewComponent.Type) as IView, 
#endif
                InitializeComponent.Resolve.Mono => _viewComponent.Mono as IView,
                InitializeComponent.Resolve.References => _viewComponent.References,
                _ => throw new ArgumentOutOfRangeException()
            };

            _viewModel = _viewModelComponent.Resolve switch
            { 
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                InitializeComponent.Resolve.Di => _diContainer.Resolve(_viewModelComponent.Type) as IViewModel, 
#endif
                InitializeComponent.Resolve.Mono => _viewModelComponent.Mono as IViewModel,
                InitializeComponent.Resolve.References => _viewModelComponent.References,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void OnValidate()
        {
            switch (_viewComponent?.Resolve)
            {
                case InitializeComponent.Resolve.Mono:
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                    _viewComponent.Type = null;
#endif
                    _viewComponent.References = null;
                    break;
                
                case InitializeComponent.Resolve.References:
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                    _viewComponent.Type = null;
#endif
                    _viewComponent.Mono = null;
                    break;
                
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    _viewComponent.Mono = null;
                    _viewComponent.References = null;
                    break;
#endif
            }
            
            switch (_viewModelComponent?.Resolve)
            {
                case InitializeComponent.Resolve.Mono:
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                    _viewModelComponent.Type = null;
#endif
                    _viewModelComponent.References = null;
                    break;
                
                case InitializeComponent.Resolve.References:
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                    _viewModelComponent.Type = null;
#endif
                    _viewModelComponent.Mono = null;
                    break;
                
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
                case InitializeComponent.Resolve.Di:
                    _viewModelComponent.Mono = null;
                    _viewModelComponent.References = null;
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