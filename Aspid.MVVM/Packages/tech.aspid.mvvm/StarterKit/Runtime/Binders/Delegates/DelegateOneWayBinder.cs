#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that forwards each ViewModel value to a setter action.
    /// </summary>
    /// <typeparam name="T">The type of the bound value.</typeparam>
    public class DelegateOneWayBinder<T> : Binder, IBinder<T>
    {
        private readonly Action<T?> _setValue;

        /// <param name="setValue">The action invoked with each value received from the ViewModel.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// w<exception cref="ArgumentNullException">Thrown when <paramref name="setValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public DelegateOneWayBinder(
            Action<T?> setValue, 
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/> to the setter action.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(value);
    }

    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that forwards each ViewModel value, together with
    /// the stored <typeparamref name="TTarget"/>, to a setter action.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is set.</typeparam>
    /// <typeparam name="T">The type of the bound value.</typeparam>
    public class DelegateOneWayBinder<TTarget, T> : Binder, IBinder<T>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, T?> _setValue;

        /// <param name="target">The target object passed as the first argument to <paramref name="setValue"/>.</param>
        /// <param name="setValue">The action invoked with the target and each value received from the ViewModel.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/> or <paramref name="setValue"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public DelegateOneWayBinder(
            TTarget target,
            Action<TTarget, T?> setValue,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
        }

        /// <summary>
        /// Forwards <paramref name="value"/>, with the stored target, to the setter action.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(T? value) =>
            _setValue.Invoke(_target, value);
    }
}
