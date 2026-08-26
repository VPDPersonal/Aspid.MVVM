using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures a <see cref="TimeSpan"/> as a number.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To Number",
        Name = "Time Span To Number",
        Tooltip = "Measures a TimeSpan as a number")]
    public sealed class TimeSpanToNumberConverter :
        IConverter<TimeSpan, int>,
        IConverter<TimeSpan, long>,
        IConverter<TimeSpan, float>,
        IConverter<TimeSpan, double>
    {
        [Tooltip("Which unit to measure in.")]
        [SerializeField] private TimeUnit _unit = TimeUnit.TotalSeconds;

        /// <remarks>Default: measuring in seconds.</remarks>
        public TimeSpanToNumberConverter() { }

        /// <param name="unit">Which unit to measure in.</param>
        public TimeSpanToNumberConverter(TimeUnit unit)
        {
            _unit = unit;
        }

        /// <summary>
        /// Measures the specified duration.
        /// </summary>
        /// <param name="value">The duration to measure.</param>
        /// <returns>The measurement; total seconds when the unit is not a declared value.</returns>
        public float Convert(TimeSpan value) => 
            NumericSaturation.ToFloat(Measure(value));

        int IConverter<TimeSpan, int>.Convert(TimeSpan value) =>
            NumericSaturation.ToInt(Measure(value));

        long IConverter<TimeSpan, long>.Convert(TimeSpan value) =>
            NumericSaturation.ToLong(Measure(value));

        double IConverter<TimeSpan, double>.Convert(TimeSpan value) =>
            Measure(value);

        private double Measure(TimeSpan value) => _unit switch
        {
            TimeUnit.Seconds => value.Seconds,
            TimeUnit.TotalSeconds => value.TotalSeconds,
            TimeUnit.TotalMinutes => value.TotalMinutes,
            TimeUnit.TotalHours => value.TotalHours,
            TimeUnit.TotalDays => value.TotalDays,
            _ => Undeclared(value)
        };

        private double Undeclared(TimeSpan value)
        {
            this.LogError(
                problem: $"the unit {_unit.Describe()} is not a declared {nameof(TimeUnit)}",
                consequence: "Measuring in total seconds.");

            return value.TotalSeconds;
        }
    }
}
