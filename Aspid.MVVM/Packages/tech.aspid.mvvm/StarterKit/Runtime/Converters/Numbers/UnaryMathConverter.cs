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
    /// The functions with a domain — square root, logarithm, reciprocal, the inverse trigonometric
    /// pair — return zero or clamp outside it rather than yielding NaN or infinity.
    /// <para>
    /// That guard covers the domain, not every non-finite input: it tests <c>value &lt;= 0</c>, which a
    /// NaN fails, so a NaN passes through the logarithms and the reciprocal unchanged and an infinity
    /// comes back infinite. Guard a source that can produce a NaN before this converter. The arithmetic
    /// runs in <see cref="double"/> whichever overload is called.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Unary Math", Tooltip = "Applies a single-argument mathematical function")]
    public sealed class UnaryMathConverter : IConverterFloat, IConverter<double, double>
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
        public float Convert(float value) => (float)Apply(value);

        /// <inheritdoc cref="Convert(float)"/>
        public double Convert(double value) => Apply(value);

        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        private double Apply(double value) => _operation switch
        {
            UnaryMathOperation.Abs => Math.Abs(value),
            UnaryMathOperation.Negate => -value,
            // Not Math.Sign, which throws on a NaN. Zero is the honest answer for a value with no
            // sign, and it is what the float overload returned before.
            UnaryMathOperation.Sign => value > 0d ? 1d : value < 0d ? -1d : 0d,
            UnaryMathOperation.Sqrt => value <= 0d ? 0d : Math.Sqrt(value),
            UnaryMathOperation.Reciprocal => value == 0d ? 0d : 1d / value,
            UnaryMathOperation.Log => value <= 0d ? 0d : Math.Log(value),
            UnaryMathOperation.Log10 => value <= 0d ? 0d : Math.Log10(value),
            // Math.Log2 arrived with .NET Core 3.0; the two-argument Log has been there since .NET 2.0
            // and is one division more.
            UnaryMathOperation.Log2 => value <= 0d ? 0d : Math.Log(value, 2d),
            UnaryMathOperation.Exp => Math.Exp(value),
            UnaryMathOperation.Sin => Math.Sin(value),
            UnaryMathOperation.Cos => Math.Cos(value),
            UnaryMathOperation.Tan => Math.Tan(value),
            // Clamped rather than zeroed: a value a hair past 1 is a rounding error on the way in, and
            // the nearest legal answer is the right-angle case, not zero.
            UnaryMathOperation.Asin => Math.Asin(Clamp1(value)),
            UnaryMathOperation.Acos => Math.Acos(Clamp1(value)),
            UnaryMathOperation.Atan => Math.Atan(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        private static double Clamp1(double value)
        {
            if (double.IsNaN(value)) return 0d;

            return value < -1d ? -1d : value > 1d ? 1d : value;
        }
    }
}
