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

        bool IBinder.Bind(IViewModel viewModel, string propertyName)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                return Bind(viewModel, propertyName);
            }
        }
        
        bool IBinder.Unbind(IViewModel viewModel, string propertyName)
        {
            using (_unbindMarker.Auto())
            {
                var result = Unbind(viewModel, propertyName);
                if (result) ReleaseBinding();
            
                return result;
            }
        }

        protected virtual bool Bind(IViewModel viewModel, string propertyName)
        {
            viewModel.AddBinder(this, propertyName);
            return true;
        }
        
        public bool Unbind(IViewModel viewModel, string propertyName)
        {
            viewModel.RemoveBinder(this, propertyName);
            return true;
        }

        protected virtual void ReleaseBinding() { }
    }
}