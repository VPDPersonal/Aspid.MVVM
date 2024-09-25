#nullable disable
using UnityEngine;
using Unity.Profiling;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono
{
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        
        public void Bind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
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
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
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