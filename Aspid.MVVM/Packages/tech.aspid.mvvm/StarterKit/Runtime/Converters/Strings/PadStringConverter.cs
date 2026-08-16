using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Pads a string to a fixed width.
    /// </summary>
    [Serializable]
    public sealed class PadStringConverter : IConverterString
    {
        [Tooltip("The width to pad to.")]
        [SerializeField] private int _totalWidth = 8;

        [Tooltip("The character used for padding.")]
        [SerializeField] private char _padChar = ' ';

        [Tooltip("Pad on the left rather than the right.")]
        [SerializeField] private bool _padLeft = true;

        /// <remarks>Default: padding to eight characters.</remarks>
        public PadStringConverter() { }

        /// <param name="totalWidth">The width to pad to.</param>
        /// <param name="padChar">The character used for padding.</param>
        /// <param name="padLeft">If <see langword="true"/>, pads on the left.</param>
        public PadStringConverter(int totalWidth, char padChar = ' ', bool padLeft = true)
        {
            _totalWidth = totalWidth;
            _padChar = padChar;
            _padLeft = padLeft;
        }

        /// <summary>
        /// Pads the specified string.
        /// </summary>
        /// <param name="value">The string to pad.</param>
        /// <returns>The padded string.</returns>
        public string? Convert(string? value)
        {
            if (value is null) return null;
            return _padLeft ? value.PadLeft(_totalWidth, _padChar) : value.PadRight(_totalWidth, _padChar);
        }
    }
}
