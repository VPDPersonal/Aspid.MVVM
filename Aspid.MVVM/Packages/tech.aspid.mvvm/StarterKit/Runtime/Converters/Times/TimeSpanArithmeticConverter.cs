using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies arithmetic to a duration.
    /// </summary>
    /// <remarks>
    /// The duration a View shows is rarely the one the ViewModel holds: a progress ring wants the
    /// total minus what has elapsed, a "sale ends in" label wants a fixed head start added. This is
    /// <see cref="ArithmeticNumberConverter"/>'s counterpart for <see cref="TimeSpan"/>, and it keeps
    /// the sum in ticks rather than sending it through a float number of seconds and back.
    /// <para>
    /// <see cref="NumberOperation.ReverseSubtract"/> is the one most often wanted: it is the operand
    /// minus the value, which is the total-minus-elapsed case.
    /// </para>
    /// <para>
    /// The operand is a number of seconds for <see cref="NumberOperation.Plus"/>,
    /// <see cref="NumberOperation.Minus"/>, <see cref="NumberOperation.ReverseSubtract"/> and
    /// <see cref="NumberOperation.Modulo"/>, and a plain factor for
    /// <see cref="NumberOperation.Multiply"/> and <see cref="NumberOperation.Division"/> — a duration
    /// multiplied by a duration is not a duration. <see cref="NumberOperation.Power"/> and
    /// <see cref="NumberOperation.ReverseDivide"/> have no meaning for durations at all; they treat
    /// the value as its number of seconds and read the result back as seconds, which is what
    /// <see cref="ArithmeticNumberConverter"/> would have done with the same numbers.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Time", Name = "Time Span Arithmetic", Tooltip = "Applies arithmetic to a duration")]
    public sealed class TimeSpanArithmeticConverter : IConverter<TimeSpan, TimeSpan>
    {
        [Tooltip("The arithmetic applied to the bound duration.")]
        [SerializeField] private NumberOperation _operation;

        [Tooltip("The operand: a number of seconds when adding or subtracting, a plain factor when "
            + "multiplying or dividing. Dividing by zero returns the duration unchanged.")]
        [SerializeField] private float _operandSeconds;

        [NonSerialized] private bool _loggedDivideByZero;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanArithmeticConverter"/> class adding nothing.
        /// </summary>
        public TimeSpanArithmeticConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanArithmeticConverter"/> class.
        /// </summary>
        /// <param name="operation">The arithmetic applied to the bound duration.</param>
        /// <param name="operandSeconds">The operand, in seconds where the operation reads it as a duration.</param>
        public TimeSpanArithmeticConverter(NumberOperation operation, float operandSeconds)
        {
            _operation = operation;
            _operandSeconds = operandSeconds;
        }

        // Every operation is computed in double ticks: adding two long tick counts can pass
        // long.MaxValue and wrap into a negative duration without saying so, and scaling a tick
        // count through a float loses whole seconds — a float carries about seven digits and a day
        // is eleven of them in ticks.
        private double OperandTicks => (double)_operandSeconds * TimeSpan.TicksPerSecond;

        /// <summary>
        /// Applies the configured arithmetic to the specified duration.
        /// </summary>
        /// <param name="value">The duration to transform.</param>
        /// <returns>
        /// The result, saturated at <see cref="TimeSpan.MaxValue"/> or <see cref="TimeSpan.MinValue"/>
        /// where the arithmetic leaves what a duration can hold.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the operation is not a declared value.</exception>
        public TimeSpan Convert(TimeSpan value) => _operation switch
        {
            NumberOperation.Plus => FromTicks(value.Ticks + OperandTicks),
            NumberOperation.Minus => FromTicks(value.Ticks - OperandTicks),
            NumberOperation.Division => Divide(value),
            NumberOperation.Multiply => FromTicks(value.Ticks * (double)_operandSeconds),
            NumberOperation.Modulo => Modulo(value),
            NumberOperation.Power => FromSeconds(Math.Pow(value.TotalSeconds, _operandSeconds)),
            NumberOperation.ReverseSubtract => FromTicks(OperandTicks - value.Ticks),
            NumberOperation.ReverseDivide => ReverseDivide(value),
            _ => throw new ArgumentOutOfRangeException(nameof(_operation), _operation, null)
        };

        private TimeSpan Divide(TimeSpan value)
        {
            if (_operandSeconds == 0f)
            {
                LogDivideByZero();
                return value;
            }

            return FromTicks(value.Ticks / (double)_operandSeconds);
        }

        // C#'s % keeps the sign of the left operand, so a duration one tick short of the period comes
        // back negative — never what "where in the cycle is this?" wants.
        private TimeSpan Modulo(TimeSpan value)
        {
            var operand = OperandTicks;

            if (operand == 0d)
            {
                LogDivideByZero();
                return value;
            }

            var remainder = value.Ticks % operand;
            return FromTicks(remainder < 0d ? remainder + Math.Abs(operand) : remainder);
        }

        private TimeSpan ReverseDivide(TimeSpan value)
        {
            var seconds = value.TotalSeconds;
            if (seconds == 0d) return value;

            return FromSeconds(_operandSeconds / seconds);
        }

        // The ends are saturated rather than allowed to wrap: a duration that overflows is wrong
        // either way, and a negative one is wrong in a way that looks plausible on a label.
        private static TimeSpan FromTicks(double ticks)
        {
            if (double.IsNaN(ticks)) return TimeSpan.Zero;
            if (ticks >= long.MaxValue) return TimeSpan.MaxValue;
            if (ticks <= long.MinValue) return TimeSpan.MinValue;

            return TimeSpan.FromTicks((long)ticks);
        }

        private static TimeSpan FromSeconds(double seconds) => FromTicks(seconds * TimeSpan.TicksPerSecond);

        private void LogDivideByZero()
        {
            if (_loggedDivideByZero) return;
            _loggedDivideByZero = true;

            Debug.LogError(
                $"{nameof(TimeSpanArithmeticConverter)}: division by a zero operand. "
                + "Returning the duration unchanged.");
        }
    }
}
