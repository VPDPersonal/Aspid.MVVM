#nullable disable
using System;
using UnityEngine;
using Unity.Profiling;
using AspidUI.MVVM.Views;
using AspidUI.MVVM.ViewModels;
using Object = UnityEngine.Object;

namespace AspidUI.MVVM.Unity.Views
{
    public abstract class MonoView : MonoBehaviour, IView
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly ProfilerMarker _initializeMarker = new("MonoView.Initialize");
        private static readonly ProfilerMarker _deinitializationMarker = new("MonoView.Deinitialization");
#endif

        public IViewModel ViewModel { get; private set; }

        protected virtual void OnDestroy()
        {
            if (ViewModel == null) return;
            Deinitialize(ViewModel);
        }

        public void Initialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_initializeMarker.Auto())
#endif
            {
                ViewModel = viewModel;
                InitializeIternal(viewModel);
            }
        }

        protected abstract void InitializeIternal(IViewModel viewModel);

        public void Deinitialize(IViewModel viewModel)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_deinitializationMarker.Auto())
#endif
            {
#if UNITY_EDITOR
                if (viewModel != ViewModel) throw new Exception();
#endif
                
                DeinitializeIternal(viewModel);
                ViewModel = null;
            }
        }
        
        protected abstract void DeinitializeIternal(IViewModel viewModel);

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
        
        protected static void UnbindSafely<T>(T binder, IViewModel viewModel, string id)
            where T : Object, IBinder
        {
            if (!binder) return;
            binder.Unbind(viewModel, id);
        }
        
        protected static void UnbindSafely(IBinder binder, IViewModel viewModel, string id) =>
            binder?.Unbind(viewModel, id);
        
        protected static void UnbindSafely<T>(T[] binders, IViewModel viewModel, string id)
            where T : IBinder
        {
            if (binders == null) return;

            foreach (var binder in binders)
                binder.Unbind(viewModel, id);
        }
    }
}