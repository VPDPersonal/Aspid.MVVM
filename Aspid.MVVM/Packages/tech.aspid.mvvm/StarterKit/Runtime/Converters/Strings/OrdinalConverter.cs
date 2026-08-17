using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number as an English ordinal: 1 becomes "1st".
    /// </summary>
    [Serializable]
    public sealed class OrdinalConverter : IConverter<int, string>
    {
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

            return value.ToString(CultureInfo.InvariantCulture) + suffix;
        }
    }
}
