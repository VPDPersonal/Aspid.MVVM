using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> to a Unix timestamp.
    /// </summary>
    /// <remarks>
    /// A moment whose <see cref="DateTime.Kind"/> is <see cref="DateTimeKind.Unspecified"/> is read
    /// as local, in both directions.
    /// <para>
    /// A value outside the range the other side holds is clamped to the nearest bound and reported
    /// as an error rather than thrown on. There is no <see cref="float"/> overload: a float holds
    /// seven digits and a timestamp in seconds needs ten.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To Number",
        Name = "Date Time To Unix Timestamp",
        Tooltip = "Converts a DateTime to a Unix timestamp")]
    public sealed class DateTimeToUnixTimestampConverter :
        ITwoWayConverter<DateTime, long>,
        ITwoWayConverter<DateTime, int>,
        ITwoWayConverter<DateTime, double>
    {
        // The epoch counts of DateTime.MinValue and MaxValue — the widest range DateTimeOffset takes.
        private const long MinSeconds = -62135596800L;
        private const long MaxSeconds = 253402300799L;
        private const long MinMilliseconds = MinSeconds * 1000L;
        private const long MaxMilliseconds = MaxSeconds * 1000L + 999L;

        private const long EpochTicks = 621355968000000000L;
        
        [Tooltip("Produce milliseconds rather than seconds. An int holds only 25 days of them.")]
        [SerializeField] private bool _milliseconds;

        [Tooltip("Produce a UTC time rather than a local one.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private bool _utc;

        [Tooltip("Returned when the timestamp is not finite. Return Input has nothing to offer: " +
            "a timestamp is never a DateTime.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<DateTime> _convertBackFallback;
        
        /// <remarks>Default: producing seconds, converting back to local time.</remarks>
        public DateTimeToUnixTimestampConverter() { }

        /// <param name="milliseconds">
        /// Whether to produce milliseconds rather than seconds. An <see cref="int"/> counts only 25
        /// days of them, so leave this off on an <see cref="int"/> timestamp.
        /// </param>
        /// <param name="utc">Whether to produce a UTC time rather than a local one when converting a timestamp back.</param>
        /// <param name="convertBackFallback">
        /// Returned when converting back a timestamp that is not a finite number.
        /// <see cref="ConverterFailureMode.ReturnInput"/> has nothing to offer here: a timestamp is
        /// never a <see cref="DateTime"/>. When omitted, <see cref="DateTime.MinValue"/>.
        /// </param>
        public DateTimeToUnixTimestampConverter(
            bool milliseconds,
            bool utc = false,
            ConverterFallback<DateTime>? convertBackFallback = null)
        {
            _utc = utc;
            _milliseconds = milliseconds;
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
        }

        /// <summary>
        /// Converts the specified date and time to a timestamp.
        /// </summary>
        /// <param name="value">The date and time.</param>
        /// <returns>The timestamp.</returns>
        public long Convert(DateTime value)
        {
            var offset = ToOffset(value);
            return _milliseconds ? offset.ToUnixTimeMilliseconds() : offset.ToUnixTimeSeconds();
        }

        // An int stops counting seconds in 2038, so a later date saturates instead of wrapping.
        int IConverter<DateTime, int>.Convert(DateTime value) => ClampedToInt(Convert(value));

        // Counting ticks keeps the fraction a whole-second timestamp drops.
        double IConverter<DateTime, double>.Convert(DateTime value)
        {
            var ticks = ToOffset(value).UtcTicks - EpochTicks;

            return _milliseconds
                ? ticks / (double)TimeSpan.TicksPerMillisecond
                : ticks / (double)TimeSpan.TicksPerSecond;
        }

        /// <summary>
        /// Converts a timestamp coming back from the View to a date and time.
        /// </summary>
        /// <param name="value">The timestamp.</param>
        /// <returns>
        /// The date and time, UTC or local as configured, or the nearest bound when the timestamp is
        /// outside the range a <see cref="DateTime"/> covers.
        /// </returns>
        public DateTime ConvertBack(long value)
        {
            var timestamp = Clamped(value);

            var offset = _milliseconds
                ? DateTimeOffset.FromUnixTimeMilliseconds(timestamp)
                : DateTimeOffset.FromUnixTimeSeconds(timestamp);

            return _utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        // Every int lands inside the calendar, read as seconds or as milliseconds alike.
        DateTime ITwoWayConverter<DateTime, int>.ConvertBack(int value) => ConvertBack((long)value);

        DateTime ITwoWayConverter<DateTime, double>.ConvertBack(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return _convertBackFallback.Fail(this, value, $"{value.Describe()} is not a finite timestamp");

            var seconds = _milliseconds ? value / 1000d : value;
            var whole = Math.Floor(seconds);
            var fractionTicks = 0L;

            if (whole is < MinSeconds or > MaxSeconds)
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

        private static DateTimeOffset ToOffset(DateTime value) =>
            new DateTimeOffset(value.ToUniversalTime());

        private long Clamped(long value)
        {
            var min = _milliseconds ? MinMilliseconds : MinSeconds;
            var max = _milliseconds ? MaxMilliseconds : MaxSeconds;

            if (value >= min && value <= max) return value;

            // FromUnixTime* throws outside this range, and the timestamp arrives from the View —
            // a broken one should show the wrong date, not stop the binder.
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
