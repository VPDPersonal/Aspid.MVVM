using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a number out of text.
    /// </summary>
    /// <remarks>
    /// Both directions are here, but the binder converter field is same-type, so a cross-type two-way
    /// converter has nowhere to sit yet: until that changes these are for use from code and inside
    /// <see cref="ComposeConverter{TFrom, TMid, TTo}"/>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Int", Tooltip = "Reads a number out of text")]
    public sealed class StringToIntConverter : ITwoWayConverter<string?, int>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private int _fallback;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Hold the result inside the bounds below.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private int _min = int.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private int _max = int.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToIntConverter() { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToIntConverter(int fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback;
            _culture = culture;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public int Convert(string? value)
        {
            // Blank text is an unfilled field, not a malformed number. Reporting it would fire on
            // every scene with an empty input, which is the noise that gets error logs ignored.
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            // Grouped text is a spelling of the number rather than a mistake — a player typing
            // 1,000 in en-US means a thousand — and the float, double and decimal converters in this
            // family already read it as one. The group separator is the player's own, so a culture
            // that writes 1,5 for one and a half still refuses that text here.
            const NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowThousands;

            if (!int.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return OnUnparsed(value);

            return _clamp ? Clamp(parsed) : parsed;
        }

        // Two comparisons rather than Math.Clamp, which throws when Max is authored below Min: the
        // bounds are Inspector fields with nothing to validate them, and an exception on the push
        // path is not what ReturnFallback promises. A reversed pair reads as the minimum.
        private int Clamp(int value)
        {
            if (value < _min) return _min;
            return value > _max ? _max : value;
        }

        private int OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToIntConverter), value, "a whole number");

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToIntConverter), value, "a whole number", "the fallback");
            return _fallback;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(int value) => value.ToString(_culture.ToCultureInfo());

        [NonSerialized] private bool _loggedFailure;
    }
}
