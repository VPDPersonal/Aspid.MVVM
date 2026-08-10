using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> to a Unix timestamp.
    /// </summary>
    /// <remarks>
    /// The forward direction of <see cref="UnixTimestampToDateTimeConverter"/>, which offers it only
    /// as its reverse — and a reverse runs only for a binder in <see cref="BindMode.TwoWay"/> or
    /// <see cref="BindMode.OneWayToSource"/>. A ViewModel that holds the date and a View that wants
    /// the number needs this one.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Time", Name = "Date Time To Unix Timestamp", Tooltip = "Converts a DateTime to a Unix timestamp")]
    public sealed class DateTimeToUnixTimestampConverter : IConverter<DateTime, long>
    {
        [Tooltip("Produce milliseconds rather than seconds.")]
        [SerializeField] private bool _milliseconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeToUnixTimestampConverter"/> class producing seconds.
        /// </summary>
        public DateTimeToUnixTimestampConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeToUnixTimestampConverter"/> class.
        /// </summary>
        /// <param name="milliseconds">Whether to produce milliseconds rather than seconds.</param>
        public DateTimeToUnixTimestampConverter(bool milliseconds)
        {
            _milliseconds = milliseconds;
        }

        /// <summary>
        /// Converts the specified date and time to a timestamp.
        /// </summary>
        /// <param name="value">The date and time.</param>
        /// <returns>The timestamp.</returns>
        public long Convert(DateTime value)
        {
            // A moment whose Kind is Unspecified is read as local, which is what ToUniversalTime
            // does with it and what UnixTimestampToDateTimeConverter assumes on the way back.
            var offset = new DateTimeOffset(value.ToUniversalTime());
            return _milliseconds ? offset.ToUnixTimeMilliseconds() : offset.ToUnixTimeSeconds();
        }
    }
}
