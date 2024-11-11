#nullable disable
using System;
using UnityEngine;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM.Mono
{
    /// <summary>
    /// Abstract class derived from <see cref="MonoBehaviour"/> that implements the basic logic for binding a component to <see cref="IViewModel"/>.
    /// Includes methods for binding and unbinding the component from the ViewModel.
    /// Derivatives must implement one or more interfaces <see cref="IBinder{T}"/> to complete the implementation of specific binding logic.
    /// </summary>
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        private IRemoveBinderFromViewModel _removeBinderFromViewModel;
        
        /// <summary>
        /// Indicates whether binding is allowed.
        /// The default value is <c>true</c>.
        /// </summary>
        protected virtual bool IsBind => true;
        
        /// <summary>
        /// Binds the component to the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="viewModel">The instance of the ViewModel to bind.</param>
        /// <param name="id">The ID of the component to bind, which matches the property name in the ViewModel.</param>
        public void Bind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                if (!IsBind) return;
                ThrowExceptionIfInvalidData(viewModel, id);
                
                OnBinding(viewModel, id);
                OnBindingDebug(viewModel, id);
                
                _removeBinderFromViewModel = viewModel.AddBinder(this, id);
                OnBound(viewModel, id);
            }
        }

        partial void OnBindingDebug(IViewModel viewModel, string id);
        
        /// <summary>
        /// Logic executed before binding, which can be overridden in derived classes.
        /// </summary>
        /// <param name="viewModel">The instance of the ViewModel.</param>
        /// <param name="id">The ID of the component, which matches the property name in the ViewModel.</param>
        protected virtual void OnBinding(IViewModel viewModel, string id) { }
        
        /// <summary>
        /// Logic executed after binding, which can be overridden in derived classes.
        /// </summary>
        /// <param name="viewModel">The instance of the ViewModel.</param>
        /// <param name="id">The ID of the component, which matches the property name in the ViewModel.</param>
        protected virtual void OnBound(IViewModel viewModel, string id) { }
        
        /// <summary>
        /// Unbinds the component from the specified <see cref="IViewModel"/>.
        /// </summary>
        public void Unbind()
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (_removeBinderFromViewModel is null) return;
                if (!IsBind) return;

                OnUnbindingDebug();
                OnUnbinding();
                
                _removeBinderFromViewModel.RemoveBinder(this);
                OnUnbound();
            }
        }
        
        partial void OnUnbindingDebug();
        
        /// <summary>
        /// Logic executed before unbinding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnUnbinding() { }
        
        /// <summary>
        /// Logic executed after unbinding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnUnbound() { }
        
        private static void ThrowExceptionIfInvalidData(IViewModel viewModel, string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            if (viewModel is null) throw new ArgumentNullException(nameof(viewModel));
        }
    }
}