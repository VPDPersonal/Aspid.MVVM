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

        /// <summary>
        /// Gets the binding mode a freshly added binder starts in.
        /// The default value is <see cref="BindMode.OneWay"/>.
        /// </summary>
        /// <remarks>
        /// Override this in binders whose <c>[BindModeOverride]</c> excludes <see cref="BindMode.OneWay"/> — a
        /// reverse-only or one-time binder must not start in a mode it forbids. There is deliberately no single
        /// correct constant here: <c>*ToSourceMonoBinder</c> needs <see cref="BindMode.OneWayToSource"/> while
        /// everything else needs <see cref="BindMode.OneWay"/>.
        /// </remarks>
        protected virtual BindMode DefaultMode => BindMode.OneWay;

        /// <summary>
        /// Called by Unity when the component is added in the Editor or reset from its context menu.
        /// Applies <see cref="DefaultMode"/> to the serialized binding mode.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.Reset()</c> to preserve the default mode.
        /// </remarks>
        protected virtual void Reset() =>
            _mode = DefaultMode;
        
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
#if UNITY_2022_1_OR_NEWER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
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
        /// Called by Unity when the binder is destroyed. Unbinds so the ViewModel drops its reference to this binder.
        /// </summary>
        /// <remarks>
        /// A binder is a component in its own right and can be destroyed independently of the <see cref="IViewModel"/>
        /// it is bound to — pooling, or a <c>Destroy</c> on a child object while the View lives on. Without this the
        /// subscription survives the component: the ViewModel keeps a managed reference to a dead
        /// <see cref="MonoBehaviour"/> and raises <c>MissingReferenceException</c> on every subsequent change, which
        /// also stops delivery to every binder subscribed after it.
        /// <para/>
        /// When overriding this method, always call <c>base.OnDestroy()</c> to preserve unbinding.
        /// </remarks>
        protected virtual void OnDestroy() =>
            Unbind();

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