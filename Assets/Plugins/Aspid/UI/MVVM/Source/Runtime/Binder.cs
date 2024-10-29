using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    public abstract partial class Binder : IBinder
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("Binder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("Binder.Unbind)");
#endif
        protected virtual bool IsBind => true;
        
        public void Bind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                if (!IsBind) return;
                ThrowExceptionIfInvalidData(viewModel, id);

                OnBindingDebug(viewModel, id);
                OnBinding(viewModel, id);
                
                viewModel.AddBinder(this, id);
                OnBound(viewModel, id);
            }
        }

        partial void OnBindingDebug(IViewModel viewModel, string id);
        
        protected virtual void OnBinding(IViewModel viewModel, string id) { }
        
        protected virtual void OnBound(IViewModel viewModel, string id) { }
        
        public void Unbind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (!IsBind) return;
                ThrowExceptionIfInvalidData(viewModel, id);

                OnUnbindingDebug(viewModel, id);
                OnUnbinding(viewModel, id);
                
                viewModel.RemoveBinder(this, id);
                OnUnbound(viewModel, id);
            }
        }

        partial void OnUnbindingDebug(IViewModel viewModel, string id);
        
        protected virtual void OnUnbinding(IViewModel viewModel, string id) { }
        
        protected virtual void OnUnbound(IViewModel viewModel, string id) { }

        private static void ThrowExceptionIfInvalidData(IViewModel viewModel, string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
        }
    }
}