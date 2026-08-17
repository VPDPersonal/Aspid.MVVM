using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number against a maximum: "35 / 100".
    /// </summary>
    /// <remarks>
    /// Ammunition, health, quest progress. The maximum is authored, so the ViewModel exposes only the
    /// number that changes.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Ratio To String", Tooltip = "Formats a number against a maximum: '35 / 100'")]
    public sealed class RatioToStringConverter : IConverter<float, string>
    {
        [Tooltip("The value the number is shown against.")]
        [SerializeField] private float _max = 100f;

        [Tooltip("A composite format: {0} is the value, {1} the maximum.")]
        [SerializeField] private string _format = "{0} / {1}";

        [Tooltip("Round both numbers to whole values.")]
        [SerializeField] private bool _round = true;

        [Tooltip("The culture the numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: against 100.</remarks>
        public RatioToStringConverter() { }

        /// <param name="max">The value the number is shown against.</param>
        /// <param name="format">A composite format: <c>{0}</c> is the value, <c>{1}</c> the maximum.</param>
        public RatioToStringConverter(float max, string format = "{0} / {1}")
        {
            _max = max;
            _format = format;
        }

        /// <summary>
        /// Formats the specified value against the authored maximum.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The formatted ratio.</returns>
        public string Convert(float value)
        {
            var culture = _culture.ToCultureInfo();

            return _round
                ? string.Format(culture, _format, Mathf.RoundToInt(value), Mathf.RoundToInt(_max))
                : string.Format(culture, _format, value, _max);
        }
    }
}
