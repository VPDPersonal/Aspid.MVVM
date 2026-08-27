using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that switches a target property between two pre-configured options
    /// based on a bound boolean ViewModel property, with optional value conversion via <see cref="IConverter{TFrom, TTo}"/> before applying.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is switched.</typeparam>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinderWithConverter<TTarget, T> : TargetBinder<TTarget>, IBinder<bool>
    {
        [Tooltip("Value applied when the bound boolean is true.")]
        [SerializeField] private T _trueValue;

        [Tooltip("Value applied when the bound boolean is false.")]
        [SerializeField] private T _falseValue;

        [Tooltip("Optional converter applied to the selected value before it is set.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

        /// <param name="target">The target object that receives the resolved value.</param>
        /// <param name="trueValue">The value forwarded when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The value forwarded when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">
        /// An optional converter applied to the selected value before it is forwarded to the target.
        /// Pass <see langword="null"/> to forward the value unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        protected SwitcherBinderWithConverter(
            TTarget target,
            T trueValue,
            T falseValue,
            IConverter<T?, T?>? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _converter = converter;
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Selects the true or false value based on <paramref name="value"/>, converts it via <see cref="GetConvertedValue"/>,
        /// and forwards it to <see cref="SetValue(T?)"/>.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the selected and converted <paramref name="value"/> to the underlying target.
        /// </summary>
        /// <param name="value">The value selected and converted based on the boolean input.</param>
        protected abstract void SetValue(T? value);

        /// <summary>
        /// Converts <paramref name="value"/> using the serialized converter, or returns it unchanged if no converter is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T? GetConvertedValue(T value) =>
            _converter is null ? value : _converter.Convert(value);
    }
}
