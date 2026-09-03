#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads an exact decimal number out of text.
    /// </summary>
    /// <remarks>Unity cannot serialize a <see langword="decimal"/>, so the fallback and bounds are text in the invariant culture: <c>1.5</c>, never <c>1,5</c>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Decimal",
        Tooltip = "Reads an exact decimal number out of text")]
    public sealed class StringToDecimalConverter : ITwoWayConverter<string?, decimal>
    {
        [Tooltip("Hold the result inside the bounds.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping, in the invariant culture. Blank means no bound.")]
        [SerializeField] private string _min = string.Empty;

        [Tooltip("The highest value allowed through when clamping, in the invariant culture. Blank means no bound.")]
        [SerializeField] private string _max = string.Empty;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a number, in the invariant culture.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private string _fallback = "0";

        [NonSerialized] private DecimalText _fallbackText;
        [NonSerialized] private DecimalText _minText;
        [NonSerialized] private DecimalText _maxText;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToDecimalConverter() { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToDecimalConverter(
            decimal fallback,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _culture = culture;
            _fallback = fallback.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, held inside the bounds when clamping, or the fallback when the text is not one.</returns>
        public decimal Convert(string? value)
        {
            const NumberStyles styles = NumberStyles.Number | NumberStyles.AllowExponent;

            if (!decimal.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
            {
                return NumberText.Fallback(
                    value: value,
                    fallback: Fallback(),
                    converter: this,
                    expected: "an exact decimal number");
            }

            return _clamp ? Clamp(parsed) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(decimal value) =>
            value.ToString(_culture.ToCultureInfo());

        private decimal Fallback() =>
            _fallbackText.Read(_fallback, decimal.Zero, this, "the fallback");

        private decimal Clamp(decimal value) => NumberText.Clamp(
            value,
            _minText.Read(_min, decimal.MinValue, this, "the lowest value"),
            _maxText.Read(_max, decimal.MaxValue, this, "the highest value"));
    }
}
