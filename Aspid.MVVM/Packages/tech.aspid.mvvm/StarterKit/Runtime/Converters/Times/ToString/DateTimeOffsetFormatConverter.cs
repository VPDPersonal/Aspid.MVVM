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
        /// <param name="offsetSource">
        /// The offset the moment is shown at. With <see cref="OffsetSource.Override"/> the offset is
        /// zero; use the <see cref="DateTimeOffsetFormatConverter(string, TimeSpan, CultureInfoMode)"/>
        /// overload to set one.
        /// </param>
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
        /// <param name="offsetOverride">
        /// The offset to show the moment at. A value past ±14 hours is clamped and reported as an error.
        /// </param>
        /// <param name="culture">The culture the date is formatted with.</param>
        public DateTimeOffsetFormatConverter(
            string format,
            TimeSpan offsetOverride,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
            _offsetSource = OffsetSource.Override;
            _offsetMinutes = (int)Math.Round(offsetOverride.TotalMinutes);
        }

        /// <summary>
        /// Formats the specified moment.
        /// </summary>
        /// <param name="value">The moment to format.</param>
        /// <returns>
        /// The formatted moment, or the default rendering when the format is unusable. An offset
        /// source that is not a declared value is reported and the moment is shown at the offset it
        /// arrived with.
        /// </returns>
        public string Convert(DateTimeOffset value)
        {
            var moment = At(value);
            var culture = _culture.ToCultureInfo();

            if (string.IsNullOrWhiteSpace(_format)) 
                return moment.ToString(culture);

            try
            {
                return moment.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                LogFormatFailure(exception);
                return moment.ToString(culture);
            }
        }

        private DateTimeOffset At(DateTimeOffset value) => _offsetSource switch
        {
            OffsetSource.AsGiven => value,
            OffsetSource.Local => value.ToLocalTime(),
            OffsetSource.Override => value.ToOffset(TimeSpan.FromMinutes(ClampedOffsetMinutes())),
            _ => Undeclared(value)
        };

        private DateTimeOffset Undeclared(DateTimeOffset value)
        {
            this.LogError(
                problem: $"the offset source {_offsetSource.Describe()} is not a declared {nameof(OffsetSource)}",
                consequence: "Showing the moment at the offset it arrived with.");

            return value;
        }

        // ToOffset throws past ±14 hours — a bad offset should show the wrong hour, not stop the binder.
        private int ClampedOffsetMinutes()
        {
            if (_offsetMinutes is >= -840 and <= 840) 
                return _offsetMinutes;

            var clamped = Math.Clamp(_offsetMinutes, -840, 840);

            this.LogError(
                problem: $"the offset override of {_offsetMinutes} minutes is past ±14 hours",
                consequence: $"Clamping to {clamped} minutes.");

            return clamped;
        }

        private void LogFormatFailure(FormatException exception) => this.LogError(
            problem: $"\"{_format}\" is not a DateTimeOffset format ({exception.Message})", 
            consequence: "Falling back to the default rendering.");
    }
}
