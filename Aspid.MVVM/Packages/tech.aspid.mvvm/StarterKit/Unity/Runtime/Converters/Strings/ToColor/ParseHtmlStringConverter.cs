#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts HTML color strings (e.g., "#FF0000") to <see cref="Color"/> values.
    /// </summary>
    /// <remarks>
    /// The default fallback is fully transparent black — also what <c>"#00000000"</c> parses to —
    /// so a failure is reported every time rather than inferred from the color.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Color",
        Name = "Parse Html Color",
        Tooltip = "Converts HTML color strings (e.g., '#FF0000') to Color values")]
    public sealed class ParseHtmlStringConverter : ITwoWayConverter<string?, Color>
    {
        [Tooltip("Returned when the string is blank or does not parse. When omitted, fully transparent black.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private Color _fallback;

        /// <remarks>Default: fully transparent black for a string that does not parse.</remarks>
        public ParseHtmlStringConverter() { }

        /// <param name="fallback">
        /// Returned when the string is blank or does not parse. When omitted, fully transparent black.
        /// </param>
        public ParseHtmlStringConverter(Color? fallback = null)
        {
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Converts an HTML color string to a <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The HTML color string (e.g., "#FF0000").</param>
        /// <returns>
        /// The parsed color, or the fallback. A blank string is treated as no value rather than as a
        /// failed parse and returns the fallback silently.
        /// </returns>
        public Color Convert(string? value) => Parse(this, value, _fallback);

        /// <summary>
        /// Writes the specified color as an HTML color string.
        /// </summary>
        /// <param name="value">The color to write.</param>
        /// <returns>
        /// <c>#RRGGBBAA</c>. The alpha pair is always written, so the string parses back to the color
        /// it came from; an HDR channel is clamped to 0..1 and rounded, and does not survive the trip.
        /// </returns>
        public string ConvertBack(Color value) =>
            ColorToHtmlStringConverter.Write(value, includeAlpha: true, includeHash: true, lowercase: false);

        /// <summary>
        /// Parses an HTML color string.
        /// </summary>
        /// <param name="reporter">The converter the string was pushed to — the report names it.</param>
        /// <param name="value">The HTML color string (e.g., "#FF0000").</param>
        /// <param name="fallback">Returned when the string is blank or does not parse.</param>
        /// <returns>
        /// The parsed color, or <paramref name="fallback"/>. A blank string is treated as no value
        /// rather than as a failed parse and takes the fallback silently.
        /// </returns>
        // Shared with ColorToHtmlStringConverter.ConvertBack, which reads back the string it wrote.
        internal static Color Parse(IConverter reporter, string? value, Color fallback)
        {
            if (string.IsNullOrWhiteSpace(value)) return fallback;

            return ColorUtility.TryParseHtmlString(value, out var color)
                ? color
                : reporter.UseFallback(fallback, value.Expected("an HTML color"));
        }
    }
}
