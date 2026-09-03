#nullable enable
using System;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a decimal number out of text.
    /// </summary>
    /// <remarks>The culture decides what a comma means: <c>1,5</c> is one and a half in German and fifteen in invariant.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Float",
        Tooltip = "Reads a decimal number out of text")]
    public sealed class StringToFloatConverter : StringToNumberConverter<float>
    {
        private const NumberStyles Styles = NumberStyles.Float | NumberStyles.AllowThousands;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToFloatConverter()
            : base(float.MinValue, float.MaxValue) { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToFloatConverter(
            float fallback,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
            : base(float.MinValue, float.MaxValue, fallback, culture) { }

        /// <summary>
        /// Writes the specified number as text, in the round-trip format.
        /// </summary>
        /// <param name="value">The number to write.</param>
        /// <returns>The text.</returns>
        public override string ConvertBack(float value) =>
            value.ToString("R", Culture);

        /// <inheritdoc/>
        protected override string Expected => "a decimal number";

        /// <inheritdoc/>
        protected override bool TryParse(string? value, CultureInfo culture, out float result) =>
            float.TryParse(value, Styles, culture, out result);

        /// <inheritdoc/>
        protected override float Clamp(float value, float min, float max) =>
            NumberText.Clamp(value, min, max);
    }
}
