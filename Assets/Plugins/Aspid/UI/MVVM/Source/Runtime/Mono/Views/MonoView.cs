#nullable disable
using System;
using UnityEngine;
using Aspid.UI.MVVM.Views;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono.Views
{
    public abstract partial class MonoView : MonoBehaviour, IView, IDisposable
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _initializeMarker = new("MonoView.Initialize");
        private static readonly Unity.Profiling.ProfilerMarker _deinitializationMarker = new("MonoView.Deinitialization");
#endif

        public IViewModel ViewModel { get; private set; }

        protected virtual void OnDestroy() => Deinitialize();

        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
                if (ViewModel is not null) throw new InvalidOperationException("View is already initialized.");
                
                ViewModel = viewModel;
                InitializeIternal(viewModel);
            }
        }

        protected abstract void InitializeIternal(IViewModel viewModel);

        public void Deinitialize()
        {
            if (ViewModel == null) return;
            
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializationMarker.Auto())
#endif
            {
                DeinitializeIternal();
                ViewModel = null;
            }
        }
        
        protected abstract void DeinitializeIternal();

        public virtual void Dispose() => Destroy(gameObject);
    }
}