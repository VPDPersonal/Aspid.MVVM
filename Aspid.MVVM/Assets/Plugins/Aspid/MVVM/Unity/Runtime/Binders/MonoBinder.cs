using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="MonoBehaviour"/> for binder implementations.
    /// Manages the binding lifecycle — binding to and unbinding from an <see cref="IViewModel"/>.
    /// Derived classes must implement <see cref="IBinder{T}"/> to define the specific binding behavior.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
        [Tooltip("Binding mode that controls the direction of data flow between the View and ViewModel.")]
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;
        
        private IBinderRemover _binderRemover;

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
        
        /// <inheritdoc/>
        public void Bind(IBinderAdder binderAdder)
        {
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                if (IsBound)
                {
                    Debug.LogError($"Binder is already bound. Type: {GetType().Name}, Name: {name}");
                    return;
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
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (this.Marker())
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