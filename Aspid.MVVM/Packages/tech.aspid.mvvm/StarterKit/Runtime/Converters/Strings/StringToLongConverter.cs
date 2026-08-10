using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a whole number out of text, past the range an <see langword="int"/> can hold.
    /// </summary>
    /// <inheritdoc cref="StringToIntConverter" path="/remarks"/>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "String To Long",
        Tooltip = "Reads a whole number out of text, past the range an int can hold")]
    public sealed class StringToLongConverter : ITwoWayConverter<string?, long>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private long _fallback;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Hold the result inside the bounds below.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private long _min = long.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private long _max = long.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToLongConverter() { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToLongConverter(long fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback;
            _culture = culture;
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

            // Grouped text is read here for the same reason as in StringToIntConverter, whose
            // remarks this class shares: the family documents one culture story, and a thousand
            // cannot be spelled one way for the int path and another for the double one.
            const NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowThousands;

            if (!long.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return OnUnparsed(value);

            return _clamp ? Clamp(parsed) : parsed;
        }

        // Two comparisons rather than Math.Clamp, which throws when Max is authored below Min: the
        // bounds are Inspector fields with nothing to validate them, and an exception on the push
        // path is not what ReturnFallback promises. A reversed pair reads as the minimum.
        private long Clamp(long value)
        {
            if (value < _min) return _min;
            return value > _max ? _max : value;
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
