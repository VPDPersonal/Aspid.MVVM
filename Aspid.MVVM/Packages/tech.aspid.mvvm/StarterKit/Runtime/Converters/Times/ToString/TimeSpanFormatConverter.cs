using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="TimeSpan"/> with a real <see cref="TimeSpan"/> format string.
    /// </summary>
    /// <remarks>The pattern is taken directly, the way <see cref="TimeSpan.ToString(string)"/> takes it.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Time/To String",
        Name = "Time Span Format",
        Tooltip = "Formats a TimeSpan with a real TimeSpan format string")]
    public sealed class TimeSpanFormatConverter : IConverter<TimeSpan, string>
    {
        [Tooltip(@"A TimeSpan format string, for example mm\:ss or hh\:mm\:ss.")]
        [SerializeField] private string _format = @"mm\:ss";

        [Tooltip("The culture the duration is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: writing mm:ss.</remarks>
        public TimeSpanFormatConverter() { }

        /// <param name="format">A <see cref="TimeSpan"/> format string.</param>
        /// <param name="culture">The culture the duration is formatted with.</param>
        public TimeSpanFormatConverter(
            string format,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <summary>
        /// Formats the specified duration.
        /// </summary>
        /// <param name="value">The duration to format.</param>
        /// <returns>The formatted duration, or the default rendering when the format is unusable.</returns>
        public string Convert(TimeSpan value) => string.IsNullOrWhiteSpace(_format)
            ? value.ToString()
            : this.FormatOrGeneral(value, _format, _culture.ToCultureInfo());
    }
}
