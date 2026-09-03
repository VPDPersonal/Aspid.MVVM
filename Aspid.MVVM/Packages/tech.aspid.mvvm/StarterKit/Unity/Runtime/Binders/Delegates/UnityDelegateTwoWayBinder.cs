#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/>
    /// that synchronises values of type <typeparamref name="T"/> in both directions between the ViewModel and the View.
    /// </summary>
    /// <typeparam name="T">The type of the value exchanged between View and ViewModel.</typeparam>
    [System.Obsolete("Use the DelegateTwoWay binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityDelegateTwoWayBinder<T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        private readonly UnityAction<T?> _setValue;
        private readonly Func<T?>? _getValueOnBound;
        private readonly Func<T?>? _getValueOnUnbinding;

        /// <param name="subscribe">
        /// A <see cref="UnityAction{T}"/> that receives the internal <see cref="OnValueChanged"/> callback and registers it
        /// with the appropriate View event so that View changes are propagated to the ViewModel.
        /// </param>
        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked when a new value arrives from the ViewModel.</param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="subscribe"/> or <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public UnityDelegateTwoWayBinder(
            UnityAction<UnityAction<T>> subscribe, 
            UnityAction<T?> setValue,
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : this(setValue, getValueOnBound, getValueOnUnbinding)
        {
            (subscribe ?? throw new ArgumentNullException(nameof(subscribe))).Invoke(OnValueChanged);
        }

        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked when a new value arrives from the ViewModel.</param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked when the binding is established; the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked just before the binding is released; the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        public UnityDelegateTwoWayBinder(
            UnityAction<T?> setValue,
            Func<T?>? getValueOnBound = null,
            Func<T?>? getValueOnUnbinding = null)
            : base(BindMode.TwoWay)
        {
            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Forwards <paramref name="value"/> from the ViewModel to the View setter.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(value);

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
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> and <see cref="IReverseBinder{T}"/>
    /// that synchronises values of type <typeparamref name="T"/> in both directions between the ViewModel
    /// and a specific <typeparamref name="TTarget"/> View object.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target View object that both receives and exposes the value.</typeparam>
    /// <typeparam name="T">The type of the value exchanged between View and ViewModel.</typeparam>
    /// <remarks>
    /// Holds a reference to a <typeparamref name="TTarget"/> instance and passes it to all factory
    /// functions, avoiding closures over Unity component references.
    /// Otherwise behaves identically to <see cref="UnityDelegateTwoWayBinder{T}"/>.
    /// </remarks>
    [System.Obsolete("Use the DelegateTwoWay binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityDelegateTwoWayBinder<TTarget, T> : Binder, IBinder<T>, IReverseBinder<T>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?> _setValue;
        private readonly Func<TTarget, T?>? _getValueOnBound;
        private readonly Func<TTarget, T?>? _getValueOnUnbinding;

        /// <param name="target">The target View object.</param>
        /// <param name="subscribe">
        /// A <see cref="UnityAction{T0,T1}"/> that receives <paramref name="target"/> and the internal <see cref="OnValueChanged"/>
        /// callback, and registers it with the appropriate View event.
        /// </param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and each new value received from the ViewModel.
        /// </param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/>, <paramref name="subscribe"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        public UnityDelegateTwoWayBinder(
            TTarget target,
            UnityAction<TTarget, UnityAction<T>> subscribe,
            UnityAction<TTarget, T?> setValue,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.TwoWay)
        {
            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            
            (subscribe ?? throw new ArgumentNullException(nameof(subscribe))).Invoke(target, OnValueChanged);
        }

        /// <param name="target">The target View object.</param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and each new value received from the ViewModel.
        /// </param>
        /// <param name="getValueOnBound">
        /// Optional factory invoked with <paramref name="target"/> when the binding is established;
        /// the returned value is pushed to the ViewModel. At least one of <paramref name="getValueOnBound"/>
        /// or <paramref name="getValueOnUnbinding"/> must be non-<see langword="null"/>.
        /// </param>
        /// <param name="getValueOnUnbinding">
        /// Optional factory invoked with <paramref name="target"/> just before the binding is released;
        /// the returned value is pushed to the ViewModel.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when both <paramref name="getValueOnBound"/> and <paramref name="getValueOnUnbinding"/>
        /// are <see langword="null"/>.
        /// </exception>
        public UnityDelegateTwoWayBinder(
            TTarget target,
            UnityAction<TTarget, T?> setValue,
            Func<TTarget, T?>? getValueOnBound = null,
            Func<TTarget, T?>? getValueOnUnbinding = null)
            : base(BindMode.TwoWay)
        {
            if (getValueOnBound is null && getValueOnUnbinding is null)
                throw new ArgumentException($"{nameof(getValueOnBound)} and {nameof(getValueOnUnbinding)} are both null.");
            
            _getValueOnBound = getValueOnBound;
            _getValueOnUnbinding = getValueOnUnbinding;
            
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Raised when the View-side value changes and should be propagated to the ViewModel.
        /// </summary>
        public event Action<T?>? ValueChanged;

        /// <summary>
        /// Forwards <paramref name="value"/> from the ViewModel to the View setter together with the stored target.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);

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