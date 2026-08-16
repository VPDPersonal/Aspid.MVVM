#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts HTML color strings (e.g., "#FF0000") to <see cref="Color"/> values.
    /// </summary>
    /// <remarks>
    /// The default fallback is fully transparent black, which is also what <c>"#00000000"</c> parses
    /// to — so a failure and a success were indistinguishable in the scene. A failure is now reported
    /// every time, whichever mode is chosen.
    /// </remarks>
    [Serializable]
    public sealed class ParseHtmlStringConverter : IConverterStringToColor
    {
        [Tooltip("What to do with a string that does not parse. ReturnInput is not available here — the input is a string and the output a colour — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Returned when the string does not parse.")]
        [SerializeField] private Color _defaultColor = new(r: 0, g: 0, b: 0, a: 0);

        public ParseHtmlStringConverter() { }

        /// <param name="defaultColor">Returned when the string does not parse.</param>
        /// <param name="onFailure">What to do with a string that does not parse.</param>
        public ParseHtmlStringConverter(
            Color defaultColor,
            ConverterFailureMode onFailure = ConverterFailureMode.ReturnFallback)
        {
            _onFailure = onFailure;
            _defaultColor = defaultColor;
        }

        /// <summary>
        /// Converts an HTML color string to a <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The HTML color string (e.g., "#FF0000").</param>
        /// <returns>The parsed color, or the fallback colour when parsing fails.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the string cannot be parsed and <c>_onFailure</c> is
        /// <see cref="ConverterFailureMode.Throw"/>.
        /// </exception>
        public Color Convert(string? value)
        {
            if (ColorUtility.TryParseHtmlString(value, out var color)) return color;

            if (_onFailure is ConverterFailureMode.Throw)
                throw new ArgumentException($"Not an HTML colour: \"{value}\"", nameof(value));

            Debug.LogError($"{nameof(ParseHtmlStringConverter)}: \"{value}\" is not an HTML colour. Using the fallback colour.");
            return _defaultColor;
        }
    }
}
