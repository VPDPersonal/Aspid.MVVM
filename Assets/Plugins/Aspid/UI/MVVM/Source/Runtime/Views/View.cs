using System;
using Unity.Profiling;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Views
{
    public abstract class View : IView, IDisposable
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _initializeMarker = new("View.Initialize");
        private static readonly ProfilerMarker _deinitializationMarker = new("View.Deinitialization");
#endif
        public IViewModel? ViewModel { get; private set; }
        
        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                ViewModel = viewModel;
                InitializeIternal(viewModel);
            }
        }
        
        protected abstract void InitializeIternal(IViewModel viewModel);

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
        
        protected abstract void DeinitializeIternal();
        
        public virtual void Dispose() => Deinitialize();
    }
}