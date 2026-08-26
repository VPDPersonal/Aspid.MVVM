using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an enum value to text.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>There is no culture setting: <c>Enum.ToString</c> ignores any format provider.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To String",
        Name = "To String",
        Tooltip = "Converts an enum value to text")]
    public class EnumToStringConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("Where the text comes from.")]
        [SerializeField] private EnumNameSource _source;

        [Tooltip("Returned for a value that is not a declared member, a flag combination included. " +
            "Unused under the Raw source, which writes a combination as its flag names and an undeclared value as its number.")]
        [SerializeField] private string _fallback = string.Empty;

        /// <remarks>Default: the member name as written in code.</remarks>
        public EnumToStringConverter() { }

        /// <param name="source">Where the text comes from.</param>
        /// <param name="fallback">
        /// Returned for a value that is not a declared member, a flag combination included. Unused
        /// under <see cref="EnumNameSource.Raw"/>, which writes a combination as its flag names and
        /// an undeclared value as its number. When omitted, an empty string.
        /// </param>
        public EnumToStringConverter(EnumNameSource source, string? fallback = null)
        {
            _source = source;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Converts the specified enum value to text.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>
        /// The member's text; the fallback for a value that is not a declared member — a flag
        /// combination included — except under <see cref="EnumNameSource.Raw"/>, which writes a
        /// combination as its flag names and an undeclared value as its number.
        /// </returns>
        public string Convert(TEnum value)
        {
            // The only source that can name a flag combination or an undeclared number.
            if (_source is EnumNameSource.Raw)
                return value.ToString();

            var index = EnumMembers<TEnum>.IndexOf(value);
            if (index >= 0)
            {
                return EnumMembers<TEnum>.Label(
                    index: index,
                    source: _source,
                    reporter: this);
            }

            return this.UseFallback(
                fallback: _fallback,
                problem: value.Expected($"a declared member of {typeof(TEnum).Name}"));
        }
    }
}
