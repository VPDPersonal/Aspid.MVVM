#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The reading the numeric parsers share.
    /// </summary>
    internal static class NumberText
    {
        /// <summary>
        /// Answers text no parser could read.
        /// </summary>
        /// <typeparam name="T">The number type the converter returns.</typeparam>
        /// <param name="value">The text that would not read.</param>
        /// <param name="fallback">The converter's fallback.</param>
        /// <param name="converter">The failing converter.</param>
        /// <param name="expected">What the converter needed, as a noun phrase: "a whole number".</param>
        /// <returns>The fallback value.</returns>
        /// <remarks>Blank text takes the fallback without reporting a failure.</remarks>
        internal static T Fallback<T>(
            string? value,
            T fallback,
            IConverter converter,
            string expected) =>
            string.IsNullOrWhiteSpace(value)
                ? fallback
                : converter.UseFallback(
                    fallback: fallback,
                    problem: value.Expected(expected));

        /// <summary>
        /// Holds a number inside a pair of authored bounds.
        /// </summary>
        /// <param name="value">The number to hold.</param>
        /// <param name="min">The lowest value allowed through.</param>
        /// <param name="max">The highest value allowed through.</param>
        /// <returns>The number, or the bound it fell outside.</returns>
        /// <remarks>Unlike <c>Math.Clamp</c>, a maximum below the minimum does not throw.</remarks>
        internal static int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            return value > max ? max : value;
        }

        /// <inheritdoc cref="Clamp(int, int, int)"/>
        internal static long Clamp(long value, long min, long max)
        {
            if (value < min) return min;
            return value > max ? max : value;
        }

        /// <inheritdoc cref="Clamp(int, int, int)"/>
        /// <remarks>Unlike <c>Math.Clamp</c>, a maximum below the minimum does not throw; a NaN passes through.</remarks>
        internal static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            return value > max ? max : value;
        }

        /// <inheritdoc cref="Clamp(float, float, float)"/>
        internal static double Clamp(double value, double min, double max)
        {
            if (value < min) return min;
            return value > max ? max : value;
        }

        /// <inheritdoc cref="Clamp(int, int, int)"/>
        internal static decimal Clamp(decimal value, decimal min, decimal max)
        {
            if (value < min) return min;
            return value > max ? max : value;
        }
    }
}
