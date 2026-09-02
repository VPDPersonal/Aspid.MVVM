// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number to a narrower type by returning the nearest value that type can hold.
    /// </summary>
    internal static class NumericSaturation
    {
        /// <summary>
        /// Converts a <see cref="long"/> to an <see cref="int"/>, saturating at its bounds.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value, or the nearest <see cref="int"/> when it is out of range.</returns>
        internal static int ToInt(long value) => value switch
        {
            >= int.MaxValue => int.MaxValue,
            <= int.MinValue => int.MinValue,
            _ => (int)value
        };

        /// <summary>
        /// Converts a <see cref="double"/> to an <see cref="int"/>, saturating at its bounds.
        /// </summary>
        /// <param name="value">The value to convert. The fraction is dropped toward zero.</param>
        /// <returns>The value, the nearest <see cref="int"/> when it is out of range, or zero for a NaN.</returns>
        internal static int ToInt(double value) => value switch
        {
            // The order of the tests matters: a NaN fails every comparison, so it has to be caught
            // before the bounds rather than after, or it falls through to the undefined cast this
            // exists to avoid.
            double.NaN => 0,
            >= int.MaxValue => int.MaxValue,
            <= int.MinValue => int.MinValue,
            _ => (int)value
        };

        /// <summary>
        /// Converts a <see cref="ulong"/> to a <see cref="long"/>, saturating at its upper bound.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value, or <see cref="long.MaxValue"/> when it is out of range.</returns>
        internal static long ToLong(ulong value) =>
            value > long.MaxValue ? long.MaxValue : (long)value;

        /// <summary>
        /// Converts a <see cref="double"/> to a <see cref="long"/>, saturating at its bounds.
        /// </summary>
        /// <param name="value">The value to convert. The fraction is dropped toward zero.</param>
        /// <returns>The value, the nearest <see cref="long"/> when it is out of range, or zero for a NaN.</returns>
        internal static long ToLong(double value) => value switch
        {
            // The order of the tests matters: a NaN fails every comparison, so it has to be caught
            // before the bounds rather than after, or it falls through to the undefined cast this
            // exists to avoid.
            double.NaN => 0L,
            // long.MaxValue has no exact double, and the nearest one is 2^63 — one above it. Testing
            // against the literal keeps the boundary where it belongs instead of one ulp past it.
            >= 9223372036854775808d => long.MaxValue,
            <= long.MinValue => long.MinValue,
            _ => (long)value
        };

        /// <summary>
        /// Converts a <see cref="double"/> to a <see cref="float"/>, saturating at its bounds.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The value as a <see cref="float"/>, or the nearest finite one when it is out of range. A NaN
        /// or an infinity passes through.
        /// </returns>
        internal static float ToFloat(double value)
        {
            // NaN and the infinities are representable in a float, so saturation has nothing to do:
            // clamping an infinity to float.MaxValue would turn "no bound" into a specific number,
            // which is a different statement about the value rather than the nearest one.
            if (double.IsNaN(value) || double.IsInfinity(value)) return (float)value;

            return value switch
            {
                >= float.MaxValue => float.MaxValue,
                <= float.MinValue => float.MinValue,
                _ => (float)value
            };
        }
    }
}
