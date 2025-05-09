using System;

namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract class that implements the base logic for binding a component with a <see cref="IViewModel"/>.
    /// It includes methods for binding and unbinding the component with the ViewModel.
    /// Derivatives must implement one or more <see cref="IBinder{T}"/> interfaces to complete specific binding logic.
    /// </summary>
    [Serializable]
    public abstract class Binder : IBinder
    {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
        private static readonly Unity.Profiling.ProfilerMarker _bindMarker = new("Binder.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _bindTMarker = new("Binder<T>.Bind");
        private static readonly Unity.Profiling.ProfilerMarker _unbindMarker = new("Binder.Unbind)");
#endif
        // ReSharper disable once MemberInitializerValueIgnored
#if UNITY_2022_1_OR_NEWER
        [UnityEngine.SerializeField]
#endif
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        private BindMode _mode = BindMode.TwoWay;
        
        private IViewModelEventRemover? _viewModelEventRemover;
        
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

        protected Binder(BindMode mode)
        {
            var b = new Binder[5];
            b.BindSafely<Binder, int>(default);
            _mode = mode;
        }

        public void Bind<T>(in BindableMember<T> bindableMember)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_bindTMarker.Auto())
#endif
            {
                OnBindingInternal();
                _viewModelEventRemover = bindableMember.AddBinder(this);
                OnBoundInternal();
            }
        }
        
        public void Bind(IViewModelEventAdder viewModelEventAdder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (_bindMarker.Auto())
#endif
            {
                OnBindingInternal();
                _viewModelEventRemover = viewModelEventAdder.AddBinder(this);
                OnBoundInternal();
            }
        }

        private void OnBindingInternal()
        {
            if (IsBound) throw new Exception("This Binder is already bound.");
            if (!IsBind) return;

            OnBinding();
        }
        
        protected virtual void OnBinding() { }

        private void OnBoundInternal()
        {
            IsBound = true;
            OnBound();
        }
        
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
                
                OnUnbinding();
                
                _viewModelEventRemover?.RemoveBinder(this);
                _viewModelEventRemover = null;
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