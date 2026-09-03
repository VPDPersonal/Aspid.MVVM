#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between Unix timestamps and <see cref="DateTime"/>, clamping and reporting instead of throwing.
    /// </summary>
    /// <remarks>An <see cref="DateTimeKind.Unspecified"/> moment is read as local.</remarks>
    internal static class UnixTime
    {
        private const long MinSeconds = -62135596800L;
        private const long MaxSeconds = 253402300799L;
        private const long MinMilliseconds = MinSeconds * 1000L;
        private const long MaxMilliseconds = MaxSeconds * 1000L + 999L;

        private const long EpochTicks = 621355968000000000L;

        /// <summary>
        /// Writes a moment as a whole timestamp.
        /// </summary>
        /// <param name="value">The moment.</param>
        /// <param name="milliseconds">Whether the timestamp is in milliseconds rather than seconds.</param>
        /// <returns>The timestamp.</returns>
        internal static long ToTimestamp(DateTime value, bool milliseconds)
        {
            var offset = ToOffset(value);
            return milliseconds ? offset.ToUnixTimeMilliseconds() : offset.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Writes a moment as a timestamp carrying the fraction of a second.
        /// </summary>
        /// <param name="value">The moment.</param>
        /// <param name="milliseconds">Whether the timestamp is in milliseconds rather than seconds.</param>
        /// <returns>The timestamp.</returns>
        internal static double ToFractionalTimestamp(DateTime value, bool milliseconds)
        {
            var ticks = ToOffset(value).UtcTicks - EpochTicks;

            return milliseconds
                ? ticks / (double)TimeSpan.TicksPerMillisecond
                : ticks / (double)TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// Reads a whole timestamp as a moment.
        /// </summary>
        /// <param name="converter">The reporting converter.</param>
        /// <param name="value">The timestamp.</param>
        /// <param name="milliseconds">Whether the timestamp is in milliseconds rather than seconds.</param>
        /// <param name="utc">Whether to produce a UTC moment rather than a local one.</param>
        /// <returns>The moment, or the nearest bound for a timestamp outside the range a <see cref="DateTime"/> covers.</returns>
        internal static DateTime ToDateTime(IConverter converter, long value, bool milliseconds, bool utc)
        {
            var min = milliseconds ? MinMilliseconds : MinSeconds;
            var max = milliseconds ? MaxMilliseconds : MaxSeconds;
            var timestamp = value;

            if (value < min || value > max)
            {
                timestamp = Math.Clamp(value, min, max);

                converter.LogError(
                    problem: $"the timestamp {value.Describe()} is outside the range a DateTime covers",
                    consequence: $"Clamping to {timestamp}.");
            }

            var offset = milliseconds
                ? DateTimeOffset.FromUnixTimeMilliseconds(timestamp)
                : DateTimeOffset.FromUnixTimeSeconds(timestamp);

            return utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        /// <summary>
        /// Reads a timestamp carrying a fraction of a second as a moment.
        /// </summary>
        /// <param name="converter">The reporting converter.</param>
        /// <param name="value">The finite timestamp.</param>
        /// <param name="milliseconds">Whether the timestamp is in milliseconds rather than seconds.</param>
        /// <param name="utc">Whether to produce a UTC moment rather than a local one.</param>
        /// <returns>The moment, or the nearest bound for a timestamp outside the range a <see cref="DateTime"/> covers.</returns>
        internal static DateTime ToDateTime(IConverter converter, double value, bool milliseconds, bool utc)
        {
            var seconds = milliseconds ? value / 1000d : value;
            var whole = Math.Floor(seconds);
            var fractionTicks = 0L;

            if (whole is < MinSeconds or > MaxSeconds)
            {
                var clamped = Math.Clamp(whole, MinSeconds, MaxSeconds);

                converter.LogError(
                    problem: $"the timestamp {value.Describe()} is outside the range a DateTime covers",
                    consequence: $"Clamping to {(milliseconds ? clamped * 1000d : clamped)}.");

                whole = clamped;
            }
            else
            {
                fractionTicks = (long)((seconds - whole) * TimeSpan.TicksPerSecond);
            }

            var offset = DateTimeOffset.FromUnixTimeSeconds((long)whole).AddTicks(fractionTicks);
            return utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        /// <summary>
        /// Narrows a timestamp to an <see cref="int"/>, reporting and saturating past 2038.
        /// </summary>
        /// <param name="converter">The reporting converter.</param>
        /// <param name="value">The timestamp.</param>
        /// <returns>The timestamp, or the nearest <see cref="int"/> bound.</returns>
        internal static int ToInt(IConverter converter, long value)
        {
            var clamped = NumericSaturation.ToInt(value);
            if (clamped == value) return clamped;

            converter.LogError(
                problem: $"the timestamp {value.Describe()} is outside the range an int covers",
                consequence: $"Clamping to {clamped}.");

            return clamped;
        }

        private static DateTimeOffset ToOffset(DateTime value) => new(value.ToUniversalTime());
    }
}
