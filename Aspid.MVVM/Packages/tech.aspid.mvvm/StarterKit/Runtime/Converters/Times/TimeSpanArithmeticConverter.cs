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
    /// The operand is a number of seconds for <see cref="NumberOperation.Plus"/>,
    /// <see cref="NumberOperation.Minus"/>, <see cref="NumberOperation.ReverseSubtract"/>,
    /// <see cref="NumberOperation.Modulo"/> and <see cref="NumberOperation.ReverseDivide"/>, a plain
    /// factor for <see cref="NumberOperation.Multiply"/> and <see cref="NumberOperation.Division"/>,
    /// and an exponent for <see cref="NumberOperation.Power"/>.
    /// <see cref="NumberOperation.Power"/> and <see cref="NumberOperation.ReverseDivide"/> have no
    /// meaning for a duration: they treat the value as seconds and read the result back as seconds.
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

        [Tooltip("Seconds, except a plain factor for Multiply and Division and an exponent for Power.")]
        [SerializeField] private float _operandSeconds;

        [Tooltip("Returned when the arithmetic cannot be done.")]
        [SerializeField] private ConverterFallback<TimeSpan> _fallback = new(TimeSpan.Zero, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: adding zero seconds, which leaves the duration unchanged.</remarks>
        public TimeSpanArithmeticConverter() { }

        /// <param name="operation">The arithmetic applied to the bound duration.</param>
        /// <param name="operandSeconds">
        /// The operand: seconds for Plus, Minus, Reverse Subtract, Modulo and Reverse Divide; a plain
        /// factor for Multiply and Division; an exponent for Power. A value that is not finite, or a
        /// division by zero, falls back.
        /// </param>
        /// <param name="fallback">
        /// Returned when the operand is not finite, the operation is undeclared, it divides by zero,
        /// or it has no real result. When omitted, returns the duration unchanged.
        /// </param>
        public TimeSpanArithmeticConverter(
            NumberOperation operation,
            float operandSeconds,
            ConverterFallback<TimeSpan>? fallback = null)
        {
            _operation = operation;
            _operandSeconds = operandSeconds;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Applies the configured arithmetic to the specified duration.
        /// </summary>
        /// <param name="value">The duration to transform.</param>
        /// <returns>
        /// The result, saturated at <see cref="TimeSpan.MaxValue"/> or <see cref="TimeSpan.MinValue"/>
        /// on overflow; the fallback when the operand is not finite, on division by zero, on a power
        /// with no real result, or when the operation is not a declared value.
        /// </returns>
        public TimeSpan Convert(TimeSpan value)
        {
            // A NaN or infinite operand would cast to an arbitrary duration rather than to an error.
            if (float.IsNaN(_operandSeconds) || float.IsInfinity(_operandSeconds)) return NotFinite(value);

            return _operation switch
            {
                NumberOperation.Plus => FromTicks(value.Ticks + OperandTicks),
                NumberOperation.Minus => FromTicks(value.Ticks - OperandTicks),
                NumberOperation.Division => Divide(value),
                NumberOperation.Multiply => FromTicks(value.Ticks * (double)_operandSeconds),
                NumberOperation.Modulo => Modulo(value),
                NumberOperation.Power => Power(value),
                NumberOperation.ReverseSubtract => FromTicks(OperandTicks - value.Ticks),
                NumberOperation.ReverseDivide => ReverseDivide(value),
                _ => Undeclared(value)
            };
        }

        // Ticks in double: a long sum can pass long.MaxValue and wrap into a negative duration, and
        // a float carries seven digits where a day in ticks needs eleven.
        private double OperandTicks => (double)_operandSeconds * TimeSpan.TicksPerSecond;

        private TimeSpan NotFinite(TimeSpan value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the operand is {_operandSeconds.Describe()}, which is not a number of seconds");

        private TimeSpan Undeclared(TimeSpan value) => _fallback.Fail(
            converter: this,
            value: value,
            problem: $"the operation {_operation.Describe()} is not a declared {nameof(NumberOperation)}");

        private TimeSpan Divide(TimeSpan value) => _operandSeconds is 0f 
            ? DivideByZero(value)
            : FromTicks(value.Ticks / (double)_operandSeconds);

        // C#'s % keeps the sign of the left operand, so a negative duration lands outside the cycle.
        private TimeSpan Modulo(TimeSpan value)
        {
            var operand = OperandTicks;
            if (operand == 0d) return DivideByZero(value);

            var remainder = value.Ticks % operand;
            return FromTicks(remainder < 0d ? remainder + Math.Abs(operand) : remainder);
        }

        private TimeSpan Power(TimeSpan value)
        {
            var seconds = Math.Pow(value.TotalSeconds, _operandSeconds);

            if (double.IsNaN(seconds))
            {
                return _fallback.Fail(
                    converter: this,
                    value: value,
                    problem: $"raising {value.TotalSeconds} seconds to the power of {_operandSeconds} has no real result");
            }

            return FromSeconds(seconds);
        }

        private TimeSpan ReverseDivide(TimeSpan value)
        {
            if (value.Ticks == 0L)
            {
                return _fallback.Fail(
                    converter: this,
                    value: value,
                    problem: "division by a zero duration");
            }

            return FromSeconds(_operandSeconds / value.TotalSeconds);
        }

        // Saturated rather than allowed to wrap: an overflowing duration is wrong either way, but a
        // negative one looks plausible on a label. A NaN never reaches here — the operand is checked.
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
