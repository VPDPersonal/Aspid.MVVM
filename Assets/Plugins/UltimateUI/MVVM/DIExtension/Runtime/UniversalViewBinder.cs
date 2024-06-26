using System;
using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.ViewBinders;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.DIExtension
{
    public sealed class UniversalViewBinder : MonoViewBinderBase
    {
        [Header("View")]
        [SerializeField] private Resolve _viewResolve;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolve), Resolve.Di)]
#endif
#if ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolve), Resolve.Di)]
#endif
        [SerializeField] private SerializableMonoScript<IView> _viewType;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolve), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolve), Resolve.Mono)]
#endif
        [SerializeField] private SerializableInterface<IView> _monoView;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewResolve), Resolve.References)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewResolve), Resolve.References)]
#endif
        [SerializeReference] private IView _referencesView;
        
        [Header("View Model")]
        [SerializeField] private Resolve _viewModelResolve;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolve), Resolve.Di)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolve), Resolve.Di)]
#endif
        [SerializeField] private SerializableMonoScript<IViewModel> _viewModelType;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolve), Resolve.Mono)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolve), Resolve.Mono)]
#endif
        [SerializeField] private SerializableInterface<IViewModel> _monoViewModel;
        
#if ULTIMATE_UI_TRI_INSPECTOR_INTEGRATION
        [TriInspector.ShowIf(nameof(_viewModelResolve), Resolve.References)]
#elif ULTIMATE_UI_ODIN_INSPECTOR_INTEGRATION
        [Sirenix.OdinInspector.ShowIf(nameof(_viewModelResolve), Resolve.References)]
#endif
        [SerializeReference] private IViewModel _referencesViewModel;

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
            _view = _viewResolve switch
            {
                Resolve.Di => diContainer.Resolve(_viewType) as IView,
                Resolve.Mono => _monoView.Instance,
                Resolve.References => _referencesView,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _viewModel = _viewModelResolve switch
            {
                Resolve.Di => diContainer.Resolve(_viewModelType) as IViewModel,
                Resolve.Mono => _monoViewModel.Instance,
                Resolve.References => _referencesViewModel,
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
            switch (_viewResolve)
            {
                case Resolve.Di:
                    _monoView = null;
                    _referencesView = null;
                    break;
                
                case Resolve.Mono:
                    _viewType = null;
                    _referencesView = null;
                    break;
                
                case Resolve.References:
                    _viewType = null;
                    _monoView = null;
                    break;
                
                default: throw new ArgumentOutOfRangeException();
            }
            
            switch (_viewModelResolve)
            {
                case Resolve.Di:
                    _monoViewModel = null;
                    _referencesViewModel = null;
                    break;
                
                case Resolve.Mono:
                    _viewModelType = null;
                    _referencesViewModel = null;
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