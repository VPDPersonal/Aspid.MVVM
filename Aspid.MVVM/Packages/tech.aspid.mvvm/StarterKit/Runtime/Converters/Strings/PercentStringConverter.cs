using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as a percentage.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Percent String", Tooltip = "Formats a number as a percentage")]
    public sealed class PercentStringConverter : IConverter<float, string>
    {
        [Tooltip("The incoming value is a 0..1 fraction rather than an already-scaled percentage.")]
        [SerializeField] private bool _inputIsNormalized = true;

        [Tooltip("How many decimals to show.")]
        [SerializeField] private int _decimals;

        [Tooltip("Placed after the number.")]
        [SerializeField] private string _suffix = "%";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: expecting a 0..1 fraction.</remarks>
        public PercentStringConverter() { }

        /// <param name="decimals">How many decimals to show.</param>
        /// <param name="inputIsNormalized">Whether the incoming value is a 0..1 fraction.</param>
        public PercentStringConverter(int decimals, bool inputIsNormalized = true)
        {
            _decimals = decimals;
            _inputIsNormalized = inputIsNormalized;
        }

        /// <summary>
        /// Formats the specified value as a percentage.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The formatted percentage.</returns>
        public string Convert(float value)
        {
            var percent = _inputIsNormalized ? value * 100f : value;
            return percent.ToString("F" + Math.Max(0, _decimals), _culture.ToCultureInfo()) + _suffix;
        }
    }
}
