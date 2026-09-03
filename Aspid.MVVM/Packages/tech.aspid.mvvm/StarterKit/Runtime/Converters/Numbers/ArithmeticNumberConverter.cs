#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies an arithmetic operation with an authored coefficient.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Arithmetic",
        Tooltip = "Applies an arithmetic operation with an authored coefficient")]
    public sealed class ArithmeticNumberConverter : TwoWayNumberConverter
    {
        [Tooltip("The number the operation is applied with.")]
        [SerializeField] private double _coefficient = 1d;

        [Tooltip("The arithmetic applied to the number.")]
        [SerializeField] private NumberOperation _operation;

        [Tooltip("Returned when the arithmetic cannot be done.")]
        [SerializeField] private ConverterFallback<double> _fallback = new(0d, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: adding a coefficient of one.</remarks>
        public ArithmeticNumberConverter() { }

        /// <param name="operation">The arithmetic applied to the number.</param>
        /// <param name="coefficient">The number the operation is applied with. Dividing by zero falls back.</param>
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

        /// <summary>
        /// Applies the authored arithmetic.
        /// </summary>
        /// <param name="value">The number to transform.</param>
        /// <returns>The result, or the fallback for an undeclared operation or a division by zero.</returns>
        protected override double Apply(double value) => _operation switch
        {
            NumberOperation.Add => value + _coefficient,
            NumberOperation.Subtract => value - _coefficient,
            NumberOperation.Divide => Divide(value),
            NumberOperation.Multiply => value * _coefficient,
            NumberOperation.Modulo => Modulo(value),
            NumberOperation.Power => Math.Pow(value, _coefficient),
            NumberOperation.ReverseSubtract => _coefficient - value,
            NumberOperation.ReverseDivide => ReverseDivide(value),
            _ => Undeclared(value)
        };

        /// <summary>
        /// Reverses the authored arithmetic.
        /// </summary>
        /// <param name="value">The number to transform back.</param>
        /// <returns>
        /// The number the forward pass was given, or the fallback where the operation cannot be undone.
        /// <see cref="NumberOperation.Modulo"/> returns the value unchanged without reporting.
        /// </returns>
        protected override double Undo(double value) => _operation switch
        {
            NumberOperation.Add => value - _coefficient,
            NumberOperation.Subtract => value + _coefficient,
            NumberOperation.Divide => UndoDivide(value),
            NumberOperation.Multiply => UndoMultiply(value),
            NumberOperation.Power => UndoPower(value),
            NumberOperation.ReverseSubtract => _coefficient - value,
            NumberOperation.ReverseDivide => ReverseDivide(value),
            NumberOperation.Modulo => value,
            _ => Undeclared(value)
        };

        private double Divide(double value) => _coefficient is not 0d
            ? value / _coefficient
            : DivideByZero(value);

        private double Modulo(double value)
        {
            if (_coefficient is 0d) return DivideByZero(value);

            var remainder = value % _coefficient;
            return remainder < 0 ? remainder + Math.Abs(_coefficient) : remainder;
        }

        private double UndoMultiply(double value) => _coefficient is not 0d
            ? value / _coefficient
            : ZeroCoefficient(value);

        private double UndoDivide(double value) => _coefficient is not 0d
            ? value * _coefficient
            : ZeroCoefficient(value);

        private double UndoPower(double value)
        {
            if (_coefficient is 0d) return ZeroCoefficient(value);
            if (double.IsNaN(value)) return value;

            var result = Math.Pow(value, 1d / _coefficient);
            if (!double.IsNaN(result)) return result;

            return _fallback.Fail(
                converter: this,
                value: value,
                problem: $"the root of {value} with exponent 1/{_coefficient} has no real answer");
        }

        private double ReverseDivide(double value)
        {
            if (value is not 0d) return _coefficient / value;

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
