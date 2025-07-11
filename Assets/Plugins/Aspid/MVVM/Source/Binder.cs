using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract class that implements the base logic for binding a component with a <see cref="IViewModel"/>.
    /// It includes methods for binding and unbinding the component with the ViewModel.
    /// Derivatives must implement one or more <see cref="IBinder{T}"/> interfaces to complete specific binding logic.
    /// </summary>
    [Serializable]
    public abstract partial class Binder : IBinder
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("Binder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("Binder.Unbind)");
#endif
        // ReSharper disable once MemberInitializerValueIgnored
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        private BindMode _mode = BindMode.TwoWay;
        
        private IBinderRemover? _binderRemover;
        
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

        internal Binder()
            : this(BindMode.OneWay) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Binder"/> class with the specified binding mode.
        /// </summary>
        /// <param name="mode">The binding mode to use for the binder.</param>
        protected Binder(BindMode mode)
        {
            _mode = mode;
        }
        
        /// <summary>
        /// Binds the component using the specified <see cref="IBinderAdder"/>.
        /// </summary>
        /// <param name="binderAdder">The event adder for the component to bind to.</param>
        /// <exception cref="Exception">Thrown if the binder is already bound.</exception>\
        public void Bind(IBinderAdder binderAdder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto())
#endif
            {
                if (IsBound) throw new Exception("This Binder is already bound.");
                if (!IsBind) return;

                OnBindingDebug(binderAdder);
                OnBinding();
                
                _binderRemover = binderAdder.Add(this);
                IsBound = true;
                
                OnBoundDebug(binderAdder);
                OnBound();
            }
        }
        
        partial void OnBindingDebug(IBinderAdder binderAdder);
        
        partial void OnBoundDebug(IBinderAdder binderAdder);
        
        /// <summary>
        /// Logic executed before binding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnBinding() { }
        
        /// <summary>
        /// Logic executed after binding, which can be overridden in derived classes.
        /// </summary>
        protected virtual void OnBound() { }
        
        /// <summary>
        /// Unbinds the component from the bound <see cref="IViewModel"/>.
        /// </summary>
        public void Unbind()
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (!IsBound) return;

                OnUnbindingDebug();
                OnUnbinding();
                
                _binderRemover?.Remove(this);
                _binderRemover = null;
                IsBound = false;

                OnUnboundDebug();
                OnUnbound();
            }
        }
        
        partial void OnUnbindingDebug();
        
        partial void OnUnboundDebug();
        
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