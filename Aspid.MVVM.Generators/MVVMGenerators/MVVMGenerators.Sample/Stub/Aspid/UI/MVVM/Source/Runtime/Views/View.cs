using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    /// <summary>
    /// Abstract class for a View implementing the <see cref="IView"/> interface.
    /// Provides methods for initializing and deinitializing a View with a <see cref="IViewModel"/> for binding.
    /// </summary>
    public abstract class View : IView, IDisposable
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _initializeMarker = new("View.Initialize");
        private static readonly Unity.Profiling.ProfilerMarker _deinitializationMarker = new("View.Deinitialization");
#endif
        
        /// <summary>
        /// Gets the associated ViewModel.
        /// If the view is not initialized, it may return <c>null</c>.
        /// </summary>
        public IViewModel? ViewModel { get; private set; }
        
        /// <summary>
        /// Initializes the view with the specified <see cref="IViewModel"/> for binding.
        /// </summary>
        /// <param name="viewModel">The <see cref="IViewModel"/> object used to initialize the View.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="viewModel"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the view is already initialized.</exception>
        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
                if (ViewModel is not null) throw new InvalidOperationException("View is already initialized.");
                
                ViewModel = viewModel;
                InitializeInternal(viewModel);
            }
        }

        /// <summary>
        /// Abstract method for internal view initialization. 
        /// Must be overridden in derived classes to implement specific initialization logic.
        /// </summary>
        /// <param name="viewModel">The <see cref="IViewModel"/> object used to initialize the View.</param>
        protected abstract void InitializeInternal(IViewModel viewModel);

        /// <summary>
        /// Deinitializes the view, resetting the associated <see cref="ViewModel"/> to null.
        /// </summary>
        public void Deinitialize()
        {
            if (ViewModel == null) return;
            
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializationMarker.Auto())
#endif
            {
                DeinitializeInternal();
                ViewModel = null;
            }
        }

        /// <summary>
        /// Abstract method for internal view deinitialization. 
        /// Must be overridden in derived classes to implement specific deinitialization logic.
        /// </summary>
        protected abstract void DeinitializeInternal();
        
        /// <summary>
        /// Releases the resources used by the view.
        /// Can be overridden by derived classes.
        /// Calls <see cref="Deinitialize"/> to ensure proper cleanup.
        /// </summary>
        public virtual void Dispose() => Deinitialize();
    }
}