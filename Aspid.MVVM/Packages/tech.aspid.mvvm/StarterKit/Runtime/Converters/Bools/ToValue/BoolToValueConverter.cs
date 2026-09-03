#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks one of two authored values based on a boolean, and reads the boolean back out of them.
    /// </summary>
    /// <typeparam name="T">The type of the values to pick between.</typeparam>
    /// <remarks>The reverse direction matches by default equality, so the two branches have to differ.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Bool/To Value",
        Name = "Bool To Value",
        Tooltip = "Picks one of two authored values based on a boolean")]
    public class BoolToValueConverter<T> : ITwoWayConverter<bool, T?>
    {
        [Tooltip("Returned when the bound value is true.")]
        [SerializeField] private T? _trueValue;

        [Tooltip("Returned when the bound value is false.")]
        [SerializeField] private T? _falseValue;

        [Tooltip("Returned when Convert Back meets a value matching neither branch, or when both branches hold the same value.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private bool _convertBackFallback;

        protected BoolToValueConverter() { }

        /// <param name="trueValue">Returned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">Returned when the bound value is <see langword="false"/>.</param>
        /// <param name="convertBackFallback">
        /// Returned when <see cref="ConvertBack"/> meets a value matching neither branch, nor when both
        /// branches hold the same value. When omitted, <see langword="false"/>.
        /// </param>
        public BoolToValueConverter(
            T trueValue,
            T falseValue,
            bool convertBackFallback = false)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
            _convertBackFallback = convertBackFallback;
        }

        /// <summary>
        /// Picks the value authored for the specified boolean.
        /// </summary>
        /// <param name="value">The bound boolean.</param>
        /// <returns>The value authored for that branch.</returns>
        public T? Convert(bool value) => value
            ? _trueValue
            : _falseValue;

        /// <summary>
        /// Reads the boolean back out of the specified value.
        /// </summary>
        /// <param name="value">The value to match against the two authored ones.</param>
        /// <returns>
        /// <see langword="true"/> or <see langword="false"/> when the value matches the branch
        /// authored for it; otherwise, the fallback.
        /// </returns>
        public bool ConvertBack(T? value)
        {
            var comparer = EqualityComparer<T?>.Default;

            if (comparer.Equals(_trueValue, _falseValue))
            {
                return this.UseFallback(
                    fallback: _convertBackFallback,
                    problem: $"both branches hold {_trueValue.Describe()}, so the boolean cannot be read back");
            }

            if (comparer.Equals(value, _trueValue)) return true;
            if (comparer.Equals(value, _falseValue)) return false;

            return this.UseFallback(
                fallback: _convertBackFallback,
                problem: value.Expected("one of the two authored values"));
        }
    }
}
