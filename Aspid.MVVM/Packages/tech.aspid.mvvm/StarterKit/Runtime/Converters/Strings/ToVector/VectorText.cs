using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The reading the two vector parsers share.
    /// </summary>
    internal static class VectorText
    {
        /// <summary>
        /// Resolves the text placed between the components.
        /// </summary>
        /// <param name="separator">The authored separator.</param>
        /// <returns>The separator, or a comma when none is authored.</returns>
        internal static string Separator(string? separator) =>
            string.IsNullOrEmpty(separator) ? "," : separator;

        /// <summary>
        /// Resolves the culture the components are written and read in.
        /// </summary>
        /// <param name="culture">The authored culture.</param>
        /// <param name="separator">The text placed between the components.</param>
        /// <returns>
        /// The culture, or the invariant one when its decimal separator is the separator itself.
        /// </returns>
        /// <remarks>
        /// A culture whose decimal separator is the component separator would make the text ambiguous.
        /// </remarks>
        internal static CultureInfo ComponentCulture(CultureInfo culture, string separator) =>
            string.Equals(culture.NumberFormat.NumberDecimalSeparator, separator, StringComparison.Ordinal)
                ? CultureInfo.InvariantCulture
                : culture;

        /// <summary>
        /// Finds the part of the text the components are written in.
        /// </summary>
        /// <param name="value">The text to look at.</param>
        /// <returns>The first index of the body and the index just past its end.</returns>
        /// <remarks>
        /// <see cref="Vector3.ToString()"/> writes <c>"(1.00, 2.00, 3.00)"</c>, so text copied out of a
        /// log arrives wrapped in brackets.
        /// </remarks>
        internal static (int Start, int End) Unwrap(string value)
        {
            var start = 0;
            var end = value.Length;

            while (start < end && char.IsWhiteSpace(value[start])) start++;
            while (end > start && char.IsWhiteSpace(value[end - 1])) end--;

            if (end - start >= 2 && value[start] == '(' && value[end - 1] == ')')
            {
                start++;
                end--;
            }

            return (start, end);
        }

        /// <summary>
        /// Reads one component of a vector out of a stretch of text.
        /// </summary>
        /// <param name="value">The text holding the component.</param>
        /// <param name="start">Where the component begins.</param>
        /// <param name="length">How long it is.</param>
        /// <param name="culture">The culture it is written in.</param>
        /// <param name="axis">The number read, or zero when there is none.</param>
        /// <returns><see langword="true"/> when the stretch of text is a number.</returns>
        /// <remarks>
        /// Thousands separators are refused: in most cultures the group separator is also the
        /// separator between components.
        /// </remarks>
        internal static bool TryReadAxis(string value, int start, int length, CultureInfo culture, out float axis)
        {
            axis = 0f;
            if (length <= 0) return false;

            return float.TryParse(value.Substring(start, length), NumberStyles.Float, culture, out axis);
        }
    }
}
