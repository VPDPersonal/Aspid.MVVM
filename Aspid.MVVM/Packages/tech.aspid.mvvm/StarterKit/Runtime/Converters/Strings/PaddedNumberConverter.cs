using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Pads a number to a fixed width: 7 becomes "007".
    /// </summary>
    [Serializable]
    public sealed class PaddedNumberConverter : IConverter<int, string>
    {
        [Tooltip("The minimum number of digits.")]
        [SerializeField] private int _digits = 2;

        [Tooltip("The character used for padding.")]
        [SerializeField] private char _padChar = '0';

        /// <remarks>Default: padding to two digits.</remarks>
        public PaddedNumberConverter() { }

        /// <param name="digits">The minimum number of digits.</param>
        /// <param name="padChar">The character used for padding.</param>
        public PaddedNumberConverter(int digits, char padChar = '0')
        {
            _digits = digits;
            _padChar = padChar;
        }

        /// <summary>
        /// Pads the specified number.
        /// </summary>
        /// <param name="value">The number to pad.</param>
        /// <returns>The padded number. A negative number keeps its sign outside the padding.</returns>
        public string Convert(int value)
        {
            var text = Math.Abs(value).ToString(CultureInfo.InvariantCulture).PadLeft(_digits, _padChar);
            return value < 0 ? "-" + text : text;
        }
    }
}
