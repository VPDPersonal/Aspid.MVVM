using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a decimal number out of text.
    /// </summary>
    /// <remarks>
    /// The culture decides what a comma means: a German player typing <c>1,5</c> means one and a half,
    /// while read as invariant it gives fifteen or nothing at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Float",
        Tooltip = "Reads a decimal number out of text")]
    public sealed class StringToFloatConverter : ITwoWayConverter<string?, float>
    {
        [Tooltip("Hold the result inside the bounds.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private float _min = float.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private float _max = float.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a number.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private float _fallback;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToFloatConverter() { }

        /// <param name="fallback">Returned when the text is not a number. When omitted, zero.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToFloatConverter(float fallback = 0, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
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
        public float Convert(string? value)
        {
            // Grouped text — 1,000 in en-US — is a spelling of the number, not a mistake.
            const NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands;

            if (!float.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return NumberText.Fallback(value, _fallback, this, "a decimal number");

            // The clamp sits after the parse, so a fallback authored outside the bounds stays outside.
            return _clamp ? NumberText.Clamp(parsed, _min, _max) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text, in the round-trip format.</returns>
        public string ConvertBack(float value) => value.ToString("R", _culture.ToCultureInfo());
    }
}
