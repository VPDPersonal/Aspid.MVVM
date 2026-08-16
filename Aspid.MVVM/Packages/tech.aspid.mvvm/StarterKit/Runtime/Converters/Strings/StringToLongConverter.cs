using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a whole number out of text.
    /// </summary>
    /// <inheritdoc cref="StringToIntConverter" path="/remarks"/>
    [Serializable]
    public sealed class StringToLongConverter : ITwoWayConverter<string?, long>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private long _fallback;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToLongConverter() { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        public StringToLongConverter(long fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public long Convert(string? value) =>
            long.TryParse(value, NumberStyles.Integer, _culture.ToCultureInfo(), out var parsed) ? parsed : _fallback;

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(long value) => value.ToString(_culture.ToCultureInfo());
    }
}
