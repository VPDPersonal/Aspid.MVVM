using Aspid.FastTools.Types;
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
    /// Comparison uses the type's own equality; reference equality is the option for a type whose
    /// <c>Equals</c> answers a different question than the binding does.
    /// <para>
    /// One case to know about with a <c>UnityEngine.Object</c>: an operand left empty.
    /// <see cref="EqualityComparer{T}"/> settles a null operand with a reference check before Unity's
    /// own equality is consulted, so a destroyed object does not match it here — ask that question with
    /// <c>UnityObjectNullToBoolConverter</c>, which goes through the operator.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Object", Name = "Equality To Bool", Tooltip = "Tests a bound value against an authored one")]
    public sealed class EqualityToBoolConverter<T> : IConverter<T, bool>
    {
        [Tooltip("The value the bound one is compared against.")]
        [SerializeField] private T _operand = default!;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        [Tooltip("Ask whether it is the same instance instead of whether it is equal. Ignored for a value type, which has no instances to tell apart.")]
        [SerializeField] private bool _referenceEquality;

        // Boxing a struct hands ReferenceEquals two fresh objects, so reference equality over a value
        // type would answer false for every pair — including a value and itself. The type's own
        // equality is the only reading of the option that is ever useful there.
        private static readonly bool IsReferenceType = !typeof(T).IsValueType;

        public EqualityToBoolConverter() { }

        /// <param name="operand">The value the bound one is compared against.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        /// <param name="referenceEquality">
        /// If <see langword="true"/>, compares by reference rather than by the type's own equality.
        /// Has no effect when <typeparamref name="T"/> is a value type.
        /// </param>
        public EqualityToBoolConverter(T operand, bool isInvert = false, bool referenceEquality = false)
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
            var equal = _referenceEquality && IsReferenceType
                ? ReferenceEquals(value, _operand)
                : EqualityComparer<T>.Default.Equals(value, _operand);

            return _isInvert ? !equal : equal;
        }
    }
}
