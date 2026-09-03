#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a duration out of text.
    /// </summary>
    /// <remarks>A bare number is not seconds: <see cref="TimeSpan"/> reads <c>"90"</c> as ninety days.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Time",
        Name = "Parse Time Span",
        Tooltip = "Reads a duration out of text")]
    public sealed class StringToTimeSpanConverter : ITwoWayConverter<string?, TimeSpan>
    {
        [Tooltip("The exact TimeSpan format, e.g. \"hh\\:mm\\:ss\". Empty accepts any format the culture understands.")]
        [SerializeField] private string _format = string.Empty;

        [Tooltip("The culture the text is read and written with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a duration. Stored as ticks.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private long _fallbackTicks;

        /// <remarks>Default: accepting any format.</remarks>
        public StringToTimeSpanConverter() { }

        /// <param name="format">The exact TimeSpan format, e.g. <c>hh\:mm\:ss</c>. Empty accepts any format the culture understands.</param>
        /// <param name="fallback">Returned when the text is not a duration. When omitted, <see cref="TimeSpan.Zero"/>.</param>
        public StringToTimeSpanConverter(
            string format,
            TimeSpan? fallback = null)
        {
            _format = format;

            if (fallback is { } value)
                _fallbackTicks = value.Ticks;
        }

        /// <summary>
        /// Reads a duration out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The duration, or the fallback when the text is not one.</returns>
        public TimeSpan Convert(string? value)
        {
            var fallback = new TimeSpan(_fallbackTicks);

            if (string.IsNullOrWhiteSpace(value)) return fallback;

            var culture = _culture.ToCultureInfo();

            var parsed = string.IsNullOrWhiteSpace(_format)
                ? TimeSpan.TryParse(value, culture, out var any) ? any : (TimeSpan?)null
                : TimeSpan.TryParseExact(value, _format, culture, out var exact) ? exact : null;

            return parsed ?? this.UseFallback(
                fallback: fallback,
                problem: value.Expected(ExpectedText()));
        }

        /// <summary>
        /// Writes the specified duration as text.
        /// </summary>
        /// <param name="value">The duration to write.</param>
        /// <returns>The duration in the authored format, or in the culture's short form when none is authored or it is unusable.</returns>
        public string ConvertBack(TimeSpan value)
        {
            var culture = _culture.ToCultureInfo();

            if (string.IsNullOrWhiteSpace(_format)) return value.ToString("g", culture);

            try
            {
                return value.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                this.LogError(
                    problem: $"\"{_format}\" is not a TimeSpan format ({exception.Message})",
                    consequence: "Falling back to the short form.");

                return value.ToString("g", culture);
            }
        }

        private string ExpectedText() => string.IsNullOrWhiteSpace(_format)
            ? "a duration"
            : $"a duration shaped \"{_format}\"";
    }
}
