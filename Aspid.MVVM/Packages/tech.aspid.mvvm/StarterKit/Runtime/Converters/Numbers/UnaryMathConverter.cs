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
    /// Fifteen one-line conversions that would otherwise be fifteen converter classes. The functions
    /// with a domain — square root, logarithm, reciprocal, the two inverse trigonometric ones —
    /// return zero or clamp outside it rather than yielding NaN or infinity, because a NaN reaching a
    /// <see cref="Transform"/> corrupts it silently while a zero is merely wrong.
    /// <para>
    /// That guard covers the domain, not every non-finite input. It tests <c>value &lt;= 0</c>, which
    /// a NaN fails, so a NaN arriving from upstream passes through the logarithms and the reciprocal
    /// unchanged, and the inverse trigonometric pair turn it into zero's answer rather than into NaN.
    /// An infinity is likewise inside the domain of a logarithm and comes back infinite. If the
    /// source can produce a NaN, guard it before this converter rather than expecting this one to.
    /// </para>
    /// <para>
    /// The arithmetic runs in <see cref="double"/> whichever overload is called, so the
    /// <see cref="float"/> result is the double result rounded once rather than a chain of float
    /// operations; the difference shows on <see cref="UnaryMathOperation.Exp"/> and the logarithms.
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
