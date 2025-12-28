using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
// ReSharper disable SuspiciousTypeConversion.Global
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer")]
    public sealed class ViewInitializer : ViewInitializerBase
    {
        [SerializeField] private bool _isDisposeViewModelOnDestroy;
        [SerializeField] private ViewModelInitializeComponent _viewModelComponent;
        
        [SerializeField] private InitializeStage _initializeStage = InitializeStage.Awake;
        [SerializeField] private bool _isDeinitialize = true;
        
        private IViewModel _viewModel;

        public override IViewModel ViewModel
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    return GetViewModel();
                }
#endif
                
                return _viewModel ??= GetViewModel();
                
                IViewModel GetViewModel()
                {
                    var viewModel = GetFromInitializeComponent(_viewModelComponent);
                    
                    if (viewModel is IComponentInitializable viewModelInitializable)
                        viewModelInitializable.Initialize();
                    
                    return viewModel;
                }
            }
        }

        #region Di Integration
#if ASPID_MVVM_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private void ZenjectConstructor()
        {
            if (_initializeStage is not InitializeStage.DiConstructor) return;
            InitializeInternal();
        }
#endif
        
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [VContainer.Inject]
        private void VContainerConstructor()
        {
            if (_initializeStage is not InitializeStage.DiConstructor) return;
            InitializeInternal();
        }
#endif
        #endregion

        #region Initialize Methods
        public void Initialize()
        {
            if (_initializeStage != InitializeStage.Manual)
                throw new Exception($"{_initializeStage} is not Manual");
            
            InitializeInternal();
        }
        
        private void InitializeInternal()
        {
            if (IsInitialized) return;
            
            foreach (var view in Views)
                view.Initialize(ViewModel);

            IsInitialized = true;
        }

        public void Deinitialize()
        {
            if (_initializeStage != InitializeStage.Manual)
                throw new Exception($"{_initializeStage} is not Manual");

            DeinitializeInternal();
        }
        
        private void DeinitializeInternal()
        {
            if (!IsInitialized) return;
            
            foreach (var view in Views)
                view.Deinitialize();

            IsInitialized = false; 
        }
        #endregion

        #region Unity Methods
        protected override void OnValidate()
        {
            base.OnValidate();
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
            if (IsDisposeViewOnDestroy)
            {
                if (Views is not null)
                {
                    foreach (var view in Views)
                        view.DisposeView();
                }
            }
            else if (_isDeinitialize)
            {
                DeinitializeInternal();
            }
            
            if (_isDisposeViewModelOnDestroy) 
                ViewModel.DisposeViewModel();
        }
        #endregion
        
        private enum InitializeStage
        {
            Manual,
            Awake,
            OnEnable,
            Start,
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
            DiConstructor,
#endif
        }
    }
}