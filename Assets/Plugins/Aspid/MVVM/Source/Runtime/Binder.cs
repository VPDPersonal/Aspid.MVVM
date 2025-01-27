using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract class that implements the base logic for binding a component with a <see cref="IViewModel"/>.
    /// It includes methods for binding and unbinding the component with the ViewModel.
    /// Derivatives must implement one or more <see cref="IBinder{T}"/> interfaces to complete specific binding logic.
    /// </summary>
    public abstract partial class Binder : IBinder
    {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
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
        /// Binds a component using the specified binding parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters that contain the ViewModel and the component ID for binding, where the component ID matches
        /// the property name in the ViewModel.
        /// </param>
        public void Bind(in BindParameters parameters)
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto()) 
#endif
            {
                if (IsBound) throw new Exception("This Binder is already bound.");
                if (!IsBind) return;
                
                OnBinding(parameters);

                _removeBinderFromViewModel = parameters.AddBinder(this);
                IsBound = true;
                
                OnBound(parameters);
            }
        }
        
        /// <summary>
        /// Logic executed before binding, which can be overridden in derived classes.
        /// </summary>
        /// <param name="parameters">
        /// The parameters that contain the ViewModel and the component ID for binding, where the component ID matches
        /// the property name in the ViewModel.
        /// </param>
        protected virtual void OnBinding(in BindParameters parameters) { }
        
        /// <summary>
        /// Logic executed after binding, which can be overridden in derived classes.
        /// </summary>
        /// <param name="parameters">
        /// The parameters that contain the ViewModel and the component ID for binding, where the component ID matches
        /// the property name in the ViewModel.
        /// </param>
        protected virtual void OnBound(in BindParameters parameters) { }
        
        /// <summary>
        /// Unbinds the component from the bound <see cref="IViewModel"/>.
        /// </summary>
        public void Unbind()
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (!IsBound) return;
                
                OnUnbinding();
                
                _removeBinderFromViewModel?.RemoveBinder(this);
                _removeBinderFromViewModel = null;
                IsBound = false;
                
                OnUnbound();
            }
        }
        
        /// <summary>
        /// Logic executed before unbinding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnUnbinding() { }
        
        /// <summary>
        /// Logic executed after unbinding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnUnbound() { }
    }
}