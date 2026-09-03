#nullable enable
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
    /// An <see cref="DateTimeKind.Unspecified"/> moment is read as local. A value out of range is clamped and reported, not thrown on.
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
        [Tooltip("Produce milliseconds rather than seconds. An int holds only 25 days of them.")]
        [SerializeField] private bool _milliseconds;

        [Tooltip("Convert a timestamp back to a UTC time rather than a local one.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private bool _utc;

        [Tooltip("Returned when the timestamp is not finite.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<DateTime> _convertBackFallback;

        /// <remarks>Default: producing seconds, converting back to local time.</remarks>
        public DateTimeToUnixTimestampConverter() { }

        /// <param name="milliseconds">Whether to produce milliseconds rather than seconds. An <see cref="int"/> holds only 25 days of them.</param>
        /// <param name="utc">Whether to convert a timestamp back to a UTC time rather than a local one.</param>
        /// <param name="convertBackFallback">Returned when the timestamp is not finite. When omitted, <see cref="DateTime.MinValue"/>.</param>
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
        public long Convert(DateTime value) =>
            UnixTime.ToTimestamp(value, _milliseconds);

        int IConverter<DateTime, int>.Convert(DateTime value) =>
            UnixTime.ToInt(this, Convert(value));

        double IConverter<DateTime, double>.Convert(DateTime value) =>
            UnixTime.ToFractionalTimestamp(value, _milliseconds);

        /// <summary>
        /// Converts a timestamp coming back from the View to a date and time.
        /// </summary>
        /// <param name="value">The timestamp.</param>
        /// <returns>The date and time, or the nearest bound when the timestamp is out of range.</returns>
        public DateTime ConvertBack(long value) =>
            UnixTime.ToDateTime(this, value, _milliseconds, _utc);

        DateTime ITwoWayConverter<DateTime, int>.ConvertBack(int value) =>
            ConvertBack(value);

        DateTime ITwoWayConverter<DateTime, double>.ConvertBack(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                return _convertBackFallback.Fail(
                    converter: this,
                    value: value,
                    problem: $"{value.Describe()} is not a finite timestamp");
            }

            return UnixTime.ToDateTime(this, value, _milliseconds, _utc);
        }
    }
}
