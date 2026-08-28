using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides parsing helpers for the string-to-number caster binders.
    /// </summary>
    /// <remarks>
    /// The user's own culture is tried first, the invariant form second, so both a typed string and one produced
    /// by code parse.
    /// <para/>
    /// Group separators are not accepted: a field that has to take grouped input needs a converter that says so.
    /// </remarks>
    public static class StringNumberParse
    {
        /// <summary>
        /// Attempts to parse <paramref name="value"/> as an <see langword="int"/>.
        /// </summary>
        /// <param name="value">The string to parse, or <see langword="null"/>.</param>
        /// <param name="result">The parsed value if parsing succeeded; otherwise <c>0</c>.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> was parsed; otherwise, <see langword="false"/>.</returns>
        public static bool TryInt(string? value, out int result)
        {
            const NumberStyles styles = NumberStyles.Integer;

            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            return int.TryParse(value, styles, CultureInfo.CurrentCulture, out result)
                || int.TryParse(value, styles, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Attempts to parse <paramref name="value"/> as a <see langword="float"/>.
        /// </summary>
        /// <param name="value">The string to parse, or <see langword="null"/>.</param>
        /// <param name="result">The parsed value if parsing succeeded; otherwise <c>0</c>.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> was parsed; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// A non-finite result is refused even when the text parses: <c>NaN</c> and <c>Infinity</c> are words float
        /// parsing accepts, and a binder that forwarded one would push it into a clamp that cannot stop it — every
        /// comparison against <c>NaN</c> is false.
        /// </remarks>
        public static bool TryFloat(string? value, out float result)
        {
            const NumberStyles styles = NumberStyles.Float;

            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0f;
                return false;
            }

            var parsed = float.TryParse(value, styles, CultureInfo.CurrentCulture, out result)
                || float.TryParse(value, styles, CultureInfo.InvariantCulture, out result);

            // BinderMath lives in the Unity assembly; this file does not reference it, so the check is inlined.
            if (parsed && !float.IsNaN(result) && !float.IsInfinity(result)) return true;

            result = 0f;
            return false;
        }
    }
}
