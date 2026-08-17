using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="TimeSpan"/> with a real <see cref="TimeSpan"/> format string.
    /// </summary>
    /// <remarks>
    /// <see cref="TimeSpanToStringConverter"/> takes a <i>composite</i> format, so the obvious
    /// <c>mm\:ss</c> comes back as itself and the pattern has to be wrapped as <c>{0:mm\:ss}</c>.
    /// This takes the pattern directly, the way <see cref="TimeSpan.ToString(string)"/> does.
    /// </remarks>
    [Serializable]
    public sealed class TimeSpanFormatConverter : IConverterTimeSpanToString
    {
        [Tooltip(@"A TimeSpan format string, for example mm\:ss or hh\:mm\:ss.")]
        [SerializeField] private string _format = @"mm\:ss";

        [Tooltip("The culture the duration is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: writing mm:ss.</remarks>
        public TimeSpanFormatConverter() { }

        /// <param name="format">A <see cref="TimeSpan"/> format string.</param>
        public TimeSpanFormatConverter(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Formats the specified duration.
        /// </summary>
        /// <param name="value">The duration to format.</param>
        /// <returns>The formatted duration, or the default rendering when the format is unusable.</returns>
        public string Convert(TimeSpan value)
        {
            if (string.IsNullOrWhiteSpace(_format)) return value.ToString();

            try
            {
                return value.ToString(_format, _culture.ToCultureInfo());
            }
            catch (FormatException exception)
            {
                Debug.LogError($"{nameof(TimeSpanFormatConverter)}: \"{_format}\" is not a TimeSpan format ({exception.Message}). Falling back to the default rendering.");
                return value.ToString();
            }
        }
    }
}
