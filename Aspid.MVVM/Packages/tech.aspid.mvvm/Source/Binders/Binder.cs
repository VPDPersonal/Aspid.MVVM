#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Serializable <see cref="IBinder"/> that tracks its binding state and calls lifecycle hooks around binding.
    /// </summary>
    /// <remarks>
    /// A derived class implements <see cref="IBinder{T}"/>, <see cref="IReverseBinder{T}"/> or both to exchange values.
    /// </remarks>
    [Serializable]
    public abstract partial class Binder : IBinder
    {
        // ReSharper disable once MemberInitializerValueIgnored
        [Tooltip("The direction of data flow between the ViewModel and the View.")]
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;

        private IBinderRemover? _binderRemover;

        /// <summary>
        /// Gets a value indicating whether <see cref="Bind(IBinderAdder)"/> attaches the binder; <see langword="true"/> unless
        /// overridden.
        /// </summary>
        public virtual bool CanBind => true;

        /// <summary>
        /// Gets a value indicating whether the binder is attached to a bindable member.
        /// </summary>
        public bool IsBound { get; private set; }

        /// <summary>
        /// Gets the binding mode set by the constructor or in the Inspector.
        /// </summary>
        public BindMode Mode => _mode;

        /// <summary>
        /// Creates a <see cref="BindMode.OneWay"/> binder for Unity deserialization, which then restores the serialized mode.
        /// </summary>
        protected Binder()
            : this(BindMode.OneWay) { }

        /// <summary>
        /// Creates a binder with the given binding mode.
        /// </summary>
        /// <param name="mode">The binding mode.</param>
        protected Binder(BindMode mode = BindMode.OneWay)
        {
            _mode = mode;
        }

        /// <summary>
        /// Attaches the binder through <paramref name="binderAdder"/>, calling <see cref="OnBinding"/> before and
        /// <see cref="OnBound"/> after.
        /// </summary>
        /// <remarks>
        /// Does nothing when <see cref="CanBind"/> is <see langword="false"/>, and logs an error when the binder is already bound.
        /// </remarks>
        /// <param name="binderAdder">The bindable member to attach to.</param>
        public void Bind(IBinderAdder binderAdder)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                if (IsBound)
                {
                    Debug.LogError($"Binder is already bound. Type: {GetType().Name}");
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
        /// Called before the binder attaches and receives the first value. Override to prepare for that value.
        /// </summary>
        /// <remarks>
        /// Subscribe to the component in <see cref="OnBound"/> instead: a subscription made here reports the first value from the
        /// ViewModel back as if the user had entered it.
        /// </remarks>
        protected virtual void OnBinding() { }

        /// <summary>
        /// Called after the binder attaches and receives the first value. Override to subscribe to the component.
        /// </summary>
        protected virtual void OnBound() { }

        /// <summary>
        /// Detaches the binder, calling <see cref="OnUnbinding"/> before and <see cref="OnUnbound"/> after; does nothing when it
        /// is not bound.
        /// </summary>
        public void Unbind()
        {
#if ASPID_MVVM_PROFILER
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
        /// Called before the binder detaches, while values it reports still reach the ViewModel. Override to report a last value.
        /// </summary>
        protected virtual void OnUnbinding() { }

        /// <summary>
        /// Called after the binder detaches. Override to release what <see cref="OnBound"/> subscribed to.
        /// </summary>
        protected virtual void OnUnbound() { }
    }
}
