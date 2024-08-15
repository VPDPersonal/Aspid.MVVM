#nullable disable
using System;
using Unity.Profiling;
using UltimateUI.MVVM.ViewModels;

using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class View : IView, IDisposable
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _initializeMarker = new("View.Initialize");
        private static readonly ProfilerMarker _deinitializationMarker = new("View.Deinitialization");
#endif
        private IViewModel _viewModel;
        
        public void Initialize(IViewModel viewModel)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                _viewModel = viewModel;
                InitializeIternal(viewModel);
            }
        }
        
        protected abstract void InitializeIternal(IViewModel viewModel);

        public void Deinitialize(IViewModel viewModel)
        {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializationMarker.Auto())
#endif
            {
                DeinitializeIternal(viewModel);
                _viewModel = null;
            }
        }
        
        protected abstract void DeinitializeIternal(IViewModel viewModel);
        
        public virtual void Dispose()
        {
            if (_viewModel == null) return;
            Deinitialize(_viewModel);
        }
        
        protected static void BindSafely<T>(T binder, IViewModel viewModel, string id)
            where T : Object, IBinder
        {
            if (!binder) return;
            binder.Bind(viewModel, id);
        }
        
        protected static void BindSafely(IBinder binder, IViewModel viewModel, string id) =>
            binder?.Bind(viewModel, id);
        
        protected static void BindSafely<T>(T[] binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders == null) return;

            foreach (var binder in binders)
                binder.Bind(viewModel, id);
        }
    }
}