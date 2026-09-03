#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies arithmetic to a duration.
    /// </summary>
    /// <remarks>
    /// The operand is seconds, except a factor for <see cref="NumberOperation.Multiply"/> and <see cref="NumberOperation.Divide"/>
    /// and an exponent for <see cref="NumberOperation.Power"/>. Power and ReverseDivide treat the duration as seconds.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time",
        Name = "Time Span Arithmetic",
        Tooltip = "Applies arithmetic to a duration")]
    public sealed class TimeSpanArithmeticConverter : IConverter<TimeSpan, TimeSpan>
    {
        [Tooltip("The arithmetic applied to the duration.")]
        [SerializeField] private NumberOperation _operation;

        [Tooltip("Seconds, except a factor for Multiply and Divide and an exponent for Power.")]
        [SerializeField] private float _operand;

        [Tooltip("Returned when the arithmetic cannot be done.")]
        [SerializeField] private ConverterFallback<TimeSpan> _fallback = new(TimeSpan.Zero, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: adding zero seconds, which leaves the duration unchanged.</remarks>
        public TimeSpanArithmeticConverter() { }

        /// <param name="operation">The arithmetic applied to the duration.</param>
        /// <param name="operand">Seconds, except a factor for Multiply and Divide and an exponent for Power.</param>
        /// <param name="fallback">
        /// Returned when the operand is not finite, the operation is undeclared, divides by zero or has no real result.
        /// When omitted, returns the duration unchanged.
        /// </param>
        public TimeSpanArithmeticConverter(
            NumberOperation operation,
            float operand,
            ConverterFallback<TimeSpan>? fallback = null)
        {
            _operand = operand;
            _operation = operation;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Applies the configured arithmetic to the specified duration.
        /// </summary>
        /// <param name="value">The duration to transform.</param>
        /// <returns>The result, saturated on overflow, or the fallback when the arithmetic cannot be done.</returns>
        public TimeSpan Convert(TimeSpan value)
        {
            if (float.IsNaN(_operand) || float.IsInfinity(_operand)) return NotFinite(value);

            return _operation switch
            {
                NumberOperation.Add => FromTicks(value.Ticks + OperandTicks),
                NumberOperation.Subtract => FromTicks(value.Ticks - OperandTicks),
                NumberOperation.Divide => Divide(value),
                NumberOperation.Multiply => FromTicks(value.Ticks * (double)_operand),
                NumberOperation.Modulo => Modulo(value),
                NumberOperation.Power => Power(value),
                NumberOperation.ReverseSubtract => FromTicks(OperandTicks - value.Ticks),
                NumberOperation.ReverseDivide => ReverseDivide(value),
                _ => Undeclared(value)
            };
        }

        private double OperandTicks => (double)_operand * TimeSpan.TicksPerSecond;

        private TimeSpan NotFinite(TimeSpan value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the operand is {_operand.Describe()}, which is not a number");

        private TimeSpan Undeclared(TimeSpan value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the operation {_operation.Describe()} is not a declared {nameof(NumberOperation)}");

        private TimeSpan Divide(TimeSpan value) => _operand is 0f
            ? DivideByZero(value)
            : FromTicks(value.Ticks / (double)_operand);

        private TimeSpan Modulo(TimeSpan value)
        {
            var operand = OperandTicks;
            if (operand is 0d) return DivideByZero(value);

            var remainder = value.Ticks % operand;
            return FromTicks(remainder < 0d ? remainder + Math.Abs(operand) : remainder);
        }

        private TimeSpan Power(TimeSpan value)
        {
            var seconds = Math.Pow(value.TotalSeconds, _operand);

            if (double.IsNaN(seconds))
            {
                return _fallback.Fail(
                    converter: this,
                    value: value,
                    problem: $"raising {value.TotalSeconds} seconds to the power of {_operand} has no real result");
            }

            return FromSeconds(seconds);
        }

        private TimeSpan ReverseDivide(TimeSpan value)
        {
            if (value.Ticks is 0L)
            {
                return _fallback.Fail(
                    converter: this,
                    value: value,
                    problem: "division by a zero duration");
            }

            return FromSeconds(_operand / value.TotalSeconds);
        }

        private static TimeSpan FromTicks(double ticks)
        {
            if (ticks >= long.MaxValue) return TimeSpan.MaxValue;
            if (ticks <= long.MinValue) return TimeSpan.MinValue;

            return TimeSpan.FromTicks((long)ticks);
        }

        private static TimeSpan FromSeconds(double seconds) =>
            FromTicks(seconds * TimeSpan.TicksPerSecond);

        private TimeSpan DivideByZero(TimeSpan value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: "division by a zero operand");
    }
}
