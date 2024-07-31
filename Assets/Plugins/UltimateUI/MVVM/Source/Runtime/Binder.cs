using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class Binder : IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly global::Unity.Profiling.ProfilerMarker _onBoundMarker = new("Binder.OnBound");
        private static readonly global::Unity.Profiling.ProfilerMarker _onUnboundMarker = new("Binder.OnUnbound");
#endif

        void IBinder.OnBound()
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_onBoundMarker.Auto()) 
#endif
            {
                OnBound();
            }
        }

        protected virtual void OnBound() { }

        void IBinder.OnUnbound()
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_onUnboundMarker.Auto())
#endif
            {
                OnUnbound();
            }
        }

        protected virtual void OnUnbound() { }
        
        // TODO Delete
        public void Bind(IViewModel viewModel, string id) => viewModel.AddBinder(this, id);
        
        // TODO Delete
        public void Unbind(IViewModel viewModel, string id) => viewModel.RemoveBinder(this, id);
    }
}