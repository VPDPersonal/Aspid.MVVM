using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ViewInitializerBase"/> that resolves its ViewModel from a serialized slot and
    /// initializes the views at the chosen lifecycle stage.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer")]
    [AddBinderContextMenu(typeof(MonoView), Path = "Add View Initializers/View Initializer")]
    public sealed class ViewInitializer : ViewInitializerBase
    {
        [Tooltip("Dispose the ViewModel when this object is destroyed.")]
        [SerializeField] private bool _isDisposeViewModelOnDestroy;

        [Tooltip("ViewModel to initialize the views with.")]
        [SerializeField] private ViewModelInitializeComponent _viewModelComponent = new();

        [Tooltip("Lifecycle stage at which the views are initialized.")]
        [SerializeField] private InitializeStage _initializeStage = InitializeStage.Awake;

        [Tooltip("Deinitialize the views in OnDisable (OnEnable stage) or OnDestroy.")]
        [SerializeField] private bool _isDeinitialize = true;

        private IViewModel _viewModel;

        /// <summary>
        /// Gets the ViewModel. Resolved once in play mode and on every call in edit mode.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the ViewModel slot is empty.</exception>
        public override IViewModel ViewModel
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) return ResolveViewModel();
#endif
                return _viewModel ??= ResolveViewModel();
            }
        }

        /// <summary>
        /// Initializes the views. Allowed only in the <c>Manual</c> stage.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the stage is not <c>Manual</c>.</exception>
        public void Initialize()
        {
            ThrowIfNotManual();
            InitializeInternal();
        }

        /// <summary>
        /// Deinitializes the views. Allowed only in the <c>Manual</c> stage.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the stage is not <c>Manual</c>.</exception>
        public void Deinitialize()
        {
            ThrowIfNotManual();
            DeinitializeInternal();
        }

        /// <inheritdoc/>
        protected override void OnValidate()
        {
            base.OnValidate();
            _viewModelComponent?.Validate();
        }

        /// <inheritdoc/>
        protected override void OnDestroy()
        {
            if (!IsDisposeViewOnDestroy && _isDeinitialize)
                DeinitializeInternal();

            base.OnDestroy();

            if (_isDisposeViewModelOnDestroy)
                _viewModel?.DisposeViewModel();
        }

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

        private void Awake()
        {
            if (_initializeStage is not InitializeStage.Awake) return;
            InitializeInternal();
        }

        private void OnEnable()
        {
            if (_initializeStage is not InitializeStage.OnEnable) return;
            InitializeInternal();
        }

        private void Start()
        {
            if (_initializeStage is not InitializeStage.Start) return;
            InitializeInternal();
        }

        private void OnDisable()
        {
            if (!_isDeinitialize || _initializeStage is not InitializeStage.OnEnable) return;
            DeinitializeInternal();
        }

        private void InitializeInternal()
        {
            if (IsInitialized) return;

            foreach (var view in Views)
                view.Initialize(ViewModel);

            IsInitialized = true;
        }

        private void DeinitializeInternal()
        {
            if (!IsInitialized) return;

            foreach (var view in Views)
                view.Deinitialize();

            IsInitialized = false;
        }

        private IViewModel ResolveViewModel()
        {
            var viewModel = Resolve(_viewModelComponent) ??
                throw new InvalidOperationException($"{name}: ViewModel is not assigned");

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (viewModel is IComponentInitializable initializable)
                initializable.Initialize();

            return viewModel;
        }

        private void ThrowIfNotManual()
        {
            if (_initializeStage is InitializeStage.Manual) return;
            throw new InvalidOperationException($"{name}: stage is {_initializeStage}, not Manual");
        }

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
