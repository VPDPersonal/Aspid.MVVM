#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes a color as an HTML string.
    /// </summary>
    /// <remarks>
    /// Each channel is clamped to 0..1 and rounded to a byte, so an HDR color writes as white.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color/To String",
        Name = "To Html String",
        Tooltip = "Writes a color as an HTML string")]
    public sealed class ColorToHtmlStringConverter : ITwoWayConverter<Color, string?>
    {
        [Tooltip("Include the alpha channel.")]
        [SerializeField] private bool _includeAlpha;

        [Tooltip("Prefix the string with a hash. A string written without it does not parse back.")]
        [SerializeField] private bool _includeHash = true;

        [Tooltip("Write the digits in lower case.")]
        [SerializeField] private bool _lowercase;

        [Tooltip("Returned when the string coming back is blank or does not parse.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private Color _convertBackFallback;

        /// <remarks>
        /// Default: <c>#RRGGBB</c> in upper case, with fully transparent black for a string that does
        /// not parse.
        /// </remarks>
        public ColorToHtmlStringConverter() { }

        /// <param name="includeAlpha">Whether to include the alpha channel.</param>
        /// <param name="includeHash">
        /// Whether to prefix the string with a hash. A string written without it does not parse back.
        /// </param>
        /// <param name="lowercase">Whether to write the digits in lower case.</param>
        /// <param name="convertBackFallback">
        /// Returned when the string coming back is blank or does not parse. When omitted, fully
        /// transparent black.
        /// </param>
        public ColorToHtmlStringConverter(
            bool includeAlpha,
            bool includeHash = true,
            bool lowercase = false,
            Color? convertBackFallback = null)
        {
            _includeAlpha = includeAlpha;
            _includeHash = includeHash;
            _lowercase = lowercase;
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
        }

        /// <summary>
        /// Writes the specified color as an HTML string.
        /// </summary>
        /// <param name="value">The color to write.</param>
        /// <returns><c>RRGGBB</c>, with the alpha pair and the leading hash as configured.</returns>
        public string Convert(Color value) =>
            Write(value, _includeAlpha, _includeHash, _lowercase);

        /// <summary>
        /// Parses an HTML string coming back from the View.
        /// </summary>
        /// <param name="value">The HTML color string (e.g., "#FF0000").</param>
        /// <returns>
        /// The parsed color, or the fallback. A blank string is treated as no value rather than as a
        /// failed parse and returns the fallback silently. A string written without the leading hash
        /// does not parse back.
        /// </returns>
        public Color ConvertBack(string? value) =>
            ParseHtmlStringConverter.Parse(this, value, _convertBackFallback);

        /// <summary>
        /// Writes a color as an HTML string.
        /// </summary>
        /// <param name="value">The color to write.</param>
        /// <param name="includeAlpha">Whether to include the alpha channel.</param>
        /// <param name="includeHash">Whether to prefix the string with a hash.</param>
        /// <param name="lowercase">Whether to write the digits in lower case.</param>
        /// <returns><c>RRGGBB</c>, with the alpha pair and the leading hash as asked for.</returns>
        // Shared with ParseHtmlStringConverter.ConvertBack, which always writes the alpha pair.
        internal static string Write(Color value, bool includeAlpha, bool includeHash, bool lowercase)
        {
            var digits = includeAlpha
                ? ColorUtility.ToHtmlStringRGBA(value)
                : ColorUtility.ToHtmlStringRGB(value);

            if (lowercase)
                digits = digits.ToLowerInvariant();

            return includeHash
                ? "#" + digits
                : digits;
        }
    }
}
