using System;
using UnityEngine;

namespace Aspid.MVVM.Unity
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
        private static readonly global::Unity.Profiling.ProfilerMarker _bindMarker = new("MonoBinder.Bind");
        private static readonly global::Unity.Profiling.ProfilerMarker _bindTMarker = new("MonoBinder<T>.Bind");
        private static readonly global::Unity.Profiling.ProfilerMarker _unbindMarker = new("MonoBinder.Unbind");
#endif
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;
        
        private IBindableMemberEventRemover _bindableMemberEventRemover;

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
        /// Binds the component using the specified <see cref="IBindableMemberEventAdder"/>.
        /// </summary>
        /// <param name="bindableMemberEventAdder">The event adder for the component to bind to.</param>
        /// <exception cref="Exception">Thrown if the binder is already bound.</exception>\
        public void Bind(IBindableMemberEventAdder bindableMemberEventAdder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto())
#endif
            {
                if (IsBound) throw new Exception("This Binder is already bound.");
                if (!IsBind) return;
                
                OnBinding();
                
                _bindableMemberEventRemover = bindableMemberEventAdder.Add(this);
                IsBound = true;
                
                OnBound();
            }
        }
        
        /// <summary>
        /// Logic executed before binding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnBinding() { }
        
        /// <summary>
        /// Logic executed after binding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnBound() { }
        
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
                
                _bindableMemberEventRemover?.Remove(this);
                _bindableMemberEventRemover = null;
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