#nullable disable
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class Binder : IBinder
    {
#if UNITY_EDITOR && !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new($"{nameof(Binder)}.{nameof(Bind)}");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new($"{nameof(Binder)}.{nameof(Unbind)}");
#endif

        public void Bind(IViewModel viewModel, string id)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                viewModel.AddBinder(this, id);
            }
        }
        
        void IBinder.Unbind(IViewModel viewModel, string propertyName)
        {
            using (_unbindMarker.Auto())
            {
                Unbind(viewModel, propertyName);
                ReleaseBinding();
            }
        }
        
        public void Unbind(IViewModel viewModel, string propertyName) =>
            viewModel.RemoveBinder(this, propertyName);

        protected virtual void ReleaseBinding() { }
    }
}