#nullable enable
using System;
using UnityEngine;
using System.Text;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as a Roman numeral.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Roman Numeral",
        Tooltip = "Formats a number as a Roman numeral")]
    public sealed class RomanNumeralConverter : IConverter<int, string>
    {
        [Tooltip("Write the numeral in lower case.")]
        [SerializeField] private bool _lowercase;

        private static readonly int[] _values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        private static readonly string[] _numerals = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

        /// <remarks>Default: upper case.</remarks>
        public RomanNumeralConverter() { }

        /// <param name="lowercase">If <see langword="true"/>, writes the numeral in lower case.</param>
        public RomanNumeralConverter(bool lowercase)
        {
            _lowercase = lowercase;
        }

        /// <summary>
        /// Formats the specified number as a Roman numeral.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The numeral, or the number in digits when it is outside 1..3999.</returns>
        public string Convert(int value)
        {
            if (value is < 1 or > 3999) return value.ToString(CultureInfo.InvariantCulture);

            var builder = new StringBuilder();
            var remaining = value;

            for (var i = 0; i < _values.Length; i++)
            {
                while (remaining >= _values[i])
                {
                    builder.Append(_numerals[i]);
                    remaining -= _values[i];
                }
            }

            var text = builder.ToString();
            return _lowercase ? text.ToLowerInvariant() : text;
        }
    }
}
