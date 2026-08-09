using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a number out of text.
    /// </summary>
    /// <remarks>
    /// Both directions are here, but the binder converter field is same-type, so a cross-type two-way
    /// converter has nowhere to sit yet: until that changes these are for use from code and inside
    /// <see cref="ComposeConverter{TFrom, TMid, TTo}"/>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Int", Tooltip = "Reads a number out of text")]
    public sealed class StringToIntConverter : ITwoWayConverter<string?, int>
    {
        [Tooltip("Returned when the text is not a number.")]
        [SerializeField] private int _fallback;

        [Tooltip("Hold the result inside the bounds below.")]
        [SerializeField] private bool _clamp;

        [Tooltip("The lowest value allowed through when clamping.")]
        [SerializeField] private int _min = int.MinValue;

        [Tooltip("The highest value allowed through when clamping.")]
        [SerializeField] private int _max = int.MaxValue;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToIntConverter() { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToIntConverter(int fallback, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _fallback = fallback;
            _culture = culture;
        }

        /// <summary>
        /// Reads a number out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The number, or the fallback when the text is not one.</returns>
        public int Convert(string? value)
        {
            if (!int.TryParse(value, NumberStyles.Integer, _culture.ToCultureInfo(), out var parsed))
                return _fallback;

            return _clamp ? Math.Clamp(parsed, _min, _max) : parsed;
        }

        /// <summary>
        /// Writes the specified number as text.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public string ConvertBack(int value) => value.ToString(_culture.ToCultureInfo());
    }
}
