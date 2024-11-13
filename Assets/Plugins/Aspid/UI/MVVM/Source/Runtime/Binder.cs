using System;
using Aspid.UI.MVVM.ViewModels;

namespace Aspid.UI.MVVM
{
    /// <summary>
    /// Abstract class that implements the base logic for binding a component with a <see cref="IViewModel"/>.
    /// It includes methods for binding and unbinding the component with the ViewModel.
    /// Derivatives must implement one or more <see cref="IBinder{T}"/> interfaces to complete specific binding logic.
    /// </summary>
    public abstract partial class Binder : IBinder
    {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("Binder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("Binder.Unbind)");
#endif
        private IRemoveBinderFromViewModel? _removeBinderFromViewModel;
        
        /// <summary>
        /// Indicates whether binding is allowed.
        /// The default value is <c>true</c>.
        /// </summary>
        public virtual bool IsBind => true;
        
        public bool IsBound { get; private set; }
        
        /// <summary>
        /// Binds the component to the specified <see cref="IViewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance to bind to.</param>
        /// <param name="id">The component ID for binding, which matches the property name in the ViewModel.</param>
        public void Bind(IViewModel viewModel, string id)
        {
#if !ASPID_UI_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                ThrowExceptionIfInvalidData(viewModel, id);
                
                if (IsBound) throw new Exception("This Binder is already bound.");
                if (!IsBind) return;

                OnBindingDebug(viewModel, id);
                OnBinding(viewModel, id);
                
                _removeBinderFromViewModel =  viewModel.AddBinder(this, id);;
                IsBound = true;
                
                OnBound(viewModel, id);
            }
        }

        partial void OnBindingDebug(IViewModel viewModel, string id);
        
        /// <summary>
        /// Logic executed before binding, which can be overridden in derived classes.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance.</param>
        /// <param name="id">The component ID, which matches the property name in the ViewModel.</param>
        protected virtual void OnBinding(IViewModel viewModel, string id) { }
        
        /// <summary>
        /// Logic executed after binding, which can be overridden in derived classes.
        /// </summary>
        /// <param name="viewModel">The ViewModel instance.</param>
        /// <param name="id">The component ID, which matches the property name in the ViewModel.</param>
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
                if (!IsBound) return;
                if (!IsBind) return;

                OnUnbindingDebug();
                OnUnbinding();
                
                _removeBinderFromViewModel?.RemoveBinder(this);
                _removeBinderFromViewModel = null;
                IsBound = false;
                
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