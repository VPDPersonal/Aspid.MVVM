using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base class for binder implementations.
    /// Manages the binding lifecycle — binding to and unbinding from an <see cref="IViewModel"/>.
    /// Derived classes must implement <see cref="IBinder{T}"/> to define the specific binding behavior.
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
        /// The default value is <see langword="true"/>.
        /// </summary>
        public virtual bool IsBind => true;

        /// <summary>
        /// Indicates whether the binder is currently bound to a ViewModel.
        /// </summary>
        public bool IsBound { get; private set; }

        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
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

        /// <inheritdoc/>
        public void Bind(IBinderAdder binderAdder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto())
#endif
            {
                if (IsBound)
                {
                    var message = $"Binder is already bound. Type: {GetType().Name}";
                    
#if UNITY_2022_1_OR_NEWER
                    UnityEngine.Debug.LogError(message);    
                    return;
#endif
                    throw new Exception(message);
                }
                
                if (!IsBind) return;

                OnBinding();
                {
                    _binderRemover = binderAdder.Add(binder: this);
                    
                    IsBound = true;
                    OnBoundDebug(binderAdder);
                }
                OnBound();
            }
        }

        partial void OnBoundDebug(IBinderAdder binderAdder);

        /// <summary>
        /// Called before binding is established. Override to add pre-binding logic.
        /// </summary>
        protected virtual void OnBinding() { }

        /// <summary>
        /// Called after binding is established. Override to add post-binding logic.
        /// </summary>
        protected virtual void OnBound() { }

        /// <inheritdoc/>
        public void Unbind()
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_unbindMarker.Auto())
#endif
            {
                if (!IsBound) return;

                OnUnbinding();
                {
                    if (_binderRemover is not null)
                    {
                        _binderRemover.Remove(binder: this);
                        _binderRemover = null;
                    }
                    
                    IsBound = false;
                    OnUnboundDebug();
                }
                OnUnbound();
            }
        }

        partial void OnUnboundDebug();

        /// <summary>
        /// Called before unbinding. Override to add pre-unbinding logic.
        /// </summary>
        protected virtual void OnUnbinding() { }

        /// <summary>
        /// Called after unbinding. Override to add post-unbinding logic.
        /// </summary>
        protected virtual void OnUnbound() { }
    }
}