using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as an amount of currency.
    /// </summary>
    /// <remarks>
    /// A game currency uses the game's own symbol, which the <c>"C"</c> format cannot express — it
    /// only knows the player's locale.
    /// </remarks>
    [Serializable]
    public sealed class CurrencyConverter : IConverter<double, string>
    {
        [Tooltip("The symbol placed beside the amount.")]
        [SerializeField] private string _symbol = "$";

        [Tooltip("Which side of the amount the symbol goes on.")]
        [SerializeField] private SymbolPosition _position = SymbolPosition.Before;

        [Tooltip("How many decimals to show.")]
        [SerializeField] private int _decimals;

        [Tooltip("Separate thousands.")]
        [SerializeField] private bool _groupDigits = true;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: with a leading dollar sign.</remarks>
        public CurrencyConverter() { }

        /// <param name="symbol">The symbol placed beside the amount.</param>
        /// <param name="position">Which side of the amount the symbol goes on.</param>
        /// <param name="decimals">How many decimals to show.</param>
        public CurrencyConverter(string symbol, SymbolPosition position = SymbolPosition.Before, int decimals = 0)
        {
            _symbol = symbol;
            _position = position;
            _decimals = decimals;
        }

        /// <summary>
        /// Formats the specified amount.
        /// </summary>
        /// <param name="value">The amount.</param>
        /// <returns>The formatted amount with its symbol.</returns>
        public string Convert(double value)
        {
            var format = (_groupDigits ? "N" : "F") + Math.Max(0, _decimals);
            var text = value.ToString(format, _culture.ToCultureInfo());

            return _position is SymbolPosition.Before ? _symbol + text : text + _symbol;
        }
    }
}
