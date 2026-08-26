using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a whole number out of text.
    /// </summary>
    /// <remarks>
    /// The culture decides what the group separator is: <c>1.000</c> reads as a thousand in one culture
    /// and not at all in another.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Int",
        Tooltip = "Reads a whole number out of text")]
    public sealed class StringToIntConverter : ITwoWayConverter<string?, int>
    {
        [Tooltip("Hold the result inside the bounds.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private int _min = int.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private int _max = int.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a number.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private int _fallback;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToIntConverter() { }

        /// <param name="fallback">Returned when the text is not a number. When omitted, zero.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToIntConverter(int fallback = 0, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
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
        public int Convert(string? value)
        {
            // Grouped text — 1,000 in en-US — is a spelling of the number. The separator is the
            // culture's own, so 1,5 as one and a half is still refused.
            const NumberStyles styles = NumberStyles.Integer | NumberStyles.AllowThousands;

            if (!int.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return NumberText.Fallback(value, _fallback, this, "a whole number");

            // The clamp sits after the parse, so a fallback authored outside the bounds stays outside.
            return _clamp ? NumberText.Clamp(parsed, _min, _max) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(int value) => value.ToString(_culture.ToCultureInfo());
    }
}
