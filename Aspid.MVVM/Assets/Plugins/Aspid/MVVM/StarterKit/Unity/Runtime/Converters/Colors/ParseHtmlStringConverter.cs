#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts HTML color strings (e.g., "#FF0000") to <see cref="Color"/> values.
    /// </summary>
    [Serializable]
    public sealed class ParseHtmlStringConverter : IConverterStringToColor
    {
        [SerializeField] private bool _throwException;
        [SerializeField] private Color _defaultColor = new(r: 0, g: 0, b: 0, a: 0);

        /// <summary>
        /// Converts an HTML color string to a <see cref="Color"/>.
        /// </summary>
        /// <param name="value">The HTML color string (e.g., "#FF0000").</param>
        /// <returns>The parsed color, or the default color if parsing fails, and exceptions are disabled.</returns>
        /// <exception cref="ArgumentException">Thrown when the color string cannot be parsed and exception throwing is enabled.</exception>
        public Color Convert(string? value)
        {
            if (ColorUtility.TryParseHtmlString(value, out var color)) return color;
            if (!_throwException) return _defaultColor;

            throw new ArgumentException($"Invalid value: {value}");
        }
    }
}