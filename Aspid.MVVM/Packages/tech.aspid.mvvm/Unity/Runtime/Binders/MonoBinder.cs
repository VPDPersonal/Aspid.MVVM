#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBehaviour"/> binder that manages binding to and unbinding from an <see cref="IViewModel"/>.
    /// </summary>
    /// <remarks>Derived classes implement <see cref="IBinder{T}"/> to define what is bound.</remarks>
    // ReSharper disable once RedundantExtendsListEntry
    public abstract partial class MonoBinder : MonoBehaviour, IBinder
    {
        [Tooltip("Direction of data flow between the View and the ViewModel.")]
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.None;

        private bool _isDestroyed;
        private BinderState _state;
        private IBinderRemover _binderRemover;

        /// <summary>
        /// Gets the owner while this binder is being attached, bound or cleaned up; <see langword="null"/> otherwise.
        /// </summary>
        protected object Owner { get; private set; }

        /// <inheritdoc/>
        public bool IsBound => _state is BinderState.Bound;

        /// <summary>
        /// Gets a value indicating whether binding is allowed. The default is <see langword="true"/>.
        /// </summary>
        public virtual bool CanBind => true;

        /// <summary>
        /// Gets or sets the binding mode; an unconfigured runtime component uses <see cref="DefaultMode"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">The binder is bound or a lifecycle operation is in progress.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The value is not a supported binding mode.</exception>
        public BindMode Mode
        {
            get => _mode is BindMode.None ? DefaultMode : _mode;
            set
            {
                if (_state is not BinderState.Unbound)
                    throw new InvalidOperationException("Cannot change the mode while the binder is bound or a lifecycle operation is in progress.");
                
                if (!value.IsValid())
                    throw new ArgumentOutOfRangeException(nameof(value));

                _mode = value;
            }
        }

        /// <summary>
        /// Gets the binding mode a freshly added binder starts in. The default is <see cref="BindMode.OneWay"/>.
        /// Override in binders whose <c>[BindModeOverride]</c> excludes it.
        /// </summary>
        protected virtual BindMode DefaultMode => BindMode.OneWay;

        private bool IsAlive => !_isDestroyed && this;

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
        protected virtual void OnDestroy()
        {
            _isDestroyed = true;
            
            if (IsBound)
                UnbindInternal();
        }

        /// <summary>
        /// Attaches the binder through <paramref name="context"/>, calling <see cref="OnBinding"/> before and
        /// <see cref="OnBound"/> after.
        /// </summary>
        /// <remarks>
        /// Does nothing when <see cref="CanBind"/> is <see langword="false"/>. Installation failures roll back;
        /// exceptions from <see cref="OnBound"/> propagate without rolling back the completed binding.
        /// </remarks>
        /// <param name="context">The bindable member and the owner acquiring the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> is uninitialized.</exception>
        /// <exception cref="InvalidOperationException">The binder is destroyed, already bound or a lifecycle operation is in progress.</exception>
        public void Bind(in BindContext context)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                context.EnsureValid();
                if (_state is not BinderState.Unbound || !IsAlive)
                    throw new InvalidOperationException("The binder is already bound or unavailable for binding.");

                _state = BinderState.Binding;
                
                Owner = context.Owner;
                var bindingStarted = false;

                try
                {
                    if (!CanBind || !IsAlive)
                    {
                        ResetBindingState();
                        return;
                    }

                    bindingStarted = true;
                    OnBinding();
                    
                    if (IsAlive)
                        _binderRemover = context.Adder.Add(binder: this);
                    
                    if (IsAlive)
                        OnAttach();
                    
                    if (IsAlive)
                    {
                        OnBoundDebug(in context);
                        _state = BinderState.Bound;
                    }
                }
                catch
                {
                    if (bindingStarted)
                    {
                        try
                        {
                            UnbindInternal();
                        }
                        catch
                        {
                            // Do not replace the original exception with a cleanup failure.
                        }
                    }
                    else
                    {
                        ResetBindingState();
                    }

                    throw;
                }

                if (_state is BinderState.Binding)
                {
                    UnbindInternal();
                    return;
                }

                OnBound();
            }
        }

        /// <summary>
        /// Called before attachment and the first value. Override to prepare for that value.
        /// </summary>
        /// <remarks>Subscribe to the component in <see cref="OnAttach"/> so the first value is not reported back as user input.</remarks>
        protected virtual void OnBinding() { }

        /// <summary>
        /// Called after attachment and the first value, before the binding is complete. Override to acquire subscriptions.
        /// </summary>
        /// <remarks>An exception rolls back the binding; <see cref="OnDetach"/> releases partially acquired resources.</remarks>
        protected virtual void OnAttach() { }

        partial void OnBoundDebug(in BindContext context);

        /// <summary>
        /// Called with the binding complete and <see cref="IsBound"/> set. Override to notify or send an initial value.
        /// </summary>
        /// <remarks>
        /// Exceptions do not roll back the binding. This hook may unbind and bind again; finish resource changes before invoking external code.
        /// </remarks>
        protected virtual void OnBound() { }

        /// <summary>
        /// Detaches the binder when <paramref name="context"/> identifies its owner, calling the cleanup hooks.
        /// </summary>
        /// <remarks>
        /// Does nothing for a different owner or when cleanup is already in progress.
        /// A single cleanup failure is rethrown with its original stack trace after the remaining steps finish.
        /// </remarks>
        /// <param name="context">The owner releasing the binding.</param>
        /// <exception cref="ArgumentException"><paramref name="context"/> is uninitialized.</exception>
        /// <exception cref="InvalidOperationException">The owner requests cleanup before attachment finishes.</exception>
        /// <exception cref="AggregateException">Multiple cleanup operations fail.</exception>
        public void Unbind(in UnbindContext context)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                context.EnsureValid();
                
                if (!ReferenceEquals(Owner, context.Owner)
                    || _state is BinderState.Unbound or BinderState.Unbinding) return;
                
                if (_state is BinderState.Binding)
                    throw new InvalidOperationException("The binder cannot be released before attachment finishes.");

                UnbindInternal();
            }
        }

        private void UnbindInternal()
        {
            if (_state is BinderState.Unbound or BinderState.Unbinding) return;
            
            _state = BinderState.Unbinding;
            var failures = new CleanupExceptions();

            try
            {
                OnUnbinding();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            try
            {
                _binderRemover?.Remove(binder: this);
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }
            finally
            {
                _binderRemover = null;
            }

            try
            {
                OnDetach();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            try
            {
                OnUnboundDebug();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            ResetBindingState();

            try
            {
                OnUnbound();
            }
            catch (Exception exception)
            {
                failures.Add(exception);
            }

            failures.ThrowIfAny();
        }

        /// <summary>
        /// Called when cleanup starts, while reported values still reach the ViewModel. Override to send a last value.
        /// </summary>
        /// <remarks><see cref="IsBound"/> is already <see langword="false"/>; the owner is retained until cleanup finishes.</remarks>
        protected virtual void OnUnbinding() { }

        /// <summary>
        /// Called after removal, before ownership is released. Override to release subscriptions and other resources.
        /// </summary>
        /// <remarks>Also called after a failed installation; tolerate partially initialized resources.</remarks>
        protected virtual void OnDetach() { }

        partial void OnUnboundDebug();

        /// <summary>
        /// Called after cleanup releases ownership. Override to notify that the binder is unbound.
        /// </summary>
        /// <remarks>This hook may bind again; finish resource changes before invoking external code.</remarks>
        protected virtual void OnUnbound() { }

        private void ResetBindingState()
        {
            Owner = null;
            _state = BinderState.Unbound;
        }
    }
}
