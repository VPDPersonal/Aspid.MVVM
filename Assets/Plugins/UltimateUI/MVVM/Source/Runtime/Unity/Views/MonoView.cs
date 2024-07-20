using UnityEngine;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Unity.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly global::Unity.Profiling.ProfilerMarker _initializeMarker = new("MonoView.Initialize");
#endif
        
        protected virtual void OnValidate() =>
            ViewUtility.ValidateBinders(this);

        public void Initialize(IViewModel viewModel)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                InitializeIternal(viewModel);
            }
        }

        protected abstract void InitializeIternal(IViewModel viewModel);
    }
}