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
        [Tooltip("The exact format expected, for reading and writing. When empty, any format the " +
            "culture understands is accepted.")]
        [SerializeField] private string _format = string.Empty;

        [Tooltip("The culture the text is read and written with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a date. Stored as ticks.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private long _fallbackTicks;

        /// <remarks>Default: accepting any format.</remarks>
        public StringToDateTimeConverter() { }

        /// <param name="format">
        /// The exact format expected, and the one a date is written back in. When empty, any format the
        /// culture understands is accepted; an unusable one is reported and written in the culture's
        /// format.
        /// </param>
        /// <param name="fallback">
        /// Returned when the text is not a date. When omitted, <see cref="DateTime.MinValue"/>.
        /// </param>
        public StringToDateTimeConverter(string format, DateTime? fallback = null)
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
            // Empty text is absence rather than a malformed date, so it takes the fallback quietly.
            if (string.IsNullOrWhiteSpace(value)) return Fallback();

            var culture = _culture.ToCultureInfo();

            var parsed = string.IsNullOrWhiteSpace(_format)
                ? DateTime.TryParse(value, culture, DateTimeStyles.None, out var any) ? any : (DateTime?)null
                : DateTime.TryParseExact(value, _format, culture, DateTimeStyles.None, out var exact) ? exact : null;

            if (parsed is { } date) return date;

            // Built on demand so a mis-authored tick count is reported only when the fallback is used.
            return this.UseFallback(Fallback(), value.Expected(ExpectedText()));
        }

        /// <summary>
        /// Writes the specified date as text.
        /// </summary>
        /// <param name="value">The date to write.</param>
        /// <returns>
        /// The date in the authored format, or in the culture's general format — which keeps time only
        /// down to the second — when none is authored or the format is unusable.
        /// </returns>
        public string ConvertBack(DateTime value)
        {
            var culture = _culture.ToCultureInfo();

            if (string.IsNullOrWhiteSpace(_format)) return value.ToString(culture);

            // A format the parser merely refuses answers false; the same format makes ToString throw.
            try
            {
                return value.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                this.LogError($"\"{_format}\" is not a DateTime format ({exception.Message})",
                    "Falling back to the default rendering.");

                return value.ToString(culture);
            }
        }

        // The DateTime constructor throws on a tick count outside the calendar, and the field is a
        // free long.
        private DateTime Fallback()
        {
            var ticks = _fallbackTicks;
            if (ticks >= DateTime.MinValue.Ticks && ticks <= DateTime.MaxValue.Ticks) return new DateTime(ticks);

            var clamped = ticks < DateTime.MinValue.Ticks ? DateTime.MinValue : DateTime.MaxValue;

            this.LogError($"the fallback tick count ({ticks}) is outside the range a date can hold",
                $"Using {clamped.ToString("O", CultureInfo.InvariantCulture)} instead.");

            return clamped;
        }

        private string ExpectedText() =>
            string.IsNullOrWhiteSpace(_format) ? "a date" : $"a date shaped \"{_format}\"";
    }
}
