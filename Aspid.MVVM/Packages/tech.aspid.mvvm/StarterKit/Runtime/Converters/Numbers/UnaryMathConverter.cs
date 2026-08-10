using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies a single-argument mathematical function.
    /// </summary>
    /// <remarks>
    /// Eleven one-line conversions that would otherwise be eleven converter classes. The functions
    /// with a domain — square root, logarithm, reciprocal — return zero outside it rather than NaN or
    /// infinity, because a NaN reaching a <see cref="Transform"/> corrupts it silently while a zero
    /// is merely wrong.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Unary Math", Tooltip = "Applies a single-argument mathematical function")]
    public sealed class UnaryMathConverter : IConverterFloat
    {
        [Tooltip("The function to apply.")]
        [SerializeField] private UnaryMathOperation _operation;

        /// <remarks>Default: applying <see cref="UnaryMathOperation.Abs"/>.</remarks>
        public UnaryMathConverter() { }

        /// <param name="operation">The function to apply.</param>
        public UnaryMathConverter(UnaryMathOperation operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// Applies the configured function to the specified value.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>The result of the function.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public float Convert(float value) => _operation switch
        {
            UnaryMathOperation.Abs => Mathf.Abs(value),
            UnaryMathOperation.Negate => -value,
            UnaryMathOperation.Sign => Mathf.Sign(value) * (value == 0f ? 0f : 1f),
            UnaryMathOperation.Sqrt => value <= 0f ? 0f : Mathf.Sqrt(value),
            UnaryMathOperation.Reciprocal => value == 0f ? 0f : 1f / value,
            UnaryMathOperation.Log => value <= 0f ? 0f : Mathf.Log(value),
            UnaryMathOperation.Log10 => value <= 0f ? 0f : Mathf.Log10(value),
            UnaryMathOperation.Exp => Mathf.Exp(value),
            UnaryMathOperation.Sin => Mathf.Sin(value),
            UnaryMathOperation.Cos => Mathf.Cos(value),
            UnaryMathOperation.Tan => Mathf.Tan(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };
    }
}
