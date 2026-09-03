#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="DateTime"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To String",
        Name = "Date Time Format",
        Tooltip = "Formats a DateTime")]
    public sealed class DateTimeFormatConverter : IConverter<DateTime, string>
    {
        [Tooltip("A DateTime format string, for example dd.MM.yyyy or HH:mm.")]
        [SerializeField] private string _format = "g";

        [Tooltip("Convert to local time before formatting.")]
        [SerializeField] private bool _toLocalTime;

        [Tooltip("The culture the date is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: with the general format.</remarks>
        public DateTimeFormatConverter() { }

        /// <param name="format">A <see cref="DateTime"/> format string.</param>
        /// <param name="toLocalTime">Whether to convert to local time before formatting.</param>
        /// <param name="culture">The culture the date is formatted with.</param>
        public DateTimeFormatConverter(
            string format,
            bool toLocalTime = false,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
            _toLocalTime = toLocalTime;
        }

        /// <summary>
        /// Formats the specified date and time.
        /// </summary>
        /// <param name="value">The date and time to format.</param>
        /// <returns>The formatted date, or the default rendering when the format is unusable.</returns>
        public string Convert(DateTime value)
        {
            var moment = _toLocalTime
                ? value.ToLocalTime()
                : value;

            var culture = _culture.ToCultureInfo();

            return string.IsNullOrWhiteSpace(_format)
                ? moment.ToString(culture)
                : this.FormatOrGeneral(moment, _format, culture);
        }
    }
}
