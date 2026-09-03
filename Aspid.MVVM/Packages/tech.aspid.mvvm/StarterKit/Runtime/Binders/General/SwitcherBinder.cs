#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that applies one of two preset values depending on a bound <see langword="bool"/>,
    /// passing the chosen value through an optional converter first.
    /// </summary>
    /// <typeparam name="T">The type of the values switched between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
    {
        [Tooltip("Value applied when the bound bool is true.")]
        [SerializeField] private T? _trueValue;

        [Tooltip("Value applied when the bound bool is false.")]
        [SerializeField] private T? _falseValue;

        [Tooltip("Optional converter applied to the chosen value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected SwitcherBinder() { }

        /// <param name="trueValue">The value applied when the bound <see langword="bool"/> is <see langword="true"/>.</param>
        /// <param name="falseValue">The value applied when the bound <see langword="bool"/> is <see langword="false"/>.</param>
        /// <param name="converter">The converter applied to the chosen value, or <see langword="null"/> to use it unchanged.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        protected SwitcherBinder(
            T trueValue, 
            T falseValue,
            IConverter<T?, T?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _converter = converter;
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Chooses the true or false value, converts it via <see cref="GetConvertedValue"/> and forwards it to <see cref="SetValue(T?)"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the chosen, converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(T? value);

        /// <summary>
        /// Converts <paramref name="value"/> with the serialized converter, or returns it unchanged when none is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T? GetConvertedValue(T? value) => _converter is null 
            ? value 
            : _converter.Convert(value);
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that applies one of two preset values depending on a bound <see langword="bool"/>,
    /// passing the chosen value through an optional converter first.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is switched.</typeparam>
    /// <typeparam name="T">The type of the values switched between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<TTarget, T> : TargetBinder<TTarget>, IBinder<bool>
    {
        [Tooltip("Value applied when the bound bool is true.")]
        [SerializeField] private T? _trueValue;

        [Tooltip("Value applied when the bound bool is false.")]
        [SerializeField] private T? _falseValue;

        [Tooltip("Optional converter applied to the chosen value; empty leaves it as-is.")]
        [SerializeReference] private IConverter<T?, T?>? _converter;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected SwitcherBinder() { }

        /// <param name="target">The target object that receives the chosen value.</param>
        /// <param name="trueValue">The value applied when the bound <see langword="bool"/> is <see langword="true"/>.</param>
        /// <param name="falseValue">The value applied when the bound <see langword="bool"/> is <see langword="false"/>.</param>
        /// <param name="converter">The converter applied to the chosen value, or <see langword="null"/> to use it unchanged.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        protected SwitcherBinder(
            TTarget target,
            T trueValue,
            T falseValue,
            IConverter<T?, T?>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _converter = converter;
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Chooses the true or false value, converts it via <see cref="GetConvertedValue"/> and forwards it to <see cref="SetValue(T?)"/>.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(GetConvertedValue(value ? _trueValue : _falseValue));

        /// <summary>
        /// Applies the chosen, converted <paramref name="value"/> to the target.
        /// </summary>
        /// <param name="value">The value to apply.</param>
        protected abstract void SetValue(T? value);

        /// <summary>
        /// Converts <paramref name="value"/> with the serialized converter, or returns it unchanged when none is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual T? GetConvertedValue(T? value) => _converter is null 
            ? value 
            : _converter.Convert(value);
    }
}
