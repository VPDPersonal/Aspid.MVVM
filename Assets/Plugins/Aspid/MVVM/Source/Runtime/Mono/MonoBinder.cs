#nullable disable
using System;
using UnityEngine;

namespace Aspid.MVVM.Mono
{
    /// <summary>
    /// Abstract class derived from <see cref="MonoBehaviour"/> that implements the basic logic for binding a component to <see cref="IViewModel"/>.
    /// Includes methods for binding and unbinding the component from the ViewModel.
    /// Derivatives must implement one or more interfaces <see cref="IBinder{T}"/> to complete the implementation of specific binding logic.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;
        
        private IRemoveBinderFromViewModel _removeBinderFromViewModel;

        /// <summary>
        /// Indicates whether binding is allowed.
        /// The default value is <c>true</c>.
        /// </summary>
        public virtual bool IsBind => true;
        
        /// <summary>
        /// Indicates whether the object is currently bound.
        /// This value can only be set within the class.
        /// </summary>
        public bool IsBound { get; private set; }

        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
        /// Default is <see cref="BindMode.OneWay"/>.
        /// </summary>
        public BindMode Mode => _mode;

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
                OnBindingDebug(parameters);
                
                var bindResult = parameters.AddBinder(this);
                
                _removeBinderFromViewModel = bindResult.BinderRemover;
                IsBound = bindResult.IsBound;
                
                OnBound(parameters, bindResult.IsBound);
            }
        }

        partial void OnBindingDebug(in BindParameters parameters);
        
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
        /// <param name="isBound">
        /// Indicates whether the binding operation was successful. 
        /// <c>true</c> if the binding was successful; otherwise, <c>false</c>.
        /// </param>
        protected virtual void OnBound(in BindParameters parameters, bool isBound) { }
        
        /// <summary>
        /// Unbinds the component from the bound s<see cref="IViewModel"/>.
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