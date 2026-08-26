using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a number to the enum value it stands for.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted to.</typeparam>
    /// <remarks>
    /// A number that names no member is refused rather than cast; a <see cref="FlagsAttribute"/> enum
    /// still accepts any combination of its declared flags. A float or double carrying a fraction names
    /// nothing and is refused with the rest.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Enum",
        Name = "Number To Enum",
        Tooltip = "Converts a number to the enum value it stands for")]
    public class NumberToEnumConverter<TEnum> :
        IConverter<int, TEnum>,
        IConverter<long, TEnum>,
        IConverter<float, TEnum>,
        IConverter<double, TEnum>
        where TEnum : struct, Enum
    {
        [Tooltip("Read the number as a member's position in the enum instead of its underlying value.")]
        [SerializeField] private bool _byIndexNotValue;

        [Tooltip("Returned for a number that names no member, which is also reported as an error.")]
        [SerializeField] private TEnum _fallback;

        // FromNumber reports a refusal by handing back the fallback it was given, which is also a
        // legal answer. Two probes that differ tell the two apart: a resolved number answers with
        // the same member whichever probe is passed, a refused one answers with the probe itself.
        private static readonly TEnum _firstProbe;
        private static readonly TEnum _secondProbe = (TEnum)Enum.ToObject(typeof(TEnum), 1);

        /// <remarks>Default: reading the underlying value.</remarks>
        public NumberToEnumConverter() { }

        /// <param name="byIndexNotValue">
        /// If <see langword="true"/>, reads the number as a member's position in the enum rather
        /// than as an underlying value.
        /// </param>
        /// <param name="fallback">
        /// Returned for a number that names no member, which is also reported as an error.
        /// </param>
        public NumberToEnumConverter(bool byIndexNotValue, TEnum fallback = default)
        {
            _fallback = fallback;
            _byIndexNotValue = byIndexNotValue;
        }

        /// <summary>
        /// Converts the specified number to an enum value.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>
        /// The enum value it stands for. A number that names no member reports an error and returns
        /// the fallback.
        /// </returns>
        public TEnum Convert(int value) => Read(value);

        TEnum IConverter<long, TEnum>.Convert(long value) => Read(value);

        TEnum IConverter<float, TEnum>.Convert(float value) => Whole(value);

        TEnum IConverter<double, TEnum>.Convert(double value) => Whole(value);

        private TEnum Read(long value) => _byIndexNotValue
            ? At(value)
            : FromNumber(value);

        // A fraction sits between two members and names neither, and a value past the long range
        // cannot be read as an underlying number at all. A NaN fails both tests and lands here too.
        private TEnum Whole(double value)
        {
            if (value is >= long.MinValue and <= long.MaxValue && value == Math.Floor(value))
                return Read((long)value);

            return Refuse(
                value: value,
                expected: $"a whole number naming a member of {typeof(TEnum).Name}");
        }

        private TEnum At(long value)
        {
            if (value is >= 0 and < int.MaxValue && EnumMembers<TEnum>.TryAt((int)value, out var member))
                return member;

            return Refuse(
                value: value,
                expected: $"a position between 0 and {EnumMembers<TEnum>.Values.Length - 1}");
        }

        private TEnum FromNumber(long value)
        {
            var comparer = EqualityComparer<TEnum>.Default;
            var resolved = EnumMembers<TEnum>.FromNumber(value, _firstProbe);

            // Only an answer equal to the first probe is in doubt: it is either the refusal or the
            // member that genuinely holds that number, and the second probe separates the two.
            if (!comparer.Equals(resolved, _firstProbe)) return resolved;
            if (!comparer.Equals(EnumMembers<TEnum>.FromNumber(value, _secondProbe), _secondProbe)) return resolved;

            return Refuse(
                value: value,
                expected: $"a number naming a member of {typeof(TEnum).Name}");
        }

        private TEnum Refuse(object value, string expected)
        {
            this.LogError(
                problem: value.Expected(expected),
                consequence: $"Using the fallback {_fallback.Describe()}.");

            return _fallback;
        }
    }
}
