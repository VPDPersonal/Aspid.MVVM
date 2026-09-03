#nullable enable
using System;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a value with a format string, falling back to the general format when .NET refuses it.
    /// </summary>
    internal static class FormatExtensions
    {
        /// <summary>
        /// Formats the specified value, reporting an unusable format string and using the general format instead.
        /// </summary>
        /// <typeparam name="T">The formattable type; a struct, so the value is not boxed.</typeparam>
        /// <param name="converter">The reporting converter.</param>
        /// <param name="value">The value to format.</param>
        /// <param name="format">A format string the type understands.</param>
        /// <param name="culture">The culture the value is formatted with.</param>
        /// <returns>The formatted value, or its general rendering when the format is unusable.</returns>
        internal static string FormatOrGeneral<T>(
            this IConverter converter,
            T value,
            string format,
            CultureInfo culture)
            where T : struct, IFormattable
        {
            try
            {
                return value.ToString(format, culture);
            }
            catch (FormatException exception)
            {
                converter.LogError(
                    problem: $"{format.Describe()} is not a {typeof(T).GetTypeName()} format ({exception.Message})",
                    consequence: "Falling back to the general format.");

                return value.ToString(string.Empty, culture);
            }
        }
    }
}
