using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a Unix timestamp to a <see cref="DateTime"/>.
    /// </summary>
    /// <remarks>A backend that speaks epoch seconds, which most do.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Time", Name = "Unix Timestamp To Date Time", Tooltip = "Converts a Unix timestamp to a DateTime")]
    public sealed class UnixTimestampToDateTimeConverter : ITwoWayConverter<long, DateTime>
    {
        [Tooltip("The timestamp is in milliseconds rather than seconds.")]
        [SerializeField] private bool _milliseconds;

        [Tooltip("Produce a UTC time rather than a local one.")]
        [SerializeField] private bool _utc;

        /// <remarks>Default: reading local seconds.</remarks>
        public UnixTimestampToDateTimeConverter() { }

        /// <param name="milliseconds">Whether the timestamp is in milliseconds.</param>
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
        /// <returns>The date and time.</returns>
        public DateTime Convert(long value)
        {
            var offset = _milliseconds
                ? DateTimeOffset.FromUnixTimeMilliseconds(value)
                : DateTimeOffset.FromUnixTimeSeconds(value);

            return _utc ? offset.UtcDateTime : offset.LocalDateTime;
        }

        /// <summary>
        /// Converts a date and time back to a timestamp.
        /// </summary>
        /// <param name="value">The date and time.</param>
        /// <returns>The timestamp.</returns>
        public long ConvertBack(DateTime value)
        {
            var offset = new DateTimeOffset(value.ToUniversalTime());
            return _milliseconds ? offset.ToUnixTimeMilliseconds() : offset.ToUnixTimeSeconds();
        }
    }
}
