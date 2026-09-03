#nullable enable
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base for a converter that reads a number out of text and writes it back.
    /// </summary>
    /// <typeparam name="T">The numeric type being read.</typeparam>
    /// <remarks>The clamp sits after the parse, so a fallback authored outside the bounds stays outside.</remarks>
    [Serializable]
    public abstract class StringToNumberConverter<T> : ITwoWayConverter<string?, T>
        where T : struct
    {
        [Tooltip("Hold the result inside the bounds.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private T _min;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private T _max;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Returned when the text is not a number.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private T _fallback;

        /// <param name="min">The lowest value the type holds; the default lower bound.</param>
        /// <param name="max">The highest value the type holds; the default upper bound.</param>
        protected StringToNumberConverter(T min, T max)
        {
            _min = min;
            _max = max;
        }

        /// <param name="min">The lowest value the type holds; the default lower bound.</param>
        /// <param name="max">The highest value the type holds; the default upper bound.</param>
        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        protected StringToNumberConverter(
            T min,
            T max,
            T fallback,
            CultureInfoMode culture)
            : this(min, max)
        {
            _culture = culture;
            _fallback = fallback;
        }

        /// <summary>
        /// Gets the culture the text is read and written with.
        /// </summary>
        protected CultureInfo Culture => _culture.ToCultureInfo();

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, held inside the bounds when clamping, or the fallback when the text is not one.</returns>
        public T Convert(string? value)
        {
            if (!TryParse(value, Culture, out var parsed))
            {
                return NumberText.Fallback(
                    value: value,
                    fallback: _fallback,
                    converter: this,
                    expected: Expected);
            }

            return _clamp ? Clamp(parsed, _min, _max) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public abstract string ConvertBack(T value);

        /// <summary>
        /// Gets what the text was expected to be, as a noun phrase: "a whole number".
        /// </summary>
        protected abstract string Expected { get; }

        /// <summary>
        /// Reads the number the specified text is written as.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <param name="culture">The culture the text is written in.</param>
        /// <param name="result">The number read, or the type default.</param>
        /// <returns><see langword="true"/> if the text is a number; otherwise, <see langword="false"/>.</returns>
        protected abstract bool TryParse(string? value, CultureInfo culture, out T result);

        /// <summary>
        /// Holds the number inside the bounds.
        /// </summary>
        /// <param name="value">The number to hold.</param>
        /// <param name="min">The lowest value allowed through.</param>
        /// <param name="max">The highest value allowed through.</param>
        /// <returns>The number, or the bound it fell outside.</returns>
        protected abstract T Clamp(T value, T min, T max);
    }
}
