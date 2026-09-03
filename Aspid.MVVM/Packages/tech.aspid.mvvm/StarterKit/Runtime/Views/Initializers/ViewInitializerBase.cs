using System;
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
        /// Disposes the already resolved views if <see cref="IsDisposeViewOnDestroy"/> is set.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (!_isDisposeViewOnDestroy || _views is null) return;

            foreach (var view in _views)
                view.DisposeView();
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
