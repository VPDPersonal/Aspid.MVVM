#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads an enum member out of text.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being read.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Enum",
        Name = "Parse Enum",
        Tooltip = "Reads an enum member out of text")]
    public class StringToEnumConverter<TEnum> : ITwoWayConverter<string?, TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("Match member names without regard to case.")]
        [SerializeField] private bool _ignoreCase = true;

        [Tooltip("Returned when the text names no member.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private TEnum _fallback;

        // ReSharper disable once StaticMemberInGenericType
        private static readonly ulong _declaredFlags = DeclaredFlags();

        /// <remarks>Default: falling back to <see langword="default"/>.</remarks>
        public StringToEnumConverter() { }

        /// <param name="fallback">Returned when the text names no member. When omitted, <see langword="default"/>.</param>
        /// <param name="ignoreCase">Whether to match without regard to case.</param>
        public StringToEnumConverter(
            TEnum? fallback = null,
            bool ignoreCase = true)
        {
            _ignoreCase = ignoreCase;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Reads an enum member out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The member, a combination of declared flags for a flags enum, or the fallback when the text names none.</returns>
        public TEnum Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return _fallback;

            return Enum.TryParse<TEnum>(value, _ignoreCase, out var parsed) && IsDeclared(parsed)
                ? parsed
                : this.UseFallback(
                    fallback: _fallback,
                    problem: value.Expected($"a member of {typeof(TEnum).Name}"));
        }

        /// <summary>
        /// Writes the specified member as text.
        /// </summary>
        /// <param name="value">The member to write.</param>
        /// <returns>Its name, or the comma-separated names of its flags. An undeclared value writes as its number.</returns>
        public string ConvertBack(TEnum value) =>
            value.ToString();

        private static bool IsDeclared(TEnum value) => EnumBits<TEnum>.IsFlags
            ? (EnumBits<TEnum>.BitsOf(value) & ~_declaredFlags) == 0
            : EnumMembers<TEnum>.IndexOf(value) >= 0;

        private static ulong DeclaredFlags()
        {
            var mask = 0UL;
            foreach (var bits in EnumBits<TEnum>.Bits)
                mask |= bits;

            return mask;
        }
    }
}
