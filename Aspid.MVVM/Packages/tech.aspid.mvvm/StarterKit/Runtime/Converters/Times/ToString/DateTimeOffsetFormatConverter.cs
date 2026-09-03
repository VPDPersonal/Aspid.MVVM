using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="DateTimeOffset"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To String",
        Name = "Date Time Offset Format",
        Tooltip = "Formats a DateTimeOffset")]
    public sealed class DateTimeOffsetFormatConverter : IConverter<DateTimeOffset, string>
    {
        [Tooltip("A DateTimeOffset format string, for example dd.MM.yyyy HH:mm or zzz.")]
        [SerializeField] private string _format = "g";

        [Tooltip("The offset the moment is shown at.")]
        [SerializeField] private OffsetSource _offsetSource = OffsetSource.AsGiven;

        [Tooltip("Minutes east of UTC to show the moment at.")]
        [SerializeField] [Range(-840, 840)] private int _offsetMinutes;

        [Tooltip("The culture the date is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: with the general format.</remarks>
        public DateTimeOffsetFormatConverter() { }

        /// <param name="format">A <see cref="DateTimeOffset"/> format string.</param>
        /// <param name="offsetSource">The offset the moment is shown at. <see cref="OffsetSource.Override"/> here means zero.</param>
        /// <param name="culture">The culture the date is formatted with.</param>
        public DateTimeOffsetFormatConverter(
            string format,
            OffsetSource offsetSource = OffsetSource.AsGiven,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
            _offsetSource = offsetSource;
        }

        /// <param name="format">A <see cref="DateTimeOffset"/> format string.</param>
        /// <param name="offsetOverride">The offset to show the moment at, within ±14 hours.</param>
        /// <param name="culture">The culture the date is formatted with.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="offsetOverride"/> is past ±14 hours.</exception>
        public DateTimeOffsetFormatConverter(
            string format,
            TimeSpan offsetOverride,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
            _offsetSource = OffsetSource.Override;

            var minutes = (int)Math.Round(offsetOverride.TotalMinutes);
            _offsetMinutes = minutes is >= -840 and <= 840 ? minutes : throw new ArgumentOutOfRangeException(nameof(offsetOverride));
        }

        /// <summary>
        /// Formats the specified moment.
        /// </summary>
        /// <param name="value">The moment to format.</param>
        /// <returns>The formatted moment, or the default rendering when the format is unusable. An undeclared source reports an error and keeps the offset.</returns>
        public string Convert(DateTimeOffset value)
        {
            var moment = At(value);
            var culture = _culture.ToCultureInfo();

            return string.IsNullOrWhiteSpace(_format)
                ? moment.ToString(culture)
                : this.FormatOrGeneral(moment, _format, culture);
        }

        private DateTimeOffset At(DateTimeOffset value) => _offsetSource switch
        {
            OffsetSource.AsGiven => value,
            OffsetSource.Local => value.ToLocalTime(),
            OffsetSource.Override => value.ToOffset(TimeSpan.FromMinutes(_offsetMinutes)),
            _ => Undeclared(value)
        };

        private DateTimeOffset Undeclared(DateTimeOffset value)
        {
            this.LogError(
                problem: $"the offset source {_offsetSource.Describe()} is not a declared {nameof(OffsetSource)}",
                consequence: "Showing the moment at the offset it arrived with.");

            return value;
        }
    }
}
