using UltimateUI.MVVM.ViewModels;
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
using Unity.Profiling;
#endif

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class Binder : IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _bindMarker = new("Binder.Bind");
        private static readonly ProfilerMarker _unbindMarker = new("Binder.Unbind)");
#endif
        public void Bind(IViewModel viewModel, string id)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                viewModel.AddBinder(this, id);
                OnBound(viewModel, id);
            }
        }
        
        protected virtual void OnBound(IViewModel viewModel, string id) { }
        
        public void Unbind(IViewModel viewModel, string id)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                viewModel.RemoveBinder(this, id);
                OnUnbound(viewModel, id);
            }
        }
        
        protected virtual void OnUnbound(IViewModel viewModel, string id) { }
    }
}