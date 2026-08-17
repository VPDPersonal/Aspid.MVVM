using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a date out of text.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Date Time", Tooltip = "Reads a date out of text")]
    public sealed class StringToDateTimeConverter : IConverter<string?, DateTime>
    {
        [Tooltip("The exact format expected. When empty, any format the culture understands is accepted.")]
        [SerializeField] private string _format = string.Empty;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Ticks of the date returned when the text is not one.")]
        [SerializeField] private long _fallbackTicks;

        /// <remarks>Default: accepting any format.</remarks>
        public StringToDateTimeConverter() { }

        /// <param name="format">The exact format expected.</param>
        /// <param name="fallback">Returned when the text is not a date.</param>
        public StringToDateTimeConverter(string format, DateTime fallback = default)
        {
            _format = format;
            _fallbackTicks = fallback.Ticks;
        }

        /// <summary>
        /// Reads a date out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The date, or the fallback when the text is not one.</returns>
        public DateTime Convert(string? value)
        {
            var culture = _culture.ToCultureInfo();
            var fallback = new DateTime(_fallbackTicks);

            // Empty text is absence rather than a malformed date, so it takes the fallback quietly.
            if (string.IsNullOrWhiteSpace(value)) return fallback;

            var parsed = string.IsNullOrWhiteSpace(_format)
                ? DateTime.TryParse(value, culture, DateTimeStyles.None, out var any) ? any : (DateTime?)null
                : DateTime.TryParseExact(value, _format, culture, DateTimeStyles.None, out var exact) ? exact : null;

            return parsed ?? OnUnparsed(value, fallback);
        }

        private DateTime OnUnparsed(string? value, DateTime fallback)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToDateTimeConverter), value, Expected());

            // Report keeps the first message and drops every one after it, and text that will not
            // parse usually fails on every push: composing the message past that point allocates a
            // string per notification for the guard inside Report to throw away.
            if (_loggedFailure) return fallback;

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToDateTimeConverter), value, Expected(), "the fallback date");
            return fallback;
        }

        private string Expected() =>
            string.IsNullOrWhiteSpace(_format) ? "a date" : $"a date shaped \"{_format}\"";

        [NonSerialized] private bool _loggedFailure;
    }
}
