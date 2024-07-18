using UnityEngine;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _initializeMarker = new("MonoView.Initialize");
#endif
        
        protected virtual void OnValidate() =>
            ViewUtility.ValidateBinders(this);

        void IView.Initialize(IViewModel viewModel)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                Initialize(viewModel);
            }
        }

        public abstract void Initialize(IViewModel viewModel);
    }
}