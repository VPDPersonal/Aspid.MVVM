using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts numeric values by applying arithmetic operations with a coefficient.
    /// </summary>
    /// <remarks>
    /// Computed in <see cref="double"/>; the int and long overloads truncate and saturate, so a TwoWay
    /// integer binding with a fractional coefficient drifts.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Arithmetic",
        Tooltip = "Converts numeric values by applying arithmetic operations with a coefficient")]
    public sealed class ArithmeticNumberConverter :
        ITwoWayConverter<int, int>, IConverter<int, long>, IConverter<int, float>, IConverter<int, double>,
        ITwoWayConverter<long, long>, IConverter<long, int>, IConverter<long, float>, IConverter<long, double>,
        ITwoWayConverter<float, float>, IConverter<float, int>, IConverter<float, long>, IConverter<float, double>,
        ITwoWayConverter<double, double>, IConverter<double, int>, IConverter<double, long>, IConverter<double, float>
    {
        [Tooltip("The number the operation is applied with.")]
        [SerializeField] private double _coefficient = 1d;

        [Tooltip("The arithmetic applied to the number.")]
        [SerializeField] private NumberOperation _operation;

        [Tooltip("Returned when the arithmetic cannot be done.")]
        [SerializeField] private ConverterFallback<double> _fallback = new(0d, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: adding a coefficient of one.</remarks>
        public ArithmeticNumberConverter() { }

        /// <param name="operation">The arithmetic operation to perform.</param>
        /// <param name="coefficient">
        /// The number the operation is applied with. Dividing by a zero coefficient reports an error
        /// and falls back.
        /// </param>
        /// <param name="fallback">
        /// Returned when the operation is undeclared, divides by zero, or cannot be undone.
        /// When omitted, returns the input value unchanged.
        /// </param>
        public ArithmeticNumberConverter(
            NumberOperation operation,
            double coefficient,
            ConverterFallback<double>? fallback = null)
        {
            _operation = operation;
            _coefficient = coefficient;
            _fallback = fallback ?? _fallback;
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
        float IConverter<float, float>.Convert(float value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<int, float>.Convert(int value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<long, float>.Convert(long value) =>
            NumericSaturation.ToFloat(Apply(value));

        float IConverter<double, float>.Convert(double value) =>
            NumericSaturation.ToFloat(Apply(value));
        #endregion

        #region Return double
        double IConverter<double, double>.Convert(double value) => Apply(value);

        /// <summary>
        /// Applies the configured arithmetic to the specified number.
        /// </summary>
        /// <param name="value">The number to transform.</param>
        /// <returns>
        /// The result, always in <see cref="double"/>. An undeclared operation reports an error and
        /// returns the fallback.
        /// </returns>
        public double Apply(double value) => _operation switch
        {
            NumberOperation.Plus => value + _coefficient,
            NumberOperation.Minus => value - _coefficient,
            NumberOperation.Division => Divide(value),
            NumberOperation.Multiply => value * _coefficient,
            NumberOperation.Modulo => Modulo(value),
            NumberOperation.Power => Math.Pow(value, _coefficient),
            NumberOperation.ReverseSubtract => _coefficient - value,
            NumberOperation.ReverseDivide => ReverseDivide(value),
            _ => Undeclared(value)
        };

        double IConverter<int, double>.Convert(int value) =>
            Apply(value);

        double IConverter<float, double>.Convert(float value) =>
            Apply(value);

        double IConverter<long, double>.Convert(long value) =>
            Apply(value);
        #endregion

        #region Convert back
        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            Undo(value);

        float ITwoWayConverter<float, float>.ConvertBack(float value) =>
            NumericSaturation.ToFloat(Undo(value));

        int ITwoWayConverter<int, int>.ConvertBack(int value) =>
            NumericSaturation.ToInt(Undo(value));

        long ITwoWayConverter<long, long>.ConvertBack(long value) =>
            NumericSaturation.ToLong(Undo(value));

        /// <summary>
        /// Reverses <see cref="Apply"/>.
        /// </summary>
        /// <param name="value">The number to transform back.</param>
        /// <returns>
        /// The number the forward pass was given, or the fallback where the operation cannot be
        /// undone: a zero coefficient, a root with no real answer, or an undeclared operation.
        /// A <see cref="NumberOperation.Modulo"/> returns <paramref name="value"/> unchanged
        /// without reporting.
        /// </returns>
        public double Undo(double value) => _operation switch
        {
            NumberOperation.Plus => value - _coefficient,
            NumberOperation.Minus => value + _coefficient,
            NumberOperation.Division => UndoDivide(value),
            NumberOperation.Multiply => UndoMultiply(value),
            NumberOperation.Power => UndoPower(value),
            // Both are their own inverse: c - (c - x) is x, and c / (c / x) is x.
            NumberOperation.ReverseSubtract => _coefficient - value,
            NumberOperation.ReverseDivide => ReverseDivide(value),
            // Modulo discards which multiple the value came from; there is nothing to undo it with.
            NumberOperation.Modulo => value,
            _ => Undeclared(value)
        };
        #endregion

        private double Divide(double value)
        {
            if (_coefficient != 0)
                return value / _coefficient;

            return DivideByZero(value);
        }

        // C#'s % keeps the sign of the left operand, so -1 % 360 is -1 rather than 359.
        private double Modulo(double value)
        {
            if (_coefficient == 0) return DivideByZero(value);

            var remainder = value % _coefficient;
            return remainder < 0 ? remainder + Math.Abs(_coefficient) : remainder;
        }

        private double UndoMultiply(double value)
        {
            if (_coefficient != 0)
                return value / _coefficient;

            return ZeroCoefficient(value);
        }

        // Multiplying by the zero would collapse every TwoWay write to zero.
        private double UndoDivide(double value)
        {
            if (_coefficient != 0)
                return value * _coefficient;

            return ZeroCoefficient(value);
        }

        private double UndoPower(double value)
        {
            if (_coefficient == 0) return ZeroCoefficient(value);

            // An incoming NaN passes through; only a root that made one is an error.
            if (double.IsNaN(value)) return value;

            // Math.Pow of a negative value and a fractional reciprocal exponent has no real answer.
            var result = Math.Pow(value, 1d / _coefficient);
            if (!double.IsNaN(result)) return result;

            return _fallback.Fail(
                converter: this,
                value: value,
                problem: $"the root of {value} with exponent 1/{_coefficient} has no real answer");
        }

        private double ReverseDivide(double value)
        {
            if (value != 0)
                return _coefficient / value;

            return _fallback.Fail(
                converter: this,
                value: value,
                problem: "division of the coefficient by a zero value");
        }

        private double Undeclared(double value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the operation {_operation.Describe()} is not a declared {nameof(NumberOperation)}");

        private double DivideByZero(double value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: "division by zero coefficient");

        private double ZeroCoefficient(double value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the coefficient is zero, which makes {_operation.Describe()} irreversible");
    }
}
