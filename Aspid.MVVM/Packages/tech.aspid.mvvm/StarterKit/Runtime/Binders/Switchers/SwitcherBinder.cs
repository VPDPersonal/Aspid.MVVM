using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="Binder"/> that switches a target value between two pre-configured options
    /// based on a bound boolean ViewModel property.
    /// </summary>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<T> : Binder, IBinder<bool>
    {
        [Tooltip("Value applied when the bound boolean is true.")]
        [SerializeField] private T _trueValue;

        [Tooltip("Value applied when the bound boolean is false.")]
        [SerializeField] private T _falseValue;

        /// <param name="trueValue">The value forwarded when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The value forwarded when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        protected SwitcherBinder(T trueValue, T falseValue, BindMode mode)
            : base(mode)
        {
            mode.ThrowExceptionIfTwo();

            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Selects the appropriate value based on <paramref name="value"/> and forwards it to
        /// <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(GetValue(value));

        /// <summary>
        /// Applies the selected <paramref name="value"/> to the underlying target.
        /// </summary>
        /// <param name="value">The value selected based on the boolean input.</param>
        protected abstract void SetValue(T value);

        private T GetValue(bool value) =>
            value ? _trueValue : _falseValue;
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that switches a target property between two pre-configured options
    /// based on a bound boolean ViewModel property.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object whose property is switched.</typeparam>
    /// <typeparam name="T">The type of value to switch between.</typeparam>
    [Serializable]
    public abstract class SwitcherBinder<TTarget, T> : TargetBinder<TTarget>, IBinder<bool>
    {
        [Tooltip("Value applied when the bound boolean is true.")]
        [SerializeField] private T _trueValue;

        [Tooltip("Value applied when the bound boolean is false.")]
        [SerializeField] private T _falseValue;

        /// <param name="target">The target object that receives the resolved value.</param>
        /// <param name="trueValue">The value forwarded when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The value forwarded when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> or <see cref="BindMode.OneWayToSource"/>.</exception>
        protected SwitcherBinder(TTarget target, T trueValue, T falseValue, BindMode mode)
            : base(target, mode)
        {
            mode.ThrowExceptionIfTwo();

            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Selects the appropriate value based on <paramref name="value"/> and forwards it to
        /// <see cref="SetValue(T)"/>.
        /// </summary>
        /// <param name="value">The boolean value received from the ViewModel.</param>
        public void SetValue(bool value) =>
            SetValue(value ? _trueValue : _falseValue);

        /// <summary>
        /// Applies the selected <paramref name="value"/> to the underlying target.
        /// </summary>
        /// <param name="value">The value selected based on the boolean input.</param>
        protected abstract void SetValue(T value);
    }
}