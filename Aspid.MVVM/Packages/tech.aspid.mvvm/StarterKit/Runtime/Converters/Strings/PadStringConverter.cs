using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Pads a string to a fixed width.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String",
        Name = "Pad",
        Tooltip = "Pads a string to a fixed width")]
    public sealed class PadStringConverter : IConverter<string?, string?>
    {
        [Tooltip("The width to pad to.")]
        [SerializeField] [Min(0)] private int _totalWidth = 8;

        [Tooltip("The character used for padding.")]
        [SerializeField] private char _padChar = ' ';

        [Tooltip("Pad on the left rather than the right.")]
        [SerializeField] private bool _padLeft = true;

        /// <remarks>Default: padding to eight characters.</remarks>
        public PadStringConverter() { }

        /// <param name="totalWidth">The width to pad to. A negative width is reported and read as zero.</param>
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
        /// <returns>The padded string, or the string itself when the authored width is negative.</returns>
        public string? Convert(string? value)
        {
            if (value is null) return null;

            var width = _totalWidth;

            // PadLeft and PadRight throw on a negative width, and the constructor takes any int.
            if (width < 0)
            {
                this.LogError($"the width to pad to is negative ({width})",
                    "Padding to zero, which leaves the string as it is.");

                width = 0;
            }

            return _padLeft ? value.PadLeft(width, _padChar) : value.PadRight(width, _padChar);
        }
    }
}
