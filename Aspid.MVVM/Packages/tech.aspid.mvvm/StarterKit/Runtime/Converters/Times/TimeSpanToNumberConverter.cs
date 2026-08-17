using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures a <see cref="TimeSpan"/> as a number.
    /// </summary>
    /// <remarks>For feeding a duration into a slider or a fill amount.</remarks>
    [Serializable]
    public sealed class TimeSpanToNumberConverter : IConverter<TimeSpan, float>
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
        /// <returns>The measurement.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the unit is not a declared value.</exception>
        public float Convert(TimeSpan value) => _unit switch
        {
            TimeUnit.Seconds => value.Seconds,
            TimeUnit.TotalSeconds => (float)value.TotalSeconds,
            TimeUnit.TotalMinutes => (float)value.TotalMinutes,
            TimeUnit.TotalHours => (float)value.TotalHours,
            TimeUnit.TotalDays => (float)value.TotalDays,
            _ => throw new ArgumentOutOfRangeException(nameof(_unit), _unit, null)
        };
    }
}
