#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a colour as an HTML string.
    /// </summary>
    /// <remarks>
    /// The missing inverse of <see cref="ParseHtmlStringConverter"/>, and the piece a rich-text
    /// colour tag is built from.
    /// </remarks>
    [Serializable]
    public sealed class ColorToHtmlStringConverter : IConverter<Color, string>
    {
        [Tooltip("Include the alpha channel.")]
        [SerializeField] private bool _includeAlpha;

        [Tooltip("Prefix the string with a hash.")]
        [SerializeField] private bool _includeHash = true;

        [Tooltip("Write the digits in lower case.")]
        [SerializeField] private bool _lowercase;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorToHtmlStringConverter"/> class.
        /// </summary>
        public ColorToHtmlStringConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorToHtmlStringConverter"/> class.
        /// </summary>
        /// <param name="includeAlpha">Whether to include the alpha channel.</param>
        /// <param name="includeHash">Whether to prefix the string with a hash.</param>
        public ColorToHtmlStringConverter(bool includeAlpha, bool includeHash = true)
        {
            _includeAlpha = includeAlpha;
            _includeHash = includeHash;
        }

        /// <summary>
        /// Writes the specified colour as an HTML string.
        /// </summary>
        /// <param name="value">The colour to write.</param>
        /// <returns>The HTML string.</returns>
        public string Convert(Color value)
        {
            var digits = _includeAlpha
                ? ColorUtility.ToHtmlStringRGBA(value)
                : ColorUtility.ToHtmlStringRGB(value);

            if (_lowercase) digits = digits.ToLowerInvariant();

            return _includeHash ? "#" + digits : digits;
        }
    }
}
