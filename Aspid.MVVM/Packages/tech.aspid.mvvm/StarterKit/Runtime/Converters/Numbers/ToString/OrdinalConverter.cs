#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as an English ordinal: 1 becomes "1st".
    /// </summary>
    /// <remarks>The suffix stays English whichever culture is picked. A float or double input is truncated.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Ordinal",
        Tooltip = "Formats a number as an English ordinal: 1 becomes '1st'")]
    public sealed class OrdinalConverter :
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<float, string>,
        IConverter<double, string>
    {
        [Tooltip("The culture the number is written with. Affects only the negative sign.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.InvariantCulture;

        /// <remarks>Default: writing invariant digits.</remarks>
        public OrdinalConverter() { }

        /// <param name="culture">The culture the number is written with. Affects only the negative sign.</param>
        public OrdinalConverter(CultureInfoMode culture)
        {
            _culture = culture;
        }

        /// <summary>
        /// Formats the specified number as an ordinal.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The number with its ordinal suffix.</returns>
        public string Convert(int value) => 
            Apply(value);

        string IConverter<long, string>.Convert(long value) => 
            Apply(value);

        string IConverter<float, string>.Convert(float value) => 
            Apply(NumericSaturation.ToLong(value));

        string IConverter<double, string>.Convert(double value) => 
            Apply(NumericSaturation.ToLong(value));

        private string Apply(long value)
        {
            var lastTwo = Math.Abs(value % 100);

            var suffix = lastTwo is >= 11 and <= 13
                ? "th"
                : (lastTwo % 10) switch { 1 => "st", 2 => "nd", 3 => "rd", _ => "th" };

            return value.ToString(_culture.ToCultureInfo()) + suffix;
        }
    }
}
