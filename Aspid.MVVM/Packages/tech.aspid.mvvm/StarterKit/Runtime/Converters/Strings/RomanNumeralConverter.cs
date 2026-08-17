using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as a Roman numeral.
    /// </summary>
    /// <remarks>
    /// Tiers, chapters, upgrade levels. Numbers outside 1..3999 have no numeral and come back as
    /// digits.
    /// </remarks>
    [Serializable]
    public sealed class RomanNumeralConverter : IConverter<int, string>
    {
        [Tooltip("Write the numeral in lower case.")]
        [SerializeField] private bool _lowercase;

        private static readonly int[] Values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };

        private static readonly string[] Numerals =
            { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        /// <summary>
        /// Formats the specified number as a Roman numeral.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The numeral, or the number in digits when it is outside 1..3999.</returns>
        public string Convert(int value)
        {
            if (value is < 1 or > 3999) return value.ToString(CultureInfo.InvariantCulture);

            var builder = new System.Text.StringBuilder();
            var remaining = value;

            for (var i = 0; i < Values.Length; i++)
                while (remaining >= Values[i])
                {
                    builder.Append(Numerals[i]);
                    remaining -= Values[i];
                }

            var text = builder.ToString();
            return _lowercase ? text.ToLowerInvariant() : text;
        }
    }
}
