#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as an amount of currency.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Currency",
        Tooltip = "Formats a number as an amount of currency")]
    public sealed class CurrencyConverter :
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<float, string>
    {
        [Tooltip("The symbol placed beside the amount.")]
        [SerializeField] private string _symbol = "$";

        [Tooltip("Which side of the amount the symbol goes on.")]
        [SerializeField] private SymbolPosition _position = SymbolPosition.Before;

        [Tooltip("How many decimals to show.")]
        [SerializeField] [Min(0)] private int _decimals;

        [Tooltip("Separate thousands.")]
        [SerializeField] private bool _groupDigits = true;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: with a leading dollar sign.</remarks>
        public CurrencyConverter() { }

        /// <param name="symbol">The symbol placed beside the amount.</param>
        /// <param name="position">Which side of the amount the symbol goes on.</param>
        /// <param name="decimals">How many decimals to show.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="decimals"/> is negative.</exception>
        public CurrencyConverter(
            string symbol,
            SymbolPosition position = SymbolPosition.Before,
            int decimals = 0)
        {
            _symbol = symbol;
            _position = position;
            _decimals = decimals >= 0 ? decimals : throw new ArgumentOutOfRangeException(nameof(decimals));
        }

        /// <summary>
        /// Formats the specified amount.
        /// </summary>
        /// <param name="value">The amount.</param>
        /// <returns>The formatted amount with its symbol; a negative keeps the sign in front.</returns>
        public string Convert(double value)
        {
            var sign = value < 0d ? "-" : string.Empty;
            var text = Math.Abs(value).ToString(Format(), _culture.ToCultureInfo());

            return _position is SymbolPosition.Before ? sign + _symbol + text : sign + text + _symbol;
        }

        string IConverter<int, string>.Convert(int value) =>
            Convert(value);

        string IConverter<long, string>.Convert(long value) =>
            Convert(value);

        string IConverter<float, string>.Convert(float value) =>
            Convert(value);

        private string Format() => _groupDigits
            ? NumericFormat.Grouped(_decimals)
            : NumericFormat.Fixed(_decimals);
    }
}
