using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a whole number out of text.
    /// </summary>
    /// <inheritdoc cref="StringToIntConverter" path="/remarks"/>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Long", Tooltip = "Reads a whole number out of text")]
    public sealed class StringToLongConverter : ITwoWayConverter<string?, long>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private long _fallback;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToLongConverter() { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        public StringToLongConverter(long fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public long Convert(string? value)
        {
            // Blank text is an unfilled field, not a malformed number. Reporting it would fire on
            // every scene with an empty input, which is the noise that gets error logs ignored.
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            return long.TryParse(value, NumberStyles.Integer, _culture.ToCultureInfo(), out var parsed)
                ? parsed
                : OnUnparsed(value);
        }

        private long OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToLongConverter), value, "a whole number");

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToLongConverter), value, "a whole number", "the fallback");
            return _fallback;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(long value) => value.ToString(_culture.ToCultureInfo());

        [NonSerialized] private bool _loggedFailure;
    }
}
