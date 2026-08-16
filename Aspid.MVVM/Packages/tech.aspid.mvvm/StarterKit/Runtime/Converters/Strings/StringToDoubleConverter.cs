using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a decimal number out of text, keeping the precision a float would lose.
    /// </summary>
    /// <remarks>
    /// Use it over <see cref="StringToFloatConverter"/> when the number is large or precise rather than
    /// merely fractional — a currency total, a cumulative timer, an id that arrived as text — since a
    /// <see langword="float"/> carries about seven significant digits and quietly rounds past them.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Double", Tooltip = "Reads a decimal number out of text, keeping the precision a float would lose")]
    public sealed class StringToDoubleConverter : ITwoWayConverter<string?, double>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private double _fallback;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Hold the result inside the bounds below.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private double _min = double.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private double _max = double.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToDoubleConverter"/> class falling back to zero.
        /// </summary>
        public StringToDoubleConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToDoubleConverter"/> class.
        /// </summary>
        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToDoubleConverter(double fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback;
            _culture = culture;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public double Convert(string? value)
        {
            // Blank text is an unfilled field, not a malformed number. Reporting it would fire on
            // every scene with an empty input, which is the noise that gets error logs ignored.
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            const NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands;

            if (!double.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return OnUnparsed(value);

            return _clamp ? Clamp(parsed) : parsed;
        }

        // Two comparisons rather than Math.Clamp, which throws when Max is authored below Min: the
        // bounds are Inspector fields with nothing to validate them, and an exception on the push
        // path is not what ReturnFallback promises. A NaN fails both comparisons and passes through.
        private double Clamp(double value)
        {
            if (value < _min) return _min;
            return value > _max ? _max : value;
        }

        private double OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToDoubleConverter), value, "a decimal number");

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToDoubleConverter), value, "a decimal number", "the fallback");
            return _fallback;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(double value) => value.ToString(_culture.ToCultureInfo());

        [NonSerialized] private bool _loggedFailure;
    }
}
