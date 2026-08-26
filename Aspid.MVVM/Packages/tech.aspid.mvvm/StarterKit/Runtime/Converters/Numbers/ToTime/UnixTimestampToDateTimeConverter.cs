using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a Unix timestamp to a <see cref="DateTime"/>.
    /// </summary>
    /// <remarks>
    /// A moment whose <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/> is read
    /// as local, in both directions.
    /// <para>
    /// A value outside the range the other side holds is clamped to the nearest bound and reported
    /// as an error rather than thrown on. The <see cref="double"/> overload carries a fraction of a
    /// second; the <see cref="int"/> one counts only seconds.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Time",
        Name = "Unix Timestamp To Date Time",
        Tooltip = "Converts a Unix timestamp to a DateTime")]
    public sealed class UnixTimestampToDateTimeConverter :
        ITwoWayConverter<long, DateTime>,
        ITwoWayConverter<int, DateTime>,
        ITwoWayConverter<double, DateTime>
    {
        [Tooltip("The timestamp is in milliseconds rather than seconds. An int holds only 25 days of them.")]
        [SerializeField] private bool _milliseconds;

        [Tooltip("Produce a UTC time rather than a local one.")]
        [SerializeField] private bool _utc;

        // The epoch counts of DateTime.MinValue and MaxValue — the widest range DateTimeOffset takes.
        private const long MinSeconds = -62135596800L;
        private const long MaxSeconds = 253402300799L;
        private const long MinMilliseconds = MinSeconds * 1000L;
        private const long MaxMilliseconds = MaxSeconds * 1000L + 999L;

        private const long EpochTicks = 621355968000000000L;

        /// <remarks>Default: reading local seconds.</remarks>
        public UnixTimestampToDateTimeConverter() { }

        /// <param name="milliseconds">
        /// Whether the timestamp is in milliseconds. An <see cref="int"/> counts only 25 days of
        /// them, so leave this off on an <see cref="int"/> timestamp.
        /// </param>
        /// <param name="utc">Whether to produce a UTC time.</param>
        public UnixTimestampToDateTimeConverter(bool milliseconds, bool utc = false)
        {
            _milliseconds = milliseconds;
            _utc = utc;
        }

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">The timestamp.</param>
        /// <returns>The date and time, or the nearest bound when the timestamp is out of range.</returns>
        public DateTime Convert(long value)
        {
            var timestamp = Clamped(value);

            var offset = _milliseconds
                ? DateTimeOffset.FromUnixTimeMilliseconds(timestamp)
                : DateTimeOffset.FromUnixTimeSeconds(timestamp);

            return _utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">
        /// The timestamp. Every <see cref="int"/> lands inside the calendar, read as seconds or as
        /// milliseconds alike.
        /// </param>
        /// <returns>The date and time.</returns>
        public DateTime Convert(int value)
        {
            // An int counts 25 days of milliseconds, so the whole type maps into January 1970.
            if (_milliseconds)
            {
                this.LogError(
                    problem: "an int cannot hold a millisecond timestamp — it counts 25 days of them",
                    consequence: "Reading it as milliseconds anyway.");
            }

            return Convert((long)value);
        }

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">The timestamp, carrying a fraction of a second.</param>
        /// <returns>
        /// The date and time; the Unix epoch for a non-finite value, the nearest bound for one out
        /// of range.
        /// </returns>
        public DateTime Convert(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                this.LogError(
                    problem: $"{value.Describe()} is not a finite timestamp",
                    consequence: "Using the Unix epoch.");

                value = 0d;
            }

            var seconds = _milliseconds ? value / 1000d : value;
            var whole = Math.Floor(seconds);
            var fractionTicks = 0L;

            if (whole < MinSeconds || whole > MaxSeconds)
            {
                var clamped = Math.Clamp(whole, MinSeconds, MaxSeconds);

                this.LogError(
                    problem: $"the timestamp {value.Describe()} is outside the range a DateTime covers",
                    consequence: $"Clamping to {(_milliseconds ? clamped * 1000d : clamped)}.");

                whole = clamped;
            }
            else
            {
                // Truncating rather than rounding keeps the top of the range in reach: the most the
                // fraction can add is one tick short of a second, which lands on DateTime.MaxValue.
                fractionTicks = (long)((seconds - whole) * TimeSpan.TicksPerSecond);
            }

            var offset = DateTimeOffset.FromUnixTimeSeconds((long)whole).AddTicks(fractionTicks);
            return _utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        /// <summary>
        /// Converts a date and time back to a timestamp.
        /// </summary>
        /// <param name="value">
        /// The date and time. A <see cref="DateTimeKind.Unspecified"/> one is read as local.
        /// </param>
        /// <returns>The timestamp.</returns>
        public long ConvertBack(DateTime value)
        {
            var offset = ToOffset(value);
            return _milliseconds ? offset.ToUnixTimeMilliseconds() : offset.ToUnixTimeSeconds();
        }

        // An int stops counting seconds in 2038, so a later date saturates instead of wrapping.
        int ITwoWayConverter<int, DateTime>.ConvertBack(DateTime value) =>
            ClampedToInt(ConvertBack(value));

        double ITwoWayConverter<double, DateTime>.ConvertBack(DateTime value)
        {
            var ticks = ToOffset(value).UtcTicks - EpochTicks;

            return _milliseconds
                ? ticks / (double)TimeSpan.TicksPerMillisecond
                : ticks / (double)TimeSpan.TicksPerSecond;
        }

        private static DateTimeOffset ToOffset(DateTime value) =>
            new DateTimeOffset(value.ToUniversalTime());

        private long Clamped(long value)
        {
            var min = _milliseconds ? MinMilliseconds : MinSeconds;
            var max = _milliseconds ? MaxMilliseconds : MaxSeconds;

            if (value >= min && value <= max) return value;

            // FromUnixTime* throws outside this range, and the timestamp arrives from the ViewModel.
            var clamped = Math.Clamp(value, min, max);

            this.LogError(
                problem: $"the timestamp {value.Describe()} is outside the range a DateTime covers",
                consequence: $"Clamping to {clamped}.");

            return clamped;
        }

        private int ClampedToInt(long value)
        {
            var clamped = NumericSaturation.ToInt(value);
            if (clamped == value) return clamped;

            this.LogError(
                problem: $"the timestamp {value.Describe()} is outside the range an int covers",
                consequence: $"Clamping to {clamped}.");

            return clamped;
        }
    }
}
