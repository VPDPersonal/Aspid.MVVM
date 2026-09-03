#nullable enable
using System;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a decimal number out of text, keeping the precision a float would lose.
    /// </summary>
    /// <remarks>The culture decides what a comma means: <c>1,5</c> is one and a half in German and fifteen in invariant.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Double",
        Tooltip = "Reads a decimal number out of text, keeping the precision a float would lose")]
    public sealed class StringToDoubleConverter : StringToNumberConverter<double>
    {
        private const NumberStyles Styles = NumberStyles.Float | NumberStyles.AllowThousands;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToDoubleConverter()
            : base(double.MinValue, double.MaxValue) { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToDoubleConverter(
            double fallback,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
            : base(double.MinValue, double.MaxValue, fallback, culture) { }

        /// <summary>
        /// Writes the specified number as text, in the round-trip format.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public override string ConvertBack(double value) =>
            value.ToString("R", Culture);

        /// <inheritdoc/>
        protected override string Expected => "a decimal number";

        /// <inheritdoc/>
        protected override bool TryParse(string? value, CultureInfo culture, out double result) =>
            double.TryParse(value, Styles, culture, out result);

        /// <inheritdoc/>
        protected override double Clamp(double value, double min, double max) =>
            NumberText.Clamp(value, min, max);
    }
}
