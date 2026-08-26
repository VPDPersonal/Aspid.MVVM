using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies a single-argument mathematical function.
    /// </summary>
    /// <remarks>
    /// Functions with a domain return zero or clamp outside it rather than yielding NaN or infinity.
    /// <para>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a NaN or
    /// an infinity cannot pass through them.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Unary Math",
        Tooltip = "Applies a single-argument mathematical function")]
    public sealed class UnaryMathConverter :
        IConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        IConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        IConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        IConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
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

        #region Return int
        int IConverter<int, int>.Convert(int value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<long, int>.Convert(long value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<float, int>.Convert(float value) =>
            NumericSaturation.ToInt(Apply(value));

        int IConverter<double, int>.Convert(double value) =>
            NumericSaturation.ToInt(Apply(value));
        #endregion

        #region Return long
        long IConverter<long, long>.Convert(long value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<int, long>.Convert(int value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<float, long>.Convert(float value) =>
            NumericSaturation.ToLong(Apply(value));

        long IConverter<double, long>.Convert(double value) =>
            NumericSaturation.ToLong(Apply(value));
        #endregion

        #region Return float
        /// <summary>
        /// Applies the configured function to the specified value.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>
        /// The result of the function. An undeclared operation reports an error and returns the value
        /// unchanged.
        /// </returns>
        public float Convert(float value) => NumericSaturation.ToFloat(Apply(value));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(Apply(value));
        #endregion

        #region Return double
        /// <inheritdoc cref="Convert(float)"/>
        public double Convert(double value) => Apply(value);

        double IConverter<int, double>.Convert(int value) =>
            Apply(value);

        double IConverter<long, double>.Convert(long value) =>
            Apply(value);

        double IConverter<float, double>.Convert(float value) =>
            Apply(value);
        #endregion

        private double Apply(double value) => _operation switch
        {
            UnaryMathOperation.Abs => Math.Abs(value),
            UnaryMathOperation.Negate => -value,
            // Not Math.Sign, which throws on a NaN; zero is the honest answer for a value with no sign.
            UnaryMathOperation.Sign => value > 0d ? 1d : value < 0d ? -1d : 0d,
            UnaryMathOperation.Sqrt => value <= 0d ? 0d : Math.Sqrt(value),
            UnaryMathOperation.Reciprocal => value == 0d ? 0d : 1d / value,
            UnaryMathOperation.Log => value <= 0d ? 0d : Math.Log(value),
            UnaryMathOperation.Log10 => value <= 0d ? 0d : Math.Log10(value),
            // Math.Log2 needs .NET Core 3.0; the two-argument Log costs one division more.
            UnaryMathOperation.Log2 => value <= 0d ? 0d : Math.Log(value, 2d),
            UnaryMathOperation.Exp => Math.Exp(value),
            UnaryMathOperation.Sin => Math.Sin(value),
            UnaryMathOperation.Cos => Math.Cos(value),
            UnaryMathOperation.Tan => Math.Tan(value),
            // Clamped rather than zeroed: a value a hair past 1 is a rounding error on the way in.
            UnaryMathOperation.Asin => Math.Asin(Clamp1(value)),
            UnaryMathOperation.Acos => Math.Acos(Clamp1(value)),
            UnaryMathOperation.Atan => Math.Atan(value),
            _ => Undeclared(value)
        };

        private double Undeclared(double value)
        {
            this.LogError($"the operation {_operation.Describe()} is not a declared {nameof(UnaryMathOperation)}",
                "Returning the value unchanged.");
            return value;
        }

        private static double Clamp1(double value)
        {
            if (double.IsNaN(value)) return 0d;

            return value < -1d ? -1d : value > 1d ? 1d : value;
        }
    }
}
