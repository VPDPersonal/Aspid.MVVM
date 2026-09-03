#nullable enable
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
    /// An <see cref="DateTimeKind.Unspecified"/> moment is read as local. A value out of range is clamped and reported, not thrown on.
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
        [Tooltip("The timestamp is in milliseconds. An int holds only 25 days of them.")]
        [SerializeField] private bool _milliseconds;

        [Tooltip("Produce a UTC time rather than a local one.")]
        [SerializeField] private bool _utc;

        /// <remarks>Default: reading local seconds.</remarks>
        public UnixTimestampToDateTimeConverter() { }

        /// <param name="milliseconds">Whether the timestamp is in milliseconds. An <see cref="int"/> holds only 25 days of them.</param>
        /// <param name="utc">Whether to produce a UTC time.</param>
        public UnixTimestampToDateTimeConverter(
            bool milliseconds,
            bool utc = false)
        {
            _utc = utc;
            _milliseconds = milliseconds;
        }

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">The timestamp.</param>
        /// <returns>The date and time, or the nearest bound when the timestamp is out of range.</returns>
        public DateTime Convert(long value) =>
            UnixTime.ToDateTime(this, value, _milliseconds, _utc);

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">The timestamp.</param>
        /// <returns>The date and time. A millisecond timestamp in an <see cref="int"/> is reported.</returns>
        public DateTime Convert(int value)
        {
            if (_milliseconds)
            {
                this.LogError(
                    problem: "an int cannot hold a millisecond timestamp, it counts 25 days of them",
                    consequence: "Reading it as milliseconds anyway.");
            }

            return Convert((long)value);
        }

        /// <summary>
        /// Converts the specified timestamp to a date and time.
        /// </summary>
        /// <param name="value">The timestamp, carrying a fraction of a second.</param>
        /// <returns>The date and time; the Unix epoch for a non-finite value, the nearest bound for one out of range.</returns>
        public DateTime Convert(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                this.LogError(
                    problem: $"{value.Describe()} is not a finite timestamp",
                    consequence: "Using the Unix epoch.");

                value = 0d;
            }

            return UnixTime.ToDateTime(this, value, _milliseconds, _utc);
        }

        /// <summary>
        /// Converts a date and time back to a timestamp.
        /// </summary>
        /// <param name="value">The date and time. An <see cref="DateTimeKind.Unspecified"/> one is read as local.</param>
        /// <returns>The timestamp.</returns>
        public long ConvertBack(DateTime value) =>
            UnixTime.ToTimestamp(value, _milliseconds);

        int ITwoWayConverter<int, DateTime>.ConvertBack(DateTime value) =>
            UnixTime.ToInt(this, ConvertBack(value));

        double ITwoWayConverter<double, DateTime>.ConvertBack(DateTime value) =>
            UnixTime.ToFractionalTimestamp(value, _milliseconds);
    }
}
