#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a date out of text.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Time",
        Name = "Parse Date Time",
        Tooltip = "Reads a date out of text")]
    public sealed class StringToDateTimeConverter : ITwoWayConverter<string?, DateTime>
    {
        [Tooltip("The exact format for reading and writing. Empty accepts any format the culture understands.")]
        [SerializeField] private string _format = string.Empty;

        [Tooltip("The culture the text is read and written with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a date. Stored as ticks.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private long _fallbackTicks;

        /// <remarks>Default: accepting any format.</remarks>
        public StringToDateTimeConverter() { }

        /// <param name="format">The exact format for reading and writing. Empty accepts any format the culture understands.</param>
        /// <param name="fallback">Returned when the text is not a date. When omitted, <see cref="DateTime.MinValue"/>.</param>
        public StringToDateTimeConverter(
            string format,
            DateTime? fallback = null)
        {
            _format = format;

            if (fallback is { } value)
                _fallbackTicks = value.Ticks;
        }

        /// <summary>
        /// Reads a date out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The date, or the fallback when the text is not one.</returns>
        public DateTime Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return Fallback();

            var culture = _culture.ToCultureInfo();

            var parsed = string.IsNullOrWhiteSpace(_format)
                ? DateTime.TryParse(value, culture, DateTimeStyles.None, out var any) ? any : (DateTime?)null
                : DateTime.TryParseExact(value, _format, culture, DateTimeStyles.None, out var exact) ? exact : null;

            if (parsed is { } date) return date;

            return this.UseFallback(
                fallback: Fallback(),
                problem: value.Expected(ExpectedText()));
        }

        /// <summary>
        /// Writes the specified date as text.
        /// </summary>
        /// <param name="value">The date to write.</param>
        /// <returns>The date in the authored format, or in the culture's general format when none is authored, or it is unusable.</returns>
        public string ConvertBack(DateTime value)
        {
            var culture = _culture.ToCultureInfo();

            if (string.IsNullOrWhiteSpace(_format)) return value.ToString(culture);

            try
            {
                return value.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                this.LogError(
                    problem: $"\"{_format}\" is not a DateTime format ({exception.Message})",
                    consequence: "Falling back to the default rendering.");

                return value.ToString(culture);
            }
        }

        private DateTime Fallback()
        {
            var ticks = _fallbackTicks;
            if (ticks >= DateTime.MinValue.Ticks && ticks <= DateTime.MaxValue.Ticks) return new DateTime(ticks);

            var clamped = ticks < DateTime.MinValue.Ticks
                ? DateTime.MinValue
                : DateTime.MaxValue;

            this.LogError(
                problem: $"the fallback tick count ({ticks}) is outside the range a date can hold",
                consequence: $"Using {clamped.ToString("O", CultureInfo.InvariantCulture)} instead.");

            return clamped;
        }

        private string ExpectedText() => string.IsNullOrWhiteSpace(_format)
            ? "a date"
            : $"a date shaped \"{_format}\"";
    }
}
