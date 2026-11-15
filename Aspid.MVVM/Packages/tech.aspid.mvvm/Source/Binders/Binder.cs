#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

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
    [SuppressMessage(category: "ReSharper",checkId: "MemberInitializerValueIgnored")]
    public abstract partial class Binder : IBinder
    {
        [Tooltip("The direction of data flow between the ViewModel and the View.")]
        [BindMode(BindMode.OneWay, BindMode.OneTime)]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;

        private BinderState _state;
        private IBinderRemover? _binderRemover;

        /// <summary>
        /// Gets the owner while this binder is being attached, bound or cleaned up; <see langword="null"/> otherwise.
        /// </summary>
        protected object? Owner { get; private set; }

        /// <summary>
        /// Creates a <see cref="BindMode.OneWay"/> binder for Unity deserialization, which then restores the serialized mode.
        /// </summary>
        protected Binder()
            : this(BindMode.OneWay) { }

        /// <summary>
        /// Creates a binder with the given binding mode.
        /// </summary>
        /// <param name="mode">The binding mode.</param>
        /// <exception cref="ArgumentOutOfRangeException">The mode is <see cref="BindMode.None"/> or undefined.</exception>
        protected Binder(BindMode mode = BindMode.OneWay)
        {
            mode.EnsureValid();
            _mode = mode;
        }

        /// <inheritdoc/>
        public bool IsBound => _state is BinderState.Bound;

        /// <summary>
        /// Gets a value indicating whether <see cref="Bind(in BindContext)"/> attaches the binder; <see langword="true"/> unless
        /// overridden.
        /// </summary>
        public virtual bool CanBind => true;

        /// <summary>
        /// Gets the binding mode set by the constructor or in the Inspector.
        /// </summary>
        public BindMode Mode => _mode;

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
        /// <exception cref="InvalidOperationException">The binder is already bound or a lifecycle operation is in progress.</exception>
        public void Bind(in BindContext context)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                context.EnsureValid();
                if (_state is not BinderState.Unbound)
                    throw new InvalidOperationException("The binder is already bound or unavailable for binding.");

                _state = BinderState.Binding;
                
                Owner = context.Owner;
                var bindingStarted = false;

                try
                {
                    if (!CanBind)
                    {
                        ResetBindingState();
                        return;
                    }

                    bindingStarted = true;
                    OnBinding();

                    _binderRemover = context.Adder.Add(binder: this);
                    OnAttach();
                    
                    OnBoundDebug(in context);
                    _state = BinderState.Bound;
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
