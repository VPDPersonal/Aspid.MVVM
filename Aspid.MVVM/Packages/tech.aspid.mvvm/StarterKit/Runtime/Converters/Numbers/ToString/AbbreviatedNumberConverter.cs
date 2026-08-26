using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Abbreviated",
        Tooltip = "Shortens a large number to a suffixed form: 1 234 567 becomes 1.23M")]
    public sealed class AbbreviatedNumberConverter :
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<float, string>
    {
        [Tooltip("The suffix for each power of a thousand, starting with none.")]
        [SerializeField] private string[] _suffixes = { "", "K", "M", "B", "T" };

        [Tooltip("How many decimals to show, shortened or in full.")]
        [SerializeField] [Min(0)] private int _decimals = 2;

        [Tooltip("Drop trailing zeros: 1.20M becomes 1.2M.")]
        [SerializeField] private bool _trimTrailingZeros = true;

        [Tooltip("Numbers below this are written out in full.")]
        [SerializeField] private double _threshold = 1000d;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        // The separator comes from the culture, so it is cached against the string it came from.
        [NonSerialized] private char[]? _separatorChars;
        [NonSerialized] private string? _separator;

        /// <remarks>Default: with K/M/B/T suffixes.</remarks>
        public AbbreviatedNumberConverter() { }

        /// <param name="decimals">How many decimals to show, shortened or in full.</param>
        /// <param name="suffixes">
        /// The suffix for each power of a thousand, starting with none. <see langword="null"/> keeps the
        /// defaults; an empty array is reported as an error and the number is written in full.
        /// </param>
        public AbbreviatedNumberConverter(int decimals, string[]? suffixes = null)
        {
            _decimals = decimals;
            if (suffixes is not null) _suffixes = suffixes;
        }

        /// <summary>
        /// Shortens the specified number.
        /// </summary>
        /// <param name="value">The number to shorten.</param>
        /// <returns>
        /// The shortened number with its suffix, or the number in full below the threshold and when no
        /// suffixes are set.
        /// </returns>
        public string Convert(double value)
        {
            var culture = _culture.ToCultureInfo();
            var magnitude = Math.Abs(value);

            if (_suffixes is not { Length: > 0 })
            {
                this.LogError("no suffixes are set", "Writing the number in full.");
                return Write(value, culture);
            }

            if (magnitude < _threshold) return Write(value, culture);

            var tier = 0;
            while (magnitude >= 1000d && tier < _suffixes.Length - 1)
            {
                magnitude /= 1000d;
                tier++;
            }

            // The decimals can carry the magnitude up to the next thousand: 999 999 written with two
            // of them is 1000.00K, which belongs a tier higher.
            if (tier < _suffixes.Length - 1 && Rounded(magnitude) >= 1000d)
            {
                magnitude /= 1000d;
                tier++;
            }

            return (value < 0 ? "-" : string.Empty) + Write(magnitude, culture) + _suffixes[tier];
        }

        string IConverter<int, string>.Convert(int value) => Convert(value);

        string IConverter<long, string>.Convert(long value) => Convert(value);

        string IConverter<float, string>.Convert(float value) => Convert(value);

        // Math.Round takes at most 15 places, and the field is authored.
        private double Rounded(double value) =>
            Math.Round(value, Math.Min(15, Math.Max(0, _decimals)), MidpointRounding.AwayFromZero);

        private string Write(double value, CultureInfo culture)
        {
            var text = value.ToString(NumericFormat.Fixed(_decimals), culture);
            return _trimTrailingZeros ? TrimZeros(text, culture) : text;
        }

        private string TrimZeros(string text, CultureInfo culture)
        {
            var separator = culture.NumberFormat.NumberDecimalSeparator;

            return !text.Contains(separator)
                ? text
                : text.TrimEnd('0').TrimEnd(SeparatorChars(separator));
        }

        private char[] SeparatorChars(string separator)
        {
            if (_separatorChars is not null && string.Equals(_separator, separator, StringComparison.Ordinal))
                return _separatorChars;

            _separator = separator;
            return _separatorChars = separator.ToCharArray();
        }
    }
}
