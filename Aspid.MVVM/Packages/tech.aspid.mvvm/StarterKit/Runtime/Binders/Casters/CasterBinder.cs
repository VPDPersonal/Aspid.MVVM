#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts a <typeparamref name="TFrom"/> value
    /// to <typeparamref name="TTo"/> and forwards it to a target setter.
    /// </summary>
    /// <typeparam name="TFrom">The source value type produced by the ViewModel binding.</typeparam>
    /// <typeparam name="TTo">The target value type expected by the setter.</typeparam>
    public class CasterBinder<TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly Action<TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        /// <param name="setValue">The action invoked with the converted <typeparamref name="TTo"/> value.</param>
        /// <param name="converter">The converter used to transform a <typeparamref name="TFrom"/> value to <typeparamref name="TTo"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public CasterBinder(
            Action<TTo?> setValue,
            IConverter<TFrom?, TTo?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to <typeparamref name="TTo"/> and forwards it to the target setter.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(TFrom? value) =>
            _setValue(_converter.Convert(value));
    }

    /// <summary>
    /// <see cref="Binder"/> implementing <see cref="IBinder{T}"/> that converts a <typeparamref name="TFrom"/> value
    /// to <typeparamref name="TTo"/> and forwards it, together with the stored <typeparamref name="TTarget"/>, to a target setter.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is set.</typeparam>
    /// <typeparam name="TFrom">The source value type produced by the ViewModel binding.</typeparam>
    /// <typeparam name="TTo">The target value type expected by the setter.</typeparam>
    public class CasterBinder<TTarget, TFrom, TTo> : Binder, IBinder<TFrom>
    {
        private readonly TTarget _target;
        private readonly Action<TTarget, TTo?> _setValue;
        private readonly IConverter<TFrom?, TTo?> _converter;

        /// <param name="target">The target object passed as the first argument to <paramref name="setValue"/>.</param>
        /// <param name="setValue">The action invoked with the target and the converted <typeparamref name="TTo"/> value.</param>
        /// <param name="converter">The converter used to transform a <typeparamref name="TFrom"/> value to <typeparamref name="TTo"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="target"/>, <paramref name="setValue"/> or <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        public CasterBinder(
            TTarget target,
            Action<TTarget, TTo?> setValue,
            IConverter<TFrom?, TTo?> converter,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _setValue = setValue ?? throw new ArgumentNullException(nameof(setValue));
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts <paramref name="value"/> to <typeparamref name="TTo"/> and forwards it, with the stored target, to the target setter.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(TFrom? value) =>
            _setValue(_target, _converter.Convert(value));
    }
}
