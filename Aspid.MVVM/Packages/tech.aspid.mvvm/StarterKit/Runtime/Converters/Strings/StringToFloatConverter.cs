using Aspid.FastTools.Types;
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
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Float", Tooltip = "Reads a decimal number out of text")]
    public sealed class StringToFloatConverter : ITwoWayConverter<string?, float>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private float _fallback;

        [Tooltip("What to do with text that does not parse. ReturnInput is not available here — the "
            + "input is text and the output is not — and behaves as ReturnFallback.")]
        [SerializeField] private ConverterFailureMode _onFailure = ConverterFailureMode.ReturnFallback;

        [Tooltip("Hold the result inside the bounds below.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private float _min = float.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private float _max = float.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToFloatConverter() { }

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
            // Blank text is an unfilled field, not a malformed number. Reporting it would fire on
            // every scene with an empty input, which is the noise that gets error logs ignored.
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            const NumberStyles styles = NumberStyles.Float | NumberStyles.AllowThousands;

            if (!float.TryParse(value, styles, _culture.ToCultureInfo(), out var parsed))
                return OnUnparsed(value);

            return _clamp ? Clamp(parsed) : parsed;
        }

        // Two comparisons, the same shape the int, long and double siblings use: Math.Clamp throws
        // when Max is authored below Min, and the whole family reads a reversed pair as the minimum
        // rather than as a crash. A NaN fails both comparisons and passes through.
        private float Clamp(float value)
        {
            if (value < _min) return _min;
            return value > _max ? _max : value;
        }

        private float OnUnparsed(string? value)
        {
            if (_onFailure is ConverterFailureMode.Throw)
                throw ConverterFailure.Rejected(nameof(StringToFloatConverter), value, "a decimal number");

            ConverterFailure.Report(
                ref _loggedFailure, nameof(StringToFloatConverter), value, "a decimal number", "the fallback");
            return _fallback;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(float value) => value.ToString(_culture.ToCultureInfo());

        [NonSerialized] private bool _loggedFailure;
    }
}
