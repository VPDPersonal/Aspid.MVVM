#nullable disable
using UnityEngine;
using UltimateUI.MVVM.ViewModels;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Views
{
    public abstract class View : IView
    {
#if !ULTIMATE_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly global::Unity.Profiling.ProfilerMarker _initializeMarker = new("View.Initialize");
#endif
        
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

        protected static void InitializeChildViewSafely(IView view, IViewModel viewModel) =>
            view?.Initialize(viewModel);

        protected static void InitializeChildViewSafely<T>(T[] views, IViewModel viewModel)
            where T : IView
        {
            if (views == null) return;

            foreach (var view in views)
                view.Initialize(viewModel);
        }
    }
}