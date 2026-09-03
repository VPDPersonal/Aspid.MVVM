#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies a single-argument mathematical function.
    /// </summary>
    /// <remarks>Functions with a domain return zero or clamp outside it rather than yielding NaN or infinity.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Unary Math",
        Tooltip = "Applies a single-argument mathematical function")]
    public sealed class UnaryMathConverter : NumberConverter
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
        /// Applies the configured function.
        /// </summary>
        /// <param name="value">The number to transform.</param>
        /// <returns>The result. An undeclared operation reports an error and returns the value unchanged.</returns>
        protected override double Apply(double value) => _operation switch
        {
            UnaryMathOperation.Abs => Math.Abs(value),
            UnaryMathOperation.Negate => -value,
            UnaryMathOperation.Sign => value > 0d ? 1d : value < 0d ? -1d : 0d,
            UnaryMathOperation.Sqrt => value <= 0d ? 0d : Math.Sqrt(value),
            UnaryMathOperation.Reciprocal => value is 0d ? 0d : 1d / value,
            UnaryMathOperation.Log => value <= 0d ? 0d : Math.Log(value),
            UnaryMathOperation.Log10 => value <= 0d ? 0d : Math.Log10(value),
            UnaryMathOperation.Log2 => value <= 0d ? 0d : Math.Log(value, 2d),
            UnaryMathOperation.Exp => Math.Exp(value),
            UnaryMathOperation.Sin => Math.Sin(value),
            UnaryMathOperation.Cos => Math.Cos(value),
            UnaryMathOperation.Tan => Math.Tan(value),
            UnaryMathOperation.Asin => Math.Asin(Clamp1(value)),
            UnaryMathOperation.Acos => Math.Acos(Clamp1(value)),
            UnaryMathOperation.Atan => Math.Atan(value),
            _ => Undeclared(value)
        };

        private double Undeclared(double value)
        {
            this.LogError(
                problem: $"the operation {_operation.Describe()} is not a declared {nameof(UnaryMathOperation)}",
                consequence: "Returning the value unchanged.");

            return value;
        }

        private static double Clamp1(double value)
        {
            if (double.IsNaN(value)) return 0d;

            return value < -1d ? -1d : value > 1d ? 1d : value;
        }
    }
}
