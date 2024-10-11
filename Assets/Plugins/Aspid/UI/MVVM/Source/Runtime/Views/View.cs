using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    public abstract class View : IView, IDisposable
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _initializeMarker = new("View.Initialize");
        private static readonly Unity.Profiling.ProfilerMarker _deinitializationMarker = new("View.Deinitialization");
#endif
        public IViewModel? ViewModel { get; private set; }
        
        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
                if (ViewModel is not null) throw new InvalidOperationException("View is already initialized.");
                
                ViewModel = viewModel;
                InitializeIternal(viewModel);
            }
        }
        
        protected virtual void InitializeIternal(IViewModel viewModel) =>
            throw new NotImplementedException("This method must be implemented in the inheritor");

        public void Deinitialize()
        {
            if (ViewModel == null) return;
            
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializationMarker.Auto())
#endif
            {
                DeinitializeIternal();
                ViewModel = null;
            }
        }
        
        protected virtual void DeinitializeIternal() =>
            throw new NotImplementedException("This method must be implemented in the inheritor");
        
        public virtual void Dispose() => Deinitialize();
    }
}