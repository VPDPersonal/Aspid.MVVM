#nullable disable
using UnityEngine;
using UltimateUI.MVVM.ViewModels;

namespace UltimateUI.MVVM.Unity
{
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly global::Unity.Profiling.ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly global::Unity.Profiling.ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        
        public void Bind(IViewModel viewModel, string id)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                OnBinding(viewModel, id);
                viewModel.AddBinder(this, id);
                OnBound(viewModel, id);
            }
        }

        partial void OnBinding(IViewModel viewModel, string id);
        
        protected virtual void OnBound(IViewModel viewModel, string id) { }
        
        public void Unbind(IViewModel viewModel, string id)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                OnUnbinding(viewModel, id);
                viewModel.RemoveBinder(this, id);
                OnUnbound(viewModel, id);
            }
        }
        
        partial void OnUnbinding(IViewModel viewModel, string id);
        
        protected virtual void OnUnbound(IViewModel viewModel, string id) { }
    }
}