using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    public abstract class Binder : IBinder
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly global::Unity.Profiling.ProfilerMarker _bindMarker = new("Binder.Bind");
        private static readonly global::Unity.Profiling.ProfilerMarker _unbindMarker = new("Binder.Unbind)");
#endif
        protected virtual bool IsBind => true;
        
        public void Bind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                if (!IsBind) return;
                
                viewModel.AddBinder(this, id);
                OnBound(viewModel, id);
            }
        }
        
        protected virtual void OnBound(IViewModel viewModel, string id) { }
        
        public void Unbind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (!IsBind) return;
                
                viewModel.RemoveBinder(this, id);
                OnUnbound(viewModel, id);
            }
        }
        
        protected virtual void OnUnbound(IViewModel viewModel, string id) { }
    }
}