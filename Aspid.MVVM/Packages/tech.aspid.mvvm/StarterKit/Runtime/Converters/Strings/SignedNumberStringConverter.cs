using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with an explicit sign: "+15", "-3".
    /// </summary>
    /// <remarks>Floating combat text, stat deltas — where the sign is the point.</remarks>
    [Serializable]
    public sealed class SignedNumberStringConverter : IConverter<float, string>
    {
        [Tooltip("A standard numeric format string applied to the magnitude.")]
        [SerializeField] private string _format = "0.##";

        [Tooltip("Show a plus on positive numbers.")]
        [SerializeField] private bool _alwaysShowSign = true;

        [Tooltip("Return an empty string for zero.")]
        [SerializeField] private bool _hideZero;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        public SignedNumberStringConverter() { }

        /// <param name="format">A standard numeric format string applied to the magnitude.</param>
        /// <param name="hideZero">If <see langword="true"/>, returns an empty string for zero.</param>
        public SignedNumberStringConverter(string format, bool hideZero = false)
        {
            _format = format;
            _hideZero = hideZero;
        }

        /// <summary>
        /// Formats the specified number with its sign.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The formatted number.</returns>
        public string Convert(float value)
        {
            if (_hideZero && value == 0f) return string.Empty;

            var text = Math.Abs(value).ToString(_format, _culture.ToCultureInfo());
            if (value < 0f) return "-" + text;

            return _alwaysShowSign ? "+" + text : text;
        }
    }
}
