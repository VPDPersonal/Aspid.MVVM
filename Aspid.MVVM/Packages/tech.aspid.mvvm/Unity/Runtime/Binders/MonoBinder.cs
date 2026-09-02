using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="MonoBehaviour"/> binder that manages binding to and unbinding from an <see cref="IViewModel"/>.
    /// Derived classes implement <see cref="IBinder{T}"/> to define what is bound.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
        [Tooltip("Direction of data flow between the View and the ViewModel.")]
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;

        private IBinderRemover _binderRemover;

        /// <summary>
        /// Indicates whether binding is allowed. The default is <see langword="true"/>.
        /// </summary>
        public virtual bool CanBind => true;

        /// <summary>
        /// Indicates whether the binder is currently bound to a ViewModel.
        /// </summary>
        public bool IsBound { get; private set; }

        /// <summary>
        /// Gets the binding mode.
        /// </summary>
        public BindMode Mode => _mode;

        /// <summary>
        /// Gets the binding mode a freshly added binder starts in. The default is <see cref="BindMode.OneWay"/>.
        /// Override in binders whose <c>[BindModeOverride]</c> excludes it.
        /// </summary>
        protected virtual BindMode DefaultMode => BindMode.OneWay;

        /// <summary>
        /// Called by Unity when the component is added or reset in the Editor. Applies <see cref="DefaultMode"/>.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.Reset()</c>.
        /// </remarks>
        protected virtual void Reset() =>
            _mode = DefaultMode;

        /// <summary>
        /// Called by Unity when the component is destroyed. Unbinds so the ViewModel drops its reference to this binder.
        /// </summary>
        /// <remarks>
        /// When overriding, always call <c>base.OnDestroy()</c>.
        /// </remarks>
        protected virtual void OnDestroy() =>
            Unbind();

        /// <inheritdoc/>
        public void Bind(IBinderAdder binderAdder)
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                if (IsBound)
                {
                    // TODO Aspid.MVVM 1.1.0 -> add log with prefix
                    Debug.LogError($"Binder is already bound. Type: {GetType().Name}, Name: {name}");
                    return;
                }

                if (!CanBind) return;

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
        /// <remarks>
        /// The ViewModel pushes its first value after this hook, so subscribe to the component in <see cref="OnBound"/>,
        /// not here: a subscription taken here hears that first value as if the user had entered it.
        /// </remarks>
        protected virtual void OnBinding() { }

        /// <summary>
        /// Called after binding is established and the first value is applied. Override to subscribe to the component.
        /// </summary>
        protected virtual void OnBound() { }

        /// <inheritdoc/>
        public void Unbind()
        {
#if !ASPID_MVVM_UNITY_PROFILER_DISABLED
            using (this.Marker())
#endif
            {
                // TODO Aspid.MVVM 1.1.0 -> add log with prefix
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
        /// Called before unbinding, while the binder is still attached to the ViewModel. Override to add pre-unbinding logic.
        /// </summary>
        protected virtual void OnUnbinding() { }

        /// <summary>
        /// Called after unbinding. Override to release a subscription taken in <see cref="OnBound"/>.
        /// </summary>
        protected virtual void OnUnbound() { }
    }
}
