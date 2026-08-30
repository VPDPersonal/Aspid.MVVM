#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IReverseBinder{T}"/> that propagates values of type <typeparamref name="T"/>
    /// from the View back to the ViewModel.
    /// </summary>
    /// <typeparam name="T">The type of the value reported to the ViewModel.</typeparam>
    [System.Obsolete("Use the GenericOneWayToSource binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityGenericOneWayToSourceBinder<T> : Binder, IReverseBinder<T>
    {
        private readonly Func<T?>? _onBoundValueChanged;
        private readonly Func<T?>? _onUnboundValueChanged;

        /// <param name="initialize">
        /// A <see cref="UnityAction{T}"/> that receives the internal <see cref="OnValueChanged"/> callback and registers it with the View event.
        /// </param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        public UnityGenericOneWayToSourceBinder(
            UnityAction<UnityAction<T>> initialize, 
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            initialize.Invoke(OnValueChanged);
            
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }

        /// <param name="onBoundValueChanged">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// At least one of <paramref name="onBoundValueChanged"/> or <paramref name="onUnboundValueChanged"/> must be provided.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="Exception">
        /// Thrown when both <paramref name="onBoundValueChanged"/> and <paramref name="onUnboundValueChanged"/>
        /// are <see langword="null"/>.
        /// </exception>
        public UnityGenericOneWayToSourceBinder(
            Func<T?>? onBoundValueChanged = null,
            Func<T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Invokes the onBoundValueChanged factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke());
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the onUnboundValueChanged factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_onUnboundValueChanged is not null)
                OnValueChanged(_onUnboundValueChanged.Invoke());
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
    
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IReverseBinder{T}"/> that propagates values of type <typeparamref name="T"/>
    /// from the View back to the ViewModel, holding a <typeparamref name="TTarget"/> reference to avoid closures.
    /// </summary>
    /// <typeparam name="TTarget">The type of the View-side target object that exposes the value.</typeparam>
    /// <typeparam name="T">The type of the value reported to the ViewModel.</typeparam>
    /// <remarks>
    /// Otherwise behaves identically to <see cref="UnityGenericOneWayToSourceBinder{T}"/>.
    /// </remarks>
    [System.Obsolete("Use the GenericOneWayToSource binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityGenericOneWayToSourceBinder<TTarget, T> : Binder, IReverseBinder<T>
    {
        private readonly TTarget _target;
        private readonly Func<TTarget, T?>? _onBoundValueChanged;
        private readonly Func<TTarget, T?>? _onUnboundValueChanged;

        /// <param name="target">The target object whose event or value is monitored.</param>
        /// <param name="initialize">
        /// A <see cref="UnityAction{T0,T1}"/> that receives <paramref name="target"/> and the internal <see cref="OnValueChanged"/>
        /// callback, and registers it with the appropriate View event.
        /// </param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        public UnityGenericOneWayToSourceBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> initialize, 
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));
            
            initialize.Invoke(target, OnValueChanged);
        }

        /// <param name="target">The target object whose value is read when bound or unbound.</param>
        /// <param name="onBoundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel. At least one of <paramref name="onBoundValueChanged"/>
        /// or <paramref name="onUnboundValueChanged"/> must be provided.
        /// </param>
        /// <param name="onUnboundValueChanged">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="Exception">
        /// Thrown when both <paramref name="onBoundValueChanged"/> and <paramref name="onUnboundValueChanged"/>
        /// are <see langword="null"/>.
        /// </exception>
        public UnityGenericOneWayToSourceBinder(
            TTarget target,
            Func<TTarget, T?>? onBoundValueChanged = null,
            Func<TTarget, T?>? onUnboundValueChanged = null)
            : base(BindMode.OneWayToSource)
        {
            if (onBoundValueChanged is null && onUnboundValueChanged is null)
                throw new Exception("OnBoundValueChanged and OnUnboundValueChanged are both null");

            _onBoundValueChanged = onBoundValueChanged;
            _onUnboundValueChanged = onUnboundValueChanged;
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Invokes the onBoundValueChanged factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_onBoundValueChanged is not null)
                OnValueChanged(_onBoundValueChanged.Invoke(_target));
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the onUnboundValueChanged factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_onUnboundValueChanged is not null)
                OnValueChanged(_onUnboundValueChanged.Invoke(_target));
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
}