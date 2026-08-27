using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides parsing helpers for the string-to-enum caster binders.
    /// </summary>
    public static class EnumCasterParse
    {
        /// <summary>
        /// Attempts to parse <paramref name="value"/> as the name of a <typeparamref name="TEnum"/> member.
        /// </summary>
        /// <typeparam name="TEnum">The enum type the string is parsed into.</typeparam>
        /// <param name="value">The string to parse, or <see langword="null"/>.</param>
        /// <param name="result">The parsed value if parsing succeeded; otherwise the enum's default.</param>
        /// <returns><see langword="true"/> if <paramref name="value"/> named a member; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// Matching is case-insensitive, because a value that came from text rarely matches the C# casing. A numeric
        /// string is refused: <see cref="Enum.TryParse{TEnum}(string, bool, out TEnum)"/> accepts any number,
        /// including one no member has, and an enum holding an undefined value fails later and elsewhere.
        /// </remarks>
        public static bool TryName<TEnum>(string? value, out TEnum result)
            where TEnum : struct, Enum
        {
            result = default;
            if (string.IsNullOrWhiteSpace(value)) return false;

            var text = value!.Trim();
            if (char.IsDigit(text[0]) || text[0] is '-' or '+') return false;

            return Enum.TryParse(text, ignoreCase: true, out result) && Enum.IsDefined(typeof(TEnum), result);
        }
    }
}
