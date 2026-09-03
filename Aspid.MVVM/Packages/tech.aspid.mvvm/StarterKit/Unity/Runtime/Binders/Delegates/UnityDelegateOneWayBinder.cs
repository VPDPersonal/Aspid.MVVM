#nullable enable
using System;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that forwards values of type <typeparamref name="T"/>
    /// from the ViewModel to a <see cref="UnityAction{T}"/> setter.
    /// </summary>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    [System.Obsolete("Use the DelegateOneWay binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityDelegateOneWayBinder<T> : Binder, IBinder<T>
    {
        private readonly UnityAction<T?> _setValue;

        /// <param name="setValue">The <see cref="UnityAction{T}"/> invoked with each new value received from the ViewModel.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public UnityDelegateOneWayBinder(UnityAction<T?> setValue, BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> to the setter action.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(value);
    }
    
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that forwards values of type <typeparamref name="T"/>
    /// from the ViewModel to a <see cref="UnityAction{T0,T1}"/> setter together with a stored <typeparamref name="TTarget"/> instance.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is being set.</typeparam>
    /// <typeparam name="T">The type of the value to bind.</typeparam>
    /// <remarks>
    /// Holding a <typeparamref name="TTarget"/> instance avoids capturing it in a closure when using
    /// method-group-style setters on Unity components.
    /// </remarks>
    [System.Obsolete("Use the DelegateOneWay binder instead: it takes a plain Action, which a UnityAction converts to implicitly. The Unity-flavoured copies exist only for that conversion and will be removed in the next major version.")]
    public class UnityDelegateOneWayBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly UnityAction<TTarget, T?> _setValue;

        /// <param name="target">The target object passed as the first argument to <paramref name="setValue"/>.</param>
        /// <param name="setValue">
        /// The <see cref="UnityAction{T0,T1}"/> invoked with the target and each new value received from the ViewModel.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.
        /// </exception>
        public UnityDelegateOneWayBinder(
            TTarget target,
            UnityAction<TTarget, T?> setValue, 
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> together with the stored target to the setter action.
        /// </summary>
        /// <param name="value">The new value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);
    }
}