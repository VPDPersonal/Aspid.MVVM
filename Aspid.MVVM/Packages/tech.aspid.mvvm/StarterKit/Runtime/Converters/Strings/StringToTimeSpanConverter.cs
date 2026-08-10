using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a duration out of text.
    /// </summary>
    /// <remarks>
    /// The outbound half of this ships twice — <see cref="TimeSpanToStringConverter"/> and
    /// <see cref="TimeSpanFormatConverter"/> — while nothing read a duration back in; a cooldown or a
    /// session length arriving from a save file or a backend as <c>"00:01:30"</c> had to be parsed by
    /// the ViewModel before it could reach a binder.
    /// <para>
    /// A bare number is not seconds here: <c>TimeSpan</c> reads <c>"90"</c> as ninety <i>days</i>,
    /// which is the trap this converter is most often walked into. Text that counts seconds wants
    /// <see cref="StringToFloatConverter"/> followed by <see cref="SecondsToTimeSpanConverter"/>
    /// instead.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Time Span", Tooltip = "Reads a duration out of text")]
    public sealed class StringToTimeSpanConverter : IConverter<string?, TimeSpan>
    {
        [Tooltip("The exact format expected, as a TimeSpan format string — \"hh\\:mm\\:ss\". When "
            + "empty, any format the culture understands is accepted.")]
        [SerializeField] private string _format = string.Empty;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Ticks of the duration returned when the text is not one. There are ten million of "
            + "them to the second.")]
        [SerializeField] private long _fallbackTicks;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToTimeSpanConverter"/> class accepting any format.
        /// </summary>
        public StringToTimeSpanConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToTimeSpanConverter"/> class.
        /// </summary>
        /// <param name="format">The exact format expected.</param>
        /// <param name="fallback">Returned when the text is not a duration.</param>
        public StringToTimeSpanConverter(string format, TimeSpan fallback = default)
        {
            _format = format;
            _fallbackTicks = fallback.Ticks;
        }

        /// <summary>
        /// Reads a duration out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The duration, or the fallback when the text is not one.</returns>
        public TimeSpan Convert(string? value)
        {
            var culture = _culture.ToCultureInfo();
            var fallback = new TimeSpan(_fallbackTicks);

            // Empty text is absence rather than a malformed duration, so it takes the fallback quietly.
            if (string.IsNullOrWhiteSpace(value)) return fallback;

            var parsed = string.IsNullOrWhiteSpace(_format)
                ? TimeSpan.TryParse(value, culture, out var any) ? any : (TimeSpan?)null
                : TimeSpan.TryParseExact(value, _format, culture, out var exact) ? exact : null;

            return parsed ?? OnUnparsed(value, fallback);
        }

        private TimeSpan OnUnparsed(string? value, TimeSpan fallback)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToTimeSpanConverter), value, Expected());

            // Report keeps the first message and drops every one after it, and text that will not
            // parse usually fails on every push: composing the message past that point allocates a
            // string per notification for the guard inside Report to throw away.
            if (_loggedFailure) return fallback;

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToTimeSpanConverter), value, Expected(), "the fallback duration");
            return fallback;
        }

        private string Expected() =>
            string.IsNullOrWhiteSpace(_format) ? "a duration" : $"a duration shaped \"{_format}\"";

        [NonSerialized] private bool _loggedFailure;
    }
}
