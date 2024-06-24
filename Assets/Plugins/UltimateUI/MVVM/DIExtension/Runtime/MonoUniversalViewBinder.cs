using System;
using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.ViewBinders;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.DIExtension
{
    public sealed class MonoUniversalViewBinder : MonoViewBinderBase
    {
        [Header("View")]
        [SerializeField] private Resolve _viewResolver;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolver), Resolve.Di)]
#endif
#if ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolver), Resolve.Di)]
#endif
        [SerializeField] private SerializableMonoScript<IView> _viewType;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolver), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolver), Resolve.Mono)]
#endif
        [SerializeField] private SerializableInterface<IView> _monoView;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolver), Resolve.References)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolver), Resolve.References)]
#endif
        [SerializeReference] private IView _viewReferences;

        [Header("View Model")]
        [SerializeField] private Resolve _viewModelResolver;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolver), Resolve.Di)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolver), Resolve.Di)]
#endif
        [SerializeField] private SerializableInterface<IViewModel> _monoViewModel;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolver), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolver), Resolve.Mono)]
#endif
        [SerializeField] private SerializableMonoScript<IViewModel> _viewModelType;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolver), Resolve.References)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolver), Resolve.References)]
#endif
        [SerializeReference] private IViewModel _viewModelReferences;

        [Header("Bind Order")]
        [SerializeField] private BindOrder _bindOrder;

        private IView _view;
        private IViewModel _viewModel;
        
        protected override IView View => _view;

        protected override IViewModel ViewModel => _viewModel;

#if ULTIMATE_UI_VCONTAINER_INTEGRATION
        [VContainer.Inject]
        private void Constructor(VContainer.IObjectResolver diContainer)
        {
            _view = _viewResolver switch
            {
                Resolve.Di => diContainer.Resolve(_viewType) as IView,
                Resolve.Mono => _monoView.Instance,
                Resolve.References => _viewReferences,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _viewModel = _viewModelResolver switch
            {
                Resolve.Di => diContainer.Resolve(_viewModelType) as IViewModel,
                Resolve.Mono => _monoViewModel.Instance,
                Resolve.References => _viewModelReferences,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
#elif ULTIMATE_UI_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private void Constructor(Zenject.DiContainer diContainer)
        {
            _view = _viewResolver switch
            {
                Resolve.Di => diContainer.Resolve(_viewType) as IView,
                Resolve.Mono => _monoView.Instance,
                Resolve.References => _viewReferences,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _viewModel = _viewModelResolver switch
            {
                Resolve.Di => diContainer.Resolve(_viewModelType) as IViewModel,
                Resolve.Mono => _monoViewModel.Instance,
                Resolve.References => _viewModelReferences,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
#endif
        
        private void OnValidate()
        {
            switch (_viewResolver)
            {
                case Resolve.Di:
                    _monoView = null;
                    _viewReferences = null;
                    break;
                
                case Resolve.Mono:
                    _viewType = null;
                    _viewReferences = null;
                    break;
                
                case Resolve.References:
                    _viewType = null;
                    _monoView = null;
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
            
            switch (_viewModelResolver)
            {
                case Resolve.Di:
                    _monoViewModel = null;
                    _viewModelReferences = null;
                    break;
                
                case Resolve.Mono:
                    _viewModelType = null;
                    _viewModelReferences = null;
                    break;
                
                case Resolve.References:
                    _viewModelType = null;
                    _monoViewModel = null;
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
            
#if !ULTIMATE_UI_VCONTAINER_INTEGRATION && !ULTIMATE_UI_ZENJECT_INTEGRATION
            throw new Exception("There must be integration with VContainer or Zenject");
#endif
        }
        
#if ULTIMATE_UI_VCONTAINER_INTEGRATION || ULTIMATE_UI_ZENJECT_INTEGRATION
        private void Awake() => Bind(BindOrder.Awake);

        private void OnEnable() => Bind(BindOrder.OnEnable);

        private void OnDisable()
        {
            if (_bindOrder == BindOrder.OnEnable)
                Unbind();
        }

        private void Start() => Bind(BindOrder.Start);

        private void OnDestroy()
        {
            if (_bindOrder != BindOrder.OnEnable)
                Unbind();
        }

        private void Bind(BindOrder bindOrder)
        {
            if (bindOrder == _bindOrder)
                Bind();
        }
#else
        private void Awake() =>
            throw new Exception("There must be integration with VContainer or Zenject");
#endif
        
        private enum Resolve
        {
            Di,
            Mono,
            References,
        }

        private enum BindOrder
        {
            Awake,
            Start,
            OnEnable,
        }
    }
}