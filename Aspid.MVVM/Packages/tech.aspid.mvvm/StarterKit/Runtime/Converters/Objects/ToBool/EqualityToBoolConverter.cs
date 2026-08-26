using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tests a bound value against an authored one.
    /// </summary>
    /// <typeparam name="T">The type of the values being compared.</typeparam>
    /// <remarks>
    /// Undervalue equality an empty operand also matches a destroyed
    /// <see cref="UnityEngine.Object"/>, so the converter doubles as an is-null test. Reference
    /// equality compares the instances raw.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Object/To Bool",
        Name = "Equals",
        Tooltip = "Tests a bound value against an authored one")]
    public class EqualityToBoolConverter<T> : IConverter<T, bool>
    {
        private static readonly bool _isReferenceType = !typeof(T).IsValueType;

        [Tooltip("The value the bound one is compared against. An empty operand also matches a destroyed Unity object.")]
        [SerializeField] private T? _operand;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        [Tooltip("Compare by instance instead of by value. Ignored for value types.")]
        [SerializeField] private bool _referenceEquality;

        /// <remarks>Default: comparing by value against an empty operand.</remarks>
        public EqualityToBoolConverter() { }

        /// <param name="operand">
        /// The value the bound one is compared against. An empty operand also matches a destroyed
        /// Unity object.
        /// </param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        /// <param name="referenceEquality">
        /// If <see langword="true"/>, compares by instance instead of by value. Ignored for value types.
        /// </param>
        public EqualityToBoolConverter(T? operand, bool isInvert = false, bool referenceEquality = false)
        {
            _operand = operand;
            _isInvert = isInvert;
            _referenceEquality = referenceEquality;
        }

        /// <summary>
        /// Compares the specified value with the authored one.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>Whether the two are equal — the same instance under reference equality — inverted when configured.</returns>
        public bool Convert(T value)
        {
            var equal = _referenceEquality && _isReferenceType
                ? ReferenceEquals(value, _operand)
                : ValueEquals(value, _operand);

            return _isInvert ? !equal : equal;
        }

        private static bool ValueEquals(T? value, T? operand)
        {
            if (operand is null)
            {
                return value is Object unityValue
                    ? unityValue == null
                    : value is null;
            }

            return value is null
                ? operand is Object unityOperand && unityOperand == null
                : EqualityComparer<T?>.Default.Equals(value, operand);
        }
    }
}
