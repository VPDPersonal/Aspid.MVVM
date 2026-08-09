using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a decimal number out of text.
    /// </summary>
    /// <remarks>
    /// The culture matters more here than anywhere else: a German player typing <c>1,5</c> means one
    /// and a half, and reading it as invariant gives fifteen or nothing at all.
    /// </remarks>
    [Serializable]
    public sealed class StringToFloatConverter : ITwoWayConverter<string?, float>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private float _fallback;

        [Tooltip("Hold the result inside the bounds below.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private float _min = float.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private float _max = float.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToFloatConverter"/> class falling back to zero.
        /// </summary>
        public StringToFloatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringToFloatConverter"/> class.
        /// </summary>
        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToFloatConverter(float fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback;
            _culture = culture;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public float Convert(string? value)
        {
            const NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands;

            if (!float.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return _fallback;

            return _clamp ? Mathf.Clamp(parsed, _min, _max) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(float value) => value.ToString(_culture.ToCultureInfo());
    }
}
