#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Pads a number to a fixed width: 7 becomes "007".
    /// </summary>
    /// <remarks>A float or double input is truncated to a whole number.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Padded",
        Tooltip = "Pads a number to a fixed width: 7 becomes '007'")]
    public sealed class PaddedNumberConverter :
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<float, string>,
        IConverter<double, string>
    {
        [Tooltip("The minimum number of digits.")]
        [SerializeField] [Min(0)] private int _digits = 2;

        [Tooltip("The character used for padding.")]
        [SerializeField] private char _padChar = '0';

        /// <remarks>Default: padding to two digits.</remarks>
        public PaddedNumberConverter() { }

        /// <param name="digits">The minimum number of digits.</param>
        /// <param name="padChar">The character used for padding.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="digits"/> is negative.</exception>
        public PaddedNumberConverter(
            int digits,
            char padChar = '0')
        {
            _padChar = padChar;
            _digits = digits >= 0 ? digits : throw new ArgumentOutOfRangeException(nameof(digits));
        }

        /// <summary>
        /// Pads the specified number.
        /// </summary>
        /// <param name="value">The number to pad.</param>
        /// <returns>The padded number; a negative keeps its sign outside the padding.</returns>
        public string Convert(int value) =>
            Apply(value);

        string IConverter<long, string>.Convert(long value) =>
            Apply(value);

        string IConverter<float, string>.Convert(float value) =>
            Apply(NumericSaturation.ToLong(value));

        string IConverter<double, string>.Convert(double value) =>
            Apply(NumericSaturation.ToLong(value));

        private string Apply(long value)
        {
            // Unsigned rather than negated: long.MinValue has no positive counterpart.
            var magnitude = value < 0
                ? unchecked((ulong)-value) 
                : (ulong)value;

            var digits = magnitude.ToString(CultureInfo.InvariantCulture);
            var text = digits.PadLeft(_digits, _padChar);

            return value < 0 ? "-" + text : text;
        }
    }
}
