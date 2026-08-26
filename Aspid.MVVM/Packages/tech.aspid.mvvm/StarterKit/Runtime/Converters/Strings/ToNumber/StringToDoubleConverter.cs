using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a decimal number out of text, keeping the precision a float would lose.
    /// </summary>
    /// <remarks>
    /// The culture decides what a comma means: a German player typing <c>1,5</c> means one and a half,
    /// while read as invariant it gives fifteen or nothing at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Double",
        Tooltip = "Reads a decimal number out of text, keeping the precision a float would lose")]
    public sealed class StringToDoubleConverter : ITwoWayConverter<string?, double>
    {
        [Tooltip("Hold the result inside the bounds.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private double _min = double.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private double _max = double.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a number.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private double _fallback;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToDoubleConverter() { }

        /// <param name="fallback">Returned when the text is not a number. When omitted, zero.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToDoubleConverter(double fallback = 0, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback;
            _culture = culture;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>
        /// The number, held inside the bounds when clamping, or the fallback when the text is not one.
        /// </returns>
        public double Convert(string? value)
        {
            // Grouped text — 1,000 in en-US — is a spelling of the number, not a mistake.
            const NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands;

            if (!double.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return NumberText.Fallback(value, _fallback, this, "a decimal number");

            // The clamp sits after the parse, so a fallback authored outside the bounds stays outside.
            return _clamp ? NumberText.Clamp(parsed, _min, _max) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text, in the round-trip format.</returns>
        public string ConvertBack(double value) => value.ToString("R", _culture.ToCultureInfo());
    }
}
