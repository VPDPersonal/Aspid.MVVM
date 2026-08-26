using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number of seconds to a <see cref="TimeSpan"/>.
    /// </summary>
    /// <remarks>
    /// A value <see cref="TimeSpan"/> cannot hold — NaN, an infinity, or seconds past its range — is
    /// reported as an error rather than thrown on. The int and long overloads drop the fraction of a
    /// second on the way back.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Time",
        Name = "Seconds To Time Span",
        Tooltip = "Converts a number of seconds to a TimeSpan")]
    public sealed class SecondsToTimeSpanConverter :
        ITwoWayConverter<float, TimeSpan>,
        ITwoWayConverter<double, TimeSpan>,
        ITwoWayConverter<int, TimeSpan>,
        ITwoWayConverter<long, TimeSpan>
    {
        // FromSeconds rounds the count before checking its range, so a value a fraction of a
        // millisecond inside TimeSpan.MaxValue still overflows; the guard mirrors that rounding.
        private const long MinMilliseconds = long.MinValue / TimeSpan.TicksPerMillisecond;
        private const long MaxMilliseconds = long.MaxValue / TimeSpan.TicksPerMillisecond;

        /// <summary>
        /// Converts the specified seconds to a duration.
        /// </summary>
        /// <param name="value">The number of seconds.</param>
        /// <returns>
        /// The duration; <see cref="TimeSpan.Zero"/> for a non-finite value, the nearest bound for
        /// one out of range.
        /// </returns>
        public TimeSpan Convert(float value) => ToDuration(value);

        /// <inheritdoc cref="Convert(float)"/>
        public TimeSpan Convert(double value) => ToDuration(value);

        /// <summary>
        /// Converts the specified seconds to a duration.
        /// </summary>
        /// <param name="value">The number of seconds.</param>
        /// <returns>The duration. No <see cref="int"/> count of seconds is out of range.</returns>
        public TimeSpan Convert(int value) => ToDuration(value);

        /// <summary>
        /// Converts the specified seconds to a duration.
        /// </summary>
        /// <param name="value">The number of seconds.</param>
        /// <returns>The duration, or the nearest bound for a count out of range.</returns>
        public TimeSpan Convert(long value) => ToDuration(value);

        /// <summary>
        /// Converts a duration back to seconds.
        /// </summary>
        /// <param name="value">The duration.</param>
        /// <returns>The number of seconds.</returns>
        public float ConvertBack(TimeSpan value) => (float)value.TotalSeconds;

        double ITwoWayConverter<double, TimeSpan>.ConvertBack(TimeSpan value) =>
            value.TotalSeconds;

        // A TimeSpan measures more seconds than an int counts, so it saturates instead of wrapping.
        int ITwoWayConverter<int, TimeSpan>.ConvertBack(TimeSpan value) =>
            NumericSaturation.ToInt(value.TotalSeconds);

        long ITwoWayConverter<long, TimeSpan>.ConvertBack(TimeSpan value) =>
            NumericSaturation.ToLong(value.TotalSeconds);

        private TimeSpan ToDuration(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                this.LogError(
                    problem: $"{value.Describe()} is not a finite number of seconds",
                    consequence: "Using TimeSpan.Zero.");

                return TimeSpan.Zero;
            }

            // FromSeconds throws past the TimeSpan range, and the count comes from the ViewModel.
            var milliseconds = value * 1000d + (value < 0d ? -0.5d : 0.5d);

            if (milliseconds < MinMilliseconds) return Clamped(value, TimeSpan.MinValue);
            if (milliseconds > MaxMilliseconds) return Clamped(value, TimeSpan.MaxValue);

            return TimeSpan.FromSeconds(value);
        }

        private TimeSpan Clamped(double value, TimeSpan bound)
        {
            this.LogError(
                problem: $"{value.Describe()} seconds is past what a TimeSpan holds",
                consequence: $"Clamping to {bound}.");

            return bound;
        }
    }
}
