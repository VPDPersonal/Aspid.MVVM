using System;
using UnityEngine;
#if ASPID_MVVM_ZENJECT_INTEGRATION
using Inject = Zenject.InjectAttribute;
using DIContainer = Zenject.DiContainer;
#elif ASPID_MVVM_VCONTAINER_INTEGRATION
using Inject = VContainer.InjectAttribute;
using DIContainer = VContainer.IObjectResolver;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer")]
    public sealed class ViewInitializer : ViewInitializerBase
    {
        [SerializeField] private bool _isDisposeViewOnDestroy;
        [SerializeField] private InitializeComponent<IView>[] _viewComponents;
        
        [SerializeField] private bool _isDisposeViewModelOnDestroy;
        [SerializeField] private InitializeComponent<IViewModel> _viewModelComponent;
        
        [SerializeField] private InitializeStage _initializeStage  = InitializeStage.Awake;
        [SerializeField] private bool _isDeinitialize;
        
        private IView[] _views;
        private bool _isConstructed;
        private IViewModel _viewModel;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
        [Inject] private DIContainer _diContainer;
#endif
        
        public bool IsInitialized { get; private set; }
        
        private void Constructor()
        {
            if (_isConstructed) return;
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

            _isConstructed = true;
        }
        
        public void Initialize()
        {
            if (_initializeStage != InitializeStage.None)
                throw new Exception($"{_initializeStage} is not None");
            
            InitializeInternal();
        }

        public void Deinitialize()
        {
            if (_initializeStage != InitializeStage.None)
                throw new Exception($"{_initializeStage} is not None");

            DeinitializeInternal();
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
            if (_initializeStage != InitializeStage.Awake) return;
            InitializeInternal();
        }

        private void Start()
        {
            if (_initializeStage != InitializeStage.Start) return;
            InitializeInternal();
        }

        private void OnEnable()
        {
            if (_initializeStage != InitializeStage.OnEnable) return;
            InitializeInternal();
        }

        private void OnDisable()
        {
            if (!_isDeinitialize || _initializeStage != InitializeStage.OnEnable) return;
            DeinitializeInternal();
        }

        private void OnDestroy()
        {
            if (_isDisposeViewOnDestroy)
            {
                foreach (var view in _views)
                    view.DisposeView();
            }
            else if (_isDeinitialize)
            {
                DeinitializeInternal();
            }
            
            if (_isDisposeViewModelOnDestroy) 
                _viewModel.DisposeViewModel();
        }

        private void InitializeInternal()
        {
            if (IsInitialized) return;
            
            Constructor();

            foreach (var view in _views)
                view.Initialize(_viewModel);

            IsInitialized = true;
        }

        private void DeinitializeInternal()
        {
            if (!IsInitialized) return;
            
            foreach (var view in _views)
                view.Deinitialize();

            IsInitialized = false; 
        }

        private enum InitializeStage
        {
            None,
            Awake,
            OnEnable,
            Start,
        }
    }
}