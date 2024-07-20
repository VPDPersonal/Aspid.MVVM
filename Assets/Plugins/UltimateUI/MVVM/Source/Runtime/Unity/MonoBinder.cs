#nullable disable
using UnityEngine;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity
{
    public abstract class MonoBinder : MonoBehaviour, IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly global::Unity.Profiling.ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly global::Unity.Profiling.ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        
#if UNITY_EDITOR
        [field: SerializeField] 
        public string Id { get; set; }
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