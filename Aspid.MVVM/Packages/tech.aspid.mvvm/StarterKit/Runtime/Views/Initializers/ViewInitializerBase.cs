using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="MonoBehaviour"/> that resolves a set of views and initializes them with a ViewModel.
    /// </summary>
    public abstract class ViewInitializerBase : MonoBehaviour
    {
        [Tooltip("Dispose the views when this object is destroyed.")]
        [SerializeField] private bool _isDisposeViewOnDestroy = true;

        [Tooltip("Views to initialize.")]
        [SerializeField] private ViewInitializeComponent[] _viewComponents = Array.Empty<ViewInitializeComponent>();

        private IView[] _views;
        private readonly List<IView> _initializedViews = new();
        private readonly List<IView> _ownedViews = new();
        private bool _isInitializing;

#if ASPID_MVVM_ZENJECT_INTEGRATION
        [Zenject.Inject]
        private Zenject.DiContainer _zenjectContainer;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [VContainer.Inject]
        private VContainer.IObjectResolver _vcontainerContainer;
#endif

        /// <summary>
        /// Gets whether the views are currently initialized.
        /// </summary>
        public bool IsInitialized { get; protected set; }

        /// <summary>
        /// Gets the resolved views. Resolved once in play mode and on every call in edit mode.
        /// </summary>
        public IView[] Views
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) return ResolveViews();
#endif
                return _views ??= ResolveViews();
            }
        }

        /// <summary>
        /// Gets the ViewModel the views are initialized with.
        /// </summary>
        public abstract IViewModel ViewModel { get; }

        /// <summary>
        /// Gets whether the views are disposed when this object is destroyed.
        /// </summary>
        protected bool IsDisposeViewOnDestroy => _isDisposeViewOnDestroy;

        /// <summary>
        /// Keeps the serialized view slots consistent with their resolve mode.
        /// </summary>
        protected virtual void OnValidate()
        {
            foreach (var viewComponent in _viewComponents)
                viewComponent?.Validate();
        }

        /// <summary>
        /// Disposes the acquired views if <see cref="IsDisposeViewOnDestroy"/> is set.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (_isDisposeViewOnDestroy)
                ReleaseViews(dispose: true);
        }

        /// <summary>
        /// Initializes the resolved views and rolls back only the views acquired by this attempt on failure.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind.</param>
        protected void InitializeViews(IViewModel viewModel)
        {
            if (_isInitializing) throw new InvalidOperationException($"{name}: views are being initialized");
            _isInitializing = true;
            try
            {
                foreach (var view in Views)
                {
                    if (view.ViewModel is not null)
                        throw new InvalidOperationException($"{name}: a view is already initialized");

                    _initializedViews.Add(view);
                    view.Initialize(viewModel);
                }

                foreach (var view in _initializedViews)
                {
                    if (!_ownedViews.Contains(view))
                        _ownedViews.Add(view);
                }

                IsInitialized = true;
            }
            catch
            {
                try { DeinitializeViews(); }
                catch { /* Preserve the initialization failure. */ }
                throw;
            }
            finally { _isInitializing = false; }
        }

        /// <summary>
        /// Deinitializes every acquired view, including a partially initialized view.
        /// </summary>
        protected void DeinitializeViews() =>
            ReleaseViews(dispose: false);

        private void ReleaseViews(bool dispose)
        {
            Exception failure = null;
            var views = dispose ? _ownedViews : _initializedViews;
            foreach (var view in views)
            {
                try
                {
                    if (dispose) view.DisposeView();
                    else view.Deinitialize();
                }
                catch (Exception exception) { failure ??= exception; }
            }

            _initializedViews.Clear();
            if (dispose)
                _ownedViews.Clear();

            IsInitialized = false;
            if (failure is not null)
                ExceptionDispatchInfo.Capture(failure).Throw();
        }

        /// <summary>
        /// Resolves the instance of the slot, handing it the DI container first.
        /// </summary>
        /// <typeparam name="T">The resolved type.</typeparam>
        /// <param name="initializeComponent">The slot to resolve.</param>
        /// <returns>The resolved instance, or <see langword="null"/> when the slot is empty.</returns>
        protected T Resolve<T>(InitializeComponent<T> initializeComponent)
            where T : class
        {
#if ASPID_MVVM_ZENJECT_INTEGRATION
            initializeComponent.ZenjectContainer = _zenjectContainer;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
            initializeComponent.VContainerContainer = _vcontainerContainer;
#endif
            return initializeComponent.Resolve();
        }

        private IView[] ResolveViews()
        {
            var views = new IView[_viewComponents.Length];

            for (var i = 0; i < views.Length; i++)
            {
                var view = Resolve(_viewComponents[i]) ??
                    throw new InvalidOperationException($"{name}: view {i} is not assigned");

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (view is IComponentInitializable initializable)
                    initializable.Initialize();

                views[i] = view;
            }

            return views;
        }
    }
}
