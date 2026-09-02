using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides parsing helpers for the string-to-number caster binders.
    /// The current culture is tried first, the invariant culture second; group separators are not accepted.
    /// </summary>
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

            return int.TryParse(value, styles, CultureInfo.CurrentCulture, out result)
                || int.TryParse(value, styles, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Attempts to parse <paramref name="value"/> as a finite <see langword="float"/>.
        /// <c>NaN</c> and infinities are rejected.
        /// </summary>
        /// <param name="value">The string to parse, or <see langword="null"/>.</param>
        /// <param name="result">The parsed value if parsing succeeded; otherwise <c>0</c>.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> was parsed; otherwise, <see langword="false"/>.</returns>
        public static bool TryFloat(string? value, out float result)
        {
            const NumberStyles styles = NumberStyles.Float;

            var parsed = float.TryParse(value, styles, CultureInfo.CurrentCulture, out result)
                || float.TryParse(value, styles, CultureInfo.InvariantCulture, out result);

            if (parsed && !float.IsNaN(result) && !float.IsInfinity(result)) return true;

            result = 0f;
            return false;
        }
    }
}
