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
    [System.Obsolete("Use the DelegateOneWayToSource binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityDelegateOneWayToSourceBinder<T> : Binder, IReverseBinder<T>
    {
        private readonly Func<T?>? _getValueOnBound;
        private readonly Func<T?>? _getValueOnUnbinding;

        /// <param name="subscribe">
        /// A <see cref="UnityAction{T}"/> that receives the internal <see cref="OnValueChanged"/> callback and registers it with the View event.
        /// </param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="subscribe"/> is <see langword="null"/>.</exception>
        public UnityDelegateOneWayToSourceBinder(
            UnityAction<UnityAction<T>> subscribe, 
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            (subscribe ?? throw new ArgumentNullException(nameof(subscribe))).Invoke(OnValueChanged);
            
            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
        }

        /// <param name="getValueOnBound">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// At least one of <paramref name="getValueOnBound"/> or <paramref name="getValueOnUnbinding"/> must be provided.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when both <paramref name="getValueOnBound"/> and <paramref name="getValueOnUnbinding"/>
        /// are <see langword="null"/>.
        /// </exception>
        public UnityDelegateOneWayToSourceBinder(
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            if (getValueOnBound is null && getValueOnUnbinding is null)
                throw new ArgumentException($"{nameof(getValueOnBound)} and {nameof(getValueOnUnbinding)} are both null.");

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Invokes the getValueOnBound factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_getValueOnBound is not null)
                OnValueChanged(_getValueOnBound.Invoke());
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the getValueOnUnbinding factory and pushes the returned value to the ViewModel,
        /// if the factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_getValueOnUnbinding is not null)
                OnValueChanged(_getValueOnUnbinding.Invoke());
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
    /// Otherwise behaves identically to <see cref="UnityDelegateOneWayToSourceBinder{T}"/>.
    /// </remarks>
    [System.Obsolete("Use the DelegateOneWayToSource binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityDelegateOneWayToSourceBinder<TTarget, T> : Binder, IReverseBinder<T>
    {
        private readonly TTarget _target;
        private readonly Func<TTarget, T?>? _getValueOnBound;
        private readonly Func<TTarget, T?>? _getValueOnUnbinding;

        /// <param name="target">The target object whose event or value is monitored.</param>
        /// <param name="subscribe">
        /// A <see cref="UnityAction{T0,T1}"/> that receives <paramref name="target"/> and the internal <see cref="OnValueChanged"/>
        /// callback, and registers it with the appropriate View event.
        /// </param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> or <paramref name="subscribe"/> is <see langword="null"/>.</exception>
        public UnityDelegateOneWayToSourceBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> subscribe, 
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            _target = target ?? throw new ArgumentNullException(nameof(target));
            
            (subscribe ?? throw new ArgumentNullException(nameof(subscribe))).Invoke(target, OnValueChanged);
        }

        /// <param name="target">The target object whose value is read when bound or unbound.</param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel. At least one of <paramref name="getValueOnBound"/>
        /// or <paramref name="getValueOnUnbinding"/> must be provided.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when both <paramref name="getValueOnBound"/> and <paramref name="getValueOnUnbinding"/>
        /// are <see langword="null"/>.
        /// </exception>
        public UnityDelegateOneWayToSourceBinder(
            TTarget target,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.OneWayToSource)
        {
            if (getValueOnBound is null && getValueOnUnbinding is null)
                throw new ArgumentException($"{nameof(getValueOnBound)} and {nameof(getValueOnUnbinding)} are both null.");

            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Called after binding is established.
        /// Invokes the getValueOnBound factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnBound()
        {
            if (_getValueOnBound is not null)
                OnValueChanged(_getValueOnBound.Invoke(_target));
        }

        /// <summary>
        /// Called just before the binding is released.
        /// Invokes the getValueOnUnbinding factory with the stored <typeparamref name="TTarget"/>
        /// and pushes the returned value to the ViewModel, if the factory was provided.
        /// </summary>
        protected override void OnUnbinding()
        {
            if (_getValueOnUnbinding is not null)
                OnValueChanged(_getValueOnUnbinding.Invoke(_target));
        }

        private void OnValueChanged(T? value) =>
            ValueChanged?.Invoke(value);
    }
}