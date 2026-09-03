#nullable enable
using System;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a whole number out of text.
    /// </summary>
    /// <remarks>The culture decides the group separator: <c>1.000</c> is a thousand in one culture and nothing in another.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Int",
        Tooltip = "Reads a whole number out of text")]
    public sealed class StringToIntConverter : StringToNumberConverter<int>
    {
        private const NumberStyles Styles = NumberStyles.Integer | NumberStyles.AllowThousands;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToIntConverter()
            : base(int.MinValue, int.MaxValue) { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToIntConverter(
            int fallback,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
            : base(int.MinValue, int.MaxValue, fallback, culture) { }

        /// <inheritdoc/>
        public override string ConvertBack(int value) =>
            value.ToString(Culture);

        /// <inheritdoc/>
        protected override string Expected => "a whole number";

        /// <inheritdoc/>
        protected override bool TryParse(string? value, CultureInfo culture, out int result) =>
            int.TryParse(value, Styles, culture, out result);

        /// <inheritdoc/>
        protected override int Clamp(int value, int min, int max) =>
            NumberText.Clamp(value, min, max);
    }
}
