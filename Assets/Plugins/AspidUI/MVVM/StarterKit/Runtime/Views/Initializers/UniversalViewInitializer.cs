#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
using System;
using UnityEngine;
using AspidUI.MVVM.Views;
using AspidUI.MVVM.ViewModels;
using AspidUI.MVVM.Unity.Initializers;
using AspidUI.MVVM.StarterKit.Utilities;

namespace AspidUI.MVVM.StarterKit.Views.Initializers
{
    [AddComponentMenu("UI/View Initializers/Universal View Initializer")]
    public sealed class UniversalViewInitializer : MonoViewInitializerBase
    {
        [Header("View")]
        [SerializeField] private Component<IView> _viewComponent;

        [Header("View Model")]
        [SerializeField] private Component<IViewModel> _viewModelComponent;

        private IView _view;
        private IViewModel _viewModel;
        
        protected override IView View => _view;

        protected override IViewModel ViewModel => _viewModel;

        #region Constructor
#if ASPID_UI_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private void Constructor(Zenject.DiContainer diContainer)
#elif ASPID_UI_VCONTAINER_INTEGRATION
        [VContainer.Inject]
        private void Constructor(VContainer.IObjectResolver diContainer)
#endif
        {
            _view = _viewComponent.Resolve switch
            {
                Resolve.Di => diContainer.Resolve(_viewComponent.Type) as IView,
                Resolve.Mono => _viewComponent.Mono.Instance,
                Resolve.References => _viewComponent.References,
                _ => throw new ArgumentOutOfRangeException()
            };

            _viewModel = _viewModelComponent.Resolve switch
            {
                Resolve.Di => diContainer.Resolve(_viewModelComponent.Type) as IViewModel,
                Resolve.Mono => _viewModelComponent.Mono.Instance,
                Resolve.References => _viewModelComponent.References,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        #endregion

        private void OnValidate()
        {
            switch (_viewComponent.Resolve)
            {
                case Resolve.Di:
                    _viewComponent.Mono = null;
                    _viewComponent.References = null;
                    break;
                
                case Resolve.Mono:
                    _viewComponent.Type = null;
                    _viewComponent.References = null;
                    break;
                
                case Resolve.References:
                    _viewComponent.Type = null;
                    _viewComponent.Mono = null;
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
            
            switch (_viewModelComponent.Resolve)
            {
                case Resolve.Di:
                    _viewModelComponent.Mono = null;
                    _viewModelComponent.References = null;
                    break;
                
                case Resolve.Mono:
                    _viewModelComponent.Type = null;
                    _viewModelComponent.References = null;
                    break;
                
                case Resolve.References:
                    _viewModelComponent.Type = null;
                    _viewModelComponent.Mono = null;
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void Awake()
        {
            Initialize();
        }
        
        private enum Resolve
        {
            Di,
            Mono,
            References,
        }
        
        [Serializable]
        private sealed class Component<T>
            where T : class
        {
            [field: SerializeField] 
            public Resolve Resolve { get; set; }
        
#if ASPID_UI_TRI_INSPECTOR_INTEGRATION
            [field: TriInspector.ShowIf(nameof(Resolve), Resolve.Di)]
#elif ASPID_UI_ODIN_INSPECTOR_INTEGRATION
            [field: Sirenix.OdinInspector.ShowIf(nameof(Resolve), Resolve.Di)]
#endif
            [field: SerializeField] 
            public SerializableMonoScript<T> Type { get; set; }
        
#if ASPID_UI_TRI_INSPECTOR_INTEGRATION
            [field: TriInspector.ShowIf(nameof(Resolve), Resolve.Mono)]
#elif ASPID_UI_ODIN_INSPECTOR_INTEGRATION
            [field: Sirenix.OdinInspector.ShowIf(nameof(Resolve), Resolve.Mono)]
#endif
            [field: SerializeField] 
            public SerializableInterface<T> Mono { get; set; }
        
#if ASPID_UI_TRI_INSPECTOR_INTEGRATION
            [field: TriInspector.ShowIf(nameof(Resolve), Resolve.References)]
#elif ASPID_UI_ODIN_INSPECTOR_INTEGRATION
            [field: Sirenix.OdinInspector.ShowIf(nameof(Resolve), Resolve.References)]
#endif
            [field: SerializeReference]
            public T References { get; set; }
        }
    }
}
#endif