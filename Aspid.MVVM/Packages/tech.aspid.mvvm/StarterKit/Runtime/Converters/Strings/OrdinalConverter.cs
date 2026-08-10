using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as an English ordinal: 1 becomes "1st".
    /// </summary>
    /// <remarks>
    /// The suffix is grammar rather than formatting, and a <see cref="CultureInfo"/> carries no
    /// ordinal rules, so the culture reaches the digits only — the suffix stays English whichever
    /// culture is picked. The culture is worth almost nothing here and is offered only for
    /// consistency with the other number converters: .NET does not substitute native digits when
    /// formatting an integer, so an Arabic or Burmese culture still writes 1234 rather than ١٢٣٤,
    /// and the only culture-visible difference is the negative sign — which no ordinal has. A
    /// language that needs its own suffixes needs its own converter.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Ordinal", Tooltip = "Formats a number as an English ordinal: 1 becomes '1st'")]
    public sealed class OrdinalConverter : IConverter<int, string>
    {
        [Tooltip("Kept for consistency with the other number converters. It changes nothing for "
            + "any ordinal: .NET writes ASCII digits whatever the culture, and the suffix is English.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdinalConverter"/> class writing invariant digits.
        /// </summary>
        public OrdinalConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdinalConverter"/> class.
        /// </summary>
        /// <param name="culture">The culture the digits are written with.</param>
        public OrdinalConverter(CultureInfoMode culture)
        {
            _culture = culture;
        }

        /// <summary>
        /// Formats the specified number as an ordinal.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The number with its ordinal suffix.</returns>
        public string Convert(int value)
        {
            var magnitude = Math.Abs(value);

            // 11th, 12th and 13th break the last-digit rule.
            var suffix = (magnitude % 100) is >= 11 and <= 13
                ? "th"
                : (magnitude % 10) switch { 1 => "st", 2 => "nd", 3 => "rd", _ => "th" };

            return value.ToString(_culture.ToCultureInfo()) + suffix;
        }
    }
}
