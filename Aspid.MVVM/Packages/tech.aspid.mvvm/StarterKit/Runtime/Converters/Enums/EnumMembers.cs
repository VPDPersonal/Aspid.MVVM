using System;
using UnityEngine;
using System.ComponentModel;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
// ReSharper disable StaticMemberInGenericType
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The metadata about <typeparamref name="TEnum"/> that the enum converters read.
    /// </summary>
    /// <typeparam name="TEnum">The enum type described.</typeparam>
    internal static class EnumMembers<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// The declared members, ordered by unsigned underlying value, so a negative member sorts last.
        /// </summary>
        internal static readonly TEnum[] Values = EnumBits<TEnum>.Values;

        /// <summary>
        /// The declared member names, in the same order as <see cref="Values"/>.
        /// </summary>
        internal static readonly string[] Names = Enum.GetNames(typeof(TEnum));

        // The three below are ordered by what they read: static initializers run in declaration
        // order, so moving one above the field it needs leaves it reading a zero.
        private static readonly ulong _underlyingMask = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(TEnum))) switch
        {
            TypeCode.SByte or TypeCode.Byte => byte.MaxValue,
            TypeCode.Int16 or TypeCode.UInt16 => ushort.MaxValue,
            TypeCode.Int32 or TypeCode.UInt32 => uint.MaxValue,
            _ => ulong.MaxValue,
        };

        // EnumBits keeps a negative member sign-extended; here every pattern is masked to the
        // underlying width, so it compares against the width-masked ToBits(long) below.
        private static readonly ulong[] _bits = BuildBits();

        private static readonly ulong? _flagMask = BuildFlagMask();

        private static string[]? _inspectorNames;
        private static string[]? _descriptions;

        /// <summary>
        /// Finds where a value sits in <see cref="Values"/>.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>Its position, or -1 when it is not a declared member.</returns>
        internal static int IndexOf(TEnum value)
        {
            // EqualityComparer compares two TEnum without boxing; Convert.ToInt64 would box both.
            var comparer = EqualityComparer<TEnum>.Default;

            for (var i = 0; i < Values.Length; i++)
                if (comparer.Equals(Values[i], value)) return i;

            return -1;
        }

        /// <summary>
        /// Reads the member at a position.
        /// </summary>
        /// <param name="index">The position to read.</param>
        /// <param name="fallback">Returned when the position is outside the enum.</param>
        /// <returns>The member at that position, or <paramref name="fallback"/>.</returns>
        internal static TEnum At(int index, TEnum fallback) =>
            TryAt(index, out var member) ? member : fallback;

        /// <summary>
        /// Reads the member at a position, if the position is inside the enum.
        /// </summary>
        /// <param name="index">The position to read.</param>
        /// <param name="member">The member at that position, or <see langword="default"/>.</param>
        /// <returns>
        /// <see langword="true"/> if the position is inside the enum; otherwise, <see langword="false"/>.
        /// </returns>
        internal static bool TryAt(int index, out TEnum member)
        {
            if (index >= 0 && index < Values.Length)
            {
                member = Values[index];
                return true;
            }

            member = default;
            return false;
        }

        /// <summary>
        /// Finds the member a number stands for.
        /// </summary>
        /// <param name="value">The number, read as the enum's underlying one.</param>
        /// <param name="fallback">Returned when the number names no member.</param>
        /// <returns>The member, a combination of declared flags, or <paramref name="fallback"/>.</returns>
        internal static TEnum FromNumber(long value, TEnum fallback)
        {
            var bits = ToBits(value);

            // The cached array keeps the common answer allocation-free.
            for (var i = 0; i < _bits.Length; i++)
                if (_bits[i] == bits) return Values[i];

            // A flag combination is a legal value the member list does not hold; only this
            // path pays for the Enum.ToObject boxing. Both sides are masked to the underlying
            // width, so a positive integer carrying bits above it — 384 into a byte — cannot
            // slip past the check and be truncated in silence.
            return _flagMask is { } mask && (bits & ~mask) == 0
                ? (TEnum)Enum.ToObject(typeof(TEnum), value)
                : fallback;
        }

        /// <summary>
        /// Reads the label of the member at a position.
        /// </summary>
        /// <param name="index">The position of the member, as <see cref="IndexOf"/> reports it.</param>
        /// <param name="source">
        /// Where the label comes from; <see cref="EnumNameSource.Raw"/> never reaches here.
        /// </param>
        /// <param name="reporter">The converter asking, named when the source is undeclared.</param>
        /// <returns>
        /// The label, falling back to the member name when the attribute is absent or blank, or when
        /// the source is undeclared — which is also logged as an error.
        /// </returns>
        internal static string Label(int index, EnumNameSource source, IConverter reporter) => source switch
        {
            EnumNameSource.Name => Names[index],
            EnumNameSource.InspectorName => (_inspectorNames ??= BuildLabels(source))[index],
            EnumNameSource.Description => (_descriptions ??= BuildLabels(source))[index],
            _ => Undeclared(index, source, reporter),
        };

        private static string Undeclared(int index, EnumNameSource source, IConverter reporter)
        {
            reporter.LogError(
                problem: $"the source {source.Describe()} is not a declared {nameof(EnumNameSource)}",
                consequence: "Using the member name.");

            return Names[index];
        }

        private static string[] BuildLabels(EnumNameSource source)
        {
            var type = typeof(TEnum);
            var labels = new string[Names.Length];

            var attribute = source is EnumNameSource.InspectorName
                ? typeof(InspectorNameAttribute)
                : typeof(DescriptionAttribute);

            for (var i = 0; i < Names.Length; i++)
            {
                labels[i] = Names[i];

                var attributes = type.GetField(Names[i])?.GetCustomAttributes(attribute, inherit: false);
                if (attributes is not { Length: > 0 }) continue;

                // The two attributes keep their text under different members and share no base that
                // exposes it.
                var text = attributes[0] switch
                {
                    InspectorNameAttribute inspector => inspector.displayName,
                    DescriptionAttribute description => description.Description,
                    _ => null
                };

                // An attribute written with no text, or only spaces, is a mistake rather than a
                // request for a blank label.
                if (!string.IsNullOrWhiteSpace(text)) labels[i] = text;
            }

            return labels;
        }

        private static ulong[] BuildBits()
        {
            var bits = new ulong[EnumBits<TEnum>.Bits.Length];

            for (var i = 0; i < bits.Length; i++)
                bits[i] = EnumBits<TEnum>.Bits[i] & _underlyingMask;

            return bits;
        }

        private static ulong? BuildFlagMask()
        {
            if (!EnumBits<TEnum>.IsFlags) return null;

            var mask = 0UL;
            foreach (var bit in _bits)
                mask |= bit;

            return mask;
        }

        // The mask makes a negative number read as the unsigned pattern of its own width: -1 into
        // a uint-backed enum means every bit of a uint, not of an ulong, and -128 into a sbyte-backed
        // one is the 0x80 its members hold. A positive value must not be masked — 456 into a
        // byte-backed enum would wrap to 200 and match the wrong member.
        private static ulong ToBits(long value) => value < 0
            ? unchecked((ulong)value) & _underlyingMask
            : (ulong)value;
    }
}
