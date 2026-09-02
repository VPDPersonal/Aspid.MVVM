using System;
using UnityEngine;

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
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("Binding mode that controls the direction of data flow between the View and ViewModel.")]
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;

        private IBinderRemover? _binderRemover;

        /// <summary>
        /// Indicates whether binding is allowed.
        /// The default value is <see langword="true"/>.
        /// </summary>
        public virtual bool CanBind => true;

        /// <summary>
        /// Indicates whether the binder is currently bound to a ViewModel.
        /// </summary>
        public bool IsBound { get; private set; }

        /// <summary>
        /// Gets the binding mode that determines the direction of data flow.
        /// </summary>
        public BindMode Mode => _mode;

        /// <remarks>
        /// For deserialization only: Unity builds a serialized instance without running a constructor's
        /// arguments and assigns the fields itself.
        /// </remarks>
        protected Binder()
            : this(BindMode.OneWay) { }

        /// <param name="mode">The binding mode to use for the binder.</param>
        protected Binder(BindMode mode = BindMode.OneWay)
        {
            _mode = mode;
        }

        /// <inheritdoc/>
        public void Bind(IBinderAdder binderAdder)
        {
#if ENABLE_PROFILER
            using (this.Marker())
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
        /// The order is: <see cref="OnBinding"/>, then the ViewModel pushes its current value, then
        /// <see cref="IsBound"/> becomes <see langword="true"/>, then <see cref="OnBound"/>.
        /// <para/>
        /// That first push happens <em>after</em> this hook, which is why a binder that listens to its component
        /// subscribes in <see cref="OnBound"/> and not here: subscribing here means hearing the ViewModel's own
        /// first value come back as if the user had entered it.
        /// </remarks>
        protected virtual void OnBinding() { }

        /// <summary>
        /// Called after binding is established. Override to add post-binding logic.
        /// </summary>
        /// <remarks>
        /// Runs after the ViewModel's first value has been applied and after <see cref="IsBound"/> is
        /// <see langword="true"/>. This is where a binder subscribes to its component — see
        /// <see cref="OnBinding"/> for why the earlier hook is the wrong place.
        /// </remarks>
        protected virtual void OnBound() { }

        /// <inheritdoc/>
        public void Unbind()
        {
#if ENABLE_PROFILER
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
        /// <remarks>
        /// Runs while <see cref="IsBound"/> is still <see langword="true"/> and the binder is still
        /// attached to the ViewModel, so anything sent from here still arrives.
        /// </remarks>
        protected virtual void OnUnbinding() { }

        /// <summary>
        /// Called after unbinding. Override to add post-unbinding logic.
        /// </summary>
        /// <remarks>
        /// Runs once the binder is detached and <see cref="IsBound"/> is <see langword="false"/>.
        /// This is where a subscription taken in <see cref="OnBound"/> is released.
        /// </remarks>
        protected virtual void OnUnbound() { }
    }
}
