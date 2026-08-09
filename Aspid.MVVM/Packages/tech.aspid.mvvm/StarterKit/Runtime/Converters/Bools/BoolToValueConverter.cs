using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks one of two authored values based on a boolean.
    /// </summary>
    /// <typeparam name="T">The type of the values to pick between.</typeparam>
    /// <remarks>
    /// This is the converter form of the <c>Switcher</c> binder family: instead of a binder per
    /// target property that knows how to hold two values, one converter turns the boolean into the
    /// value and any ordinary binder applies it. Closing it over a colour, a sprite, a material or a
    /// string covers what a dozen switcher binders each do for one property.
    /// </remarks>
    [Serializable]
    public sealed class BoolToValueConverter<T> : IConverter<bool, T>
    {
        [Tooltip("Returned when the bound value is true.")]
        [SerializeField] private T _trueValue = default!;

        [Tooltip("Returned when the bound value is false.")]
        [SerializeField] private T _falseValue = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolToValueConverter{T}"/> class with default values.
        /// </summary>
        public BoolToValueConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoolToValueConverter{T}"/> class.
        /// </summary>
        /// <param name="trueValue">Returned when the bound value is <see langword="true"/>.</param>
        /// <param name="falseValue">Returned when the bound value is <see langword="false"/>.</param>
        public BoolToValueConverter(T trueValue, T falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        /// <summary>
        /// Picks the value matching the specified boolean.
        /// </summary>
        /// <param name="value">The bound boolean.</param>
        /// <returns>The value authored for that branch.</returns>
        public T Convert(bool value) => value ? _trueValue : _falseValue;
    }
}
