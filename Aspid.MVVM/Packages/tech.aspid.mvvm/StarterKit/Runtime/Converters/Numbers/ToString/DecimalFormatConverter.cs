using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="decimal"/> with a standard .NET format string.
    /// </summary>
    /// <remarks>
    /// Unity cannot serialize a <see cref="decimal"/> field, so the amount has to arrive from the
    /// ViewModel. A format string .NET refuses is reported as an error and the general format is used
    /// instead.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Format (Decimal)",
        Tooltip = "Formats a decimal with a standard .NET format string")]
    public sealed class DecimalFormatConverter : IConverter<decimal, string>
    {
        [Tooltip("A standard numeric format string: " +
            "C2 for currency, N2 for two decimals with thousands separators, F2 for two without.")]
        [SerializeField] private string _format = "N2";

        [Tooltip("The culture the amount is formatted with. C picks its currency symbol from here.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: writing two decimals.</remarks>
        public DecimalFormatConverter() { }

        /// <param name="format">
        /// A standard numeric format string. One .NET refuses falls back to the general format and is
        /// reported as an error.
        /// </param>
        /// <param name="culture">The culture the amount is formatted with.</param>
        public DecimalFormatConverter(string format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <summary>
        /// Formats the specified amount.
        /// </summary>
        /// <param name="value">The amount to format.</param>
        /// <returns>The formatted amount, or the general rendering when the format is unusable.</returns>
        public string Convert(decimal value)
        {
            var culture = _culture.ToCultureInfo();

            try
            {
                return value.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                this.LogError(
                    problem: $"{_format.Describe()} is not a numeric format ({exception.Message})",
                    consequence: "Falling back to the general format.");

                return value.ToString(culture);
            }
        }
    }
}
