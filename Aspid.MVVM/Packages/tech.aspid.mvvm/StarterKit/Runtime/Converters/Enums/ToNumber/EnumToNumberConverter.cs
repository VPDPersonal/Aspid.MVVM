#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an enum value to a number and back.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>Read as a <see langword="long"/>: the int overloads saturate, float and double lose precision past their range.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To Number",
        Name = "To Number",
        Tooltip = "Converts an enum value to a number and back")]
    public class EnumToNumberConverter<TEnum> :
        IConverter<TEnum, float>,
        IConverter<TEnum, double>,
        ITwoWayConverter<TEnum, int>,
        ITwoWayConverter<TEnum, long>
        where TEnum : struct, Enum
    {
        [Tooltip("Use the member's position in the enum instead of its underlying value.")]
        [SerializeField] private bool _byIndexNotValue;

        [Tooltip("Returned for an undeclared member. Unused unless by position.")]
        [UsedInModes(BindMode.OneWay, BindMode.TwoWay, BindMode.OneTime)]
        [SerializeField] private int _indexFallback = -1;

        [Tooltip("Returned for a position outside the enum. Unused unless by position.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private TEnum _fallback;

        /// <remarks>Default: reading the underlying value.</remarks>
        public EnumToNumberConverter() { }

        /// <param name="byIndexNotValue">
        /// When <see langword="true"/>, uses the member's position in the enum instead of its
        /// underlying value.
        /// </param>
        /// <param name="fallback">
        /// Returned for a position outside the enum. Unused while the position mode is off. When
        /// omitted, <see langword="default"/>.
        /// </param>
        /// <param name="indexFallback">
        /// Returned for a value that is not a declared member. Unused while the position mode is
        /// off. When omitted, <c>-1</c>.
        /// </param>
        public EnumToNumberConverter(
            bool byIndexNotValue,
            TEnum? fallback = null,
            int? indexFallback = null)
        {
            _byIndexNotValue = byIndexNotValue;
            _fallback = fallback ?? _fallback;
            _indexFallback = indexFallback ?? _indexFallback;
        }

        /// <summary>
        /// Converts the specified enum value to an integer.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>The underlying number or the member's position; the index fallback for an undeclared member. Saturates to <see cref="int"/>.</returns>
        public int Convert(TEnum value)
        {
            var number = Number(value);
            if (number is >= int.MinValue and <= int.MaxValue) return (int)number;

            return this.UseFallback(
                fallback: NumericSaturation.ToInt(number),
                problem: $"the underlying value {number} does not fit in an int");
        }

        long IConverter<TEnum, long>.Convert(TEnum value) => Number(value);

        float IConverter<TEnum, float>.Convert(TEnum value) => Number(value);

        double IConverter<TEnum, double>.Convert(TEnum value) => Number(value);

        /// <summary>
        /// Converts an integer back to the enum value it stands for.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>The enum value, not necessarily a declared member, or the fallback for a position outside the enum.</returns>
        public TEnum ConvertBack(int value)
        {
            if (!_byIndexNotValue) return (TEnum)Enum.ToObject(typeof(TEnum), value);
            if (EnumMembers<TEnum>.TryAt(value, out var member)) return member;

            return this.UseFallback(
                fallback: _fallback,
                problem: value.Expected($"a position inside {typeof(TEnum).Name}"));
        }

        TEnum ITwoWayConverter<TEnum, long>.ConvertBack(long value) => _byIndexNotValue
            ? ConvertBack(NumericSaturation.ToInt(value))
            : EnumBits<TEnum>.FromBits(unchecked((ulong)value));

        private long Number(TEnum value)
        {
            if (_byIndexNotValue)
            {
                var index = EnumMembers<TEnum>.IndexOf(value);
                if (index >= 0) return index;

                return this.UseFallback(
                    fallback: (long)_indexFallback,
                    problem: value.Expected($"a declared member of {typeof(TEnum).Name}"));
            }

            var bits = EnumBits<TEnum>.BitsOf(value);

            if (EnumBits<TEnum>.IsUnsigned && bits > long.MaxValue)
            {
                return this.UseFallback(
                    fallback: long.MaxValue,
                    problem: $"the underlying value {bits} does not fit in a long");
            }

            return unchecked((long)bits);
        }
    }
}
