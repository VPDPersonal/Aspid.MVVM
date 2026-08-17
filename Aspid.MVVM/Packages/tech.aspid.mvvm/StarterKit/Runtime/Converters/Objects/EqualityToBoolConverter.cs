using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tests a bound value against an authored one.
    /// </summary>
    /// <typeparam name="T">The type of the values being compared.</typeparam>
    /// <remarks>
    /// The generalisation of <see cref="NumberToBoolConverter"/> beyond numbers: "is this the
    /// selected item?", "is this the equipped weapon?". Comparison uses the type's own equality.
    /// </remarks>
    [Serializable]
    public sealed class EqualityToBoolConverter<T> : IConverter<T, bool>
    {
        [Tooltip("The value the bound one is compared against.")]
        [SerializeField] private T _operand = default!;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        public EqualityToBoolConverter() { }

        /// <param name="operand">The value the bound one is compared against.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public EqualityToBoolConverter(T operand, bool isInvert = false)
        {
            _operand = operand;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Compares the specified value with the authored one.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>Whether the two are equal, inverted when configured.</returns>
        public bool Convert(T value)
        {
            var equal = EqualityComparer<T>.Default.Equals(value, _operand);
            return _isInvert ? !equal : equal;
        }
    }
}
