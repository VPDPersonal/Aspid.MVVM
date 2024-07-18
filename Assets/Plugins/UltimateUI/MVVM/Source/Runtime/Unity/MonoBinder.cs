#nullable disable
using UnityEngine;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM
{
    public abstract class MonoBinder : MonoBehaviour, IBinder
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new($"{nameof(MonoBinder)}.{nameof(Bind)}");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new($"{nameof(MonoBinder)}.{nameof(Unbind)}");
#endif
        
// #if UNITY_EDITOR
        [field: SerializeField] 
        public string Id { get; set; }
// #endif
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

        public bool Unbind(IViewModel viewModel, string propertyName)
        {
            viewModel.RemoveBinder(this, propertyName);
            return true;
        }
        
        protected virtual void ReleaseBinding() { }
    }
}