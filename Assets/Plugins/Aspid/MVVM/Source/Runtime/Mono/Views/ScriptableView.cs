#nullable disable
using System;
using UnityEngine;

namespace Aspid.MVVM.Mono
{
    public abstract class ScriptableView : ScriptableObject, IView
    {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _initializeMarker = new("ScriptableView.Initialize");
        private static readonly Unity.Profiling.ProfilerMarker _deinitializeMarker = new("ScriptableView.Deinitialize");
#endif

        /// <summary>
        /// Gets the associated ViewModel.
        /// May return <c>null</c> if the view is not initialized.
        /// </summary>
        public IViewModel ViewModel { get; private set; }

        /// <summary>
        /// Initializes the view with the specified <see cref="IViewModel"/> for binding.
        /// </summary>
        /// <param name="viewModel">The <see cref="IViewModel"/> object to initialize the View.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewModel"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the view is already initialized.</exception>
        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
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
        /// Must be overridden in a derived class to implement specific initialization logic.
        /// </summary>
        /// <param name="viewModel">The <see cref="IViewModel"/> object to initialize the View.</param>
        protected abstract void InitializeInternal(IViewModel viewModel);

        /// <summary>
        /// Deinitialize the View, resetting the bound <see cref="ViewModel"/>.
        /// </summary>
        public void Deinitialize()
        {
            if (ViewModel is null) return;
            
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializeMarker.Auto())
#endif
            {
                DeinitializeInternal();
                ViewModel = null;
            }
        }
        
        /// <summary>
        /// Abstract method for internal view deinitialization. 
        /// Must be overridden in a derived class to implement specific deinitialization logic.
        /// </summary>
        protected abstract void DeinitializeInternal();
    }
}