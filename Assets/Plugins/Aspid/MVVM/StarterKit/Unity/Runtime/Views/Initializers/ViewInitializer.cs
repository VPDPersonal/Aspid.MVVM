using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer")]
    public sealed class ViewInitializer : ViewInitializerBase
    {
        [SerializeField] private bool _isDisposeViewOnDestroy = true;
        [SerializeField] private InitializeComponent<IView>[] _viewComponents;
        
        [SerializeField] private bool _isDisposeViewModelOnDestroy;
        [SerializeField] private InitializeComponent<IViewModel> _viewModelComponent;
        
        [SerializeField] private InitializeStage _initializeStage  = InitializeStage.Awake;
        [SerializeField] private bool _isDeinitialize = true;
        
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
            {
                var view = Get(_viewComponents[i]);
                
                if (view is IComponentInitializable viewInitializable)
                    viewInitializable.Initialize();
                    
                _views[i] = view;
            }

            var viewModel = Get(_viewModelComponent);
            
            if (viewModel is IComponentInitializable viewModelInitializable)
                viewModelInitializable.Initialize();
            
            ViewModel = viewModel;
            
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
        
        public void Initialize()
        {
            if (_initializeStage != InitializeStage.Manual)
                throw new Exception($"{_initializeStage} is not Manual");
            
            InitializeInternal();
        }

        public void Deinitialize()
        {
            if (_initializeStage != InitializeStage.Manual)
                throw new Exception($"{_initializeStage} is not Manual");

            DeinitializeInternal();
        }

        private void OnValidate()
        {
            if (_viewComponents is not null)
            {
                foreach (var viewComponent in _viewComponents)
                    viewComponent?.Validate();
            }
            
            _viewModelComponent?.Validate();
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
                ViewModel.DisposeViewModel();
        }

        private void InitializeInternal()
        {
            if (IsInitialized) return;
            
            Constructor();

            foreach (var view in _views)
                view.Initialize(ViewModel);

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
            Manual,
            Awake,
            OnEnable,
            Start,
        }
    }
}