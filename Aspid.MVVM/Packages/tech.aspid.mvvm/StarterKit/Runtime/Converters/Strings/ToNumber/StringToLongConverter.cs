#nullable enable
using System;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a whole number out of text, past the range an <see langword="int"/> can hold.
    /// </summary>
    /// <remarks>The culture decides the group separator: <c>1.000</c> is a thousand in one culture and nothing in another.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Number",
        Name = "Parse Long",
        Tooltip = "Reads a whole number out of text, past the range an int can hold")]
    public sealed class StringToLongConverter : StringToNumberConverter<long>
    {
        private const NumberStyles Styles = NumberStyles.Integer | NumberStyles.AllowThousands;

        /// <remarks>Default: falling back to zero.</remarks>
        public StringToLongConverter()
            : base(long.MinValue, long.MaxValue) { }

        /// <param name="fallback">Returned when the text is not a number.</param>
        /// <param name="culture">The culture the text is read with.</param>
        public StringToLongConverter(
            long fallback,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
            : base(long.MinValue, long.MaxValue, fallback, culture) { }

        /// <inheritdoc/>
        public override string ConvertBack(long value) =>
            value.ToString(Culture);

        /// <inheritdoc/>
        protected override string Expected => "a whole number";

        /// <inheritdoc/>
        protected override bool TryParse(string? value, CultureInfo culture, out long result) =>
            long.TryParse(value, Styles, culture, out result);

        /// <inheritdoc/>
        protected override long Clamp(long value, long min, long max) =>
            NumberText.Clamp(value, min, max);
    }
}
