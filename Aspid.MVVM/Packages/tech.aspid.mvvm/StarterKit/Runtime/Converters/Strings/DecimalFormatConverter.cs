using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a <see cref="decimal"/> with a standard .NET format string.
    /// </summary>
    /// <remarks>
    /// A <see cref="decimal"/> counts in hundredths exactly; <see cref="NumberFormatConverter"/> covers
    /// the other four numeric types but cannot take this one, because reaching it through
    /// <see cref="double"/> gives away the property the type was chosen for. Unity cannot serialize a
    /// <see cref="decimal"/> field, so the amount has to arrive from the ViewModel.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Decimal Format", Tooltip = "Formats a decimal with a standard .NET format string")]
    public sealed class DecimalFormatConverter : IConverter<decimal, string>
    {
        [Tooltip("A standard numeric format string: C2 for currency in the player's locale, N2 for "
            + "two decimals with thousands separators, F2 for two decimals without.")]
        [SerializeField] private string _format = "N2";

        [Tooltip("The culture the amount is formatted with. C picks its currency symbol from here.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: writing two decimals.</remarks>
        public DecimalFormatConverter() { }

        /// <param name="format">A standard numeric format string.</param>
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
        /// <returns>The formatted amount.</returns>
        public string Convert(decimal value) => value.ToString(_format, _culture.ToCultureInfo());
    }
}
