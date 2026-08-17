using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M.
    /// </summary>
    /// <remarks>
    /// Every idle game, every leaderboard and every currency counter reinvents this. The suffixes are
    /// authored rather than hard-coded, because past trillions games stop agreeing on what to call
    /// the next one.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Abbreviated Number", Tooltip = "Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M")]
    public sealed class AbbreviatedNumberConverter : IConverter<double, string>
    {
        [Tooltip("The suffix for each power of a thousand, starting with none.")]
        [SerializeField] private string[] _suffixes = { "", "K", "M", "B", "T" };

        [Tooltip("How many decimals to show on a shortened number.")]
        [SerializeField] private int _decimals = 2;

        [Tooltip("Drop trailing zeros: 1.20M becomes 1.2M.")]
        [SerializeField] private bool _trimTrailingZeros = true;

        [Tooltip("Numbers below this are written out in full.")]
        [SerializeField] private double _threshold = 1000d;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: with K/M/B/T suffixes.</remarks>
        public AbbreviatedNumberConverter() { }

        /// <param name="decimals">How many decimals to show on a shortened number.</param>
        /// <param name="suffixes">The suffix for each power of a thousand, starting with none.</param>
        public AbbreviatedNumberConverter(int decimals, string[]? suffixes = null)
        {
            _decimals = decimals;
            if (suffixes is { Length: > 0 }) _suffixes = suffixes;
        }

        /// <summary>
        /// Shortens the specified number.
        /// </summary>
        /// <param name="value">The number to shorten.</param>
        /// <returns>The shortened number with its suffix.</returns>
        public string Convert(double value)
        {
            var culture = _culture.ToCultureInfo();
            var magnitude = Math.Abs(value);

            if (_suffixes is not { Length: > 0 } || magnitude < _threshold)
                return value.ToString("0.##", culture);

            var tier = 0;
            while (magnitude >= 1000d && tier < _suffixes.Length - 1)
            {
                magnitude /= 1000d;
                tier++;
            }

            var text = magnitude.ToString("F" + Math.Max(0, _decimals), culture);
            if (_trimTrailingZeros) text = TrimZeros(text, culture);

            return (value < 0 ? "-" : string.Empty) + text + _suffixes[tier];
        }

        private static string TrimZeros(string text, CultureInfo culture)
        {
            var separator = culture.NumberFormat.NumberDecimalSeparator;
            if (!text.Contains(separator)) return text;

            return text.TrimEnd('0').TrimEnd(separator.ToCharArray());
        }
    }
}
