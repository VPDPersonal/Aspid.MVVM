#nullable enable
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The metadata about <typeparamref name="TEnum"/> that the enum converters read.
    /// </summary>
    /// <typeparam name="TEnum">The enum type described.</typeparam>
    /// <remarks>
    /// All of it comes from reflection, and a binder pushes on every notification rather than on
    /// every change — read per call, the attribute lookup behind a label that never changes would
    /// allocate an array on every push. A static field of a generic type is per closed type, so each
    /// enum pays for this once, and only for the label source actually in use.
    /// </remarks>
    internal static class EnumMembers<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>The declared members, in the order Enum.GetValues returns them — by UNSIGNED underlying value, so a negative member sorts last rather than first.</summary>
        internal static readonly TEnum[] Values = (TEnum[])Enum.GetValues(typeof(TEnum));

        /// <summary>The declared member names, in the same order as <see cref="Values"/>.</summary>
        internal static readonly string[] Names = Enum.GetNames(typeof(TEnum));

        // The four below are ordered by what they read: static initialisers run in declaration
        // order, so moving one above the field it needs leaves it reading a null or a zero.
        private static readonly bool IsUnsigned = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(TEnum)))
            is TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64;

        private static readonly ulong UnderlyingMask = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(TEnum))) switch
        {
            TypeCode.SByte or TypeCode.Byte => byte.MaxValue,
            TypeCode.Int16 or TypeCode.UInt16 => ushort.MaxValue,
            TypeCode.Int32 or TypeCode.UInt32 => uint.MaxValue,
            _ => ulong.MaxValue,
        };

        private static readonly ulong[] Bits = BuildBits();

        private static readonly ulong? FlagMask = BuildFlagMask();

        private static string[]? _inspectorNames;
        private static string[]? _descriptions;

        /// <summary>
        /// Finds where a value sits in <see cref="Values"/>.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>Its position, or -1 when it is not a declared member.</returns>
        internal static int IndexOf(TEnum value)
        {
            // EqualityComparer compares two TEnum without boxing either; Convert.ToInt64 would box
            // both, once per member, on every push.
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
            index >= 0 && index < Values.Length ? Values[index] : fallback;

        /// <summary>
        /// Finds the member an integer stands for.
        /// </summary>
        /// <param name="value">The integer, read as the enum's underlying number.</param>
        /// <param name="fallback">Returned when the integer names no member.</param>
        /// <returns>The member, a combination of declared flags, or <paramref name="fallback"/>.</returns>
        internal static TEnum FromNumber(int value, TEnum fallback)
        {
            var bits = ToBits(value);

            // Returning the member out of the cached array rather than converting the integer keeps
            // the common answer allocation-free.
            for (var i = 0; i < Bits.Length; i++)
                if (Bits[i] == bits) return Values[i];

            // A flag combination is a legal value the member list does not hold, so it has to be
            // built. Enum.ToObject boxes, which is why only this path pays for it.
            return FlagMask is { } mask && (bits & ~mask) == 0
                ? (TEnum)Enum.ToObject(typeof(TEnum), value)
                : fallback;
        }

        /// <summary>
        /// Reads the label of the member at a position.
        /// </summary>
        /// <param name="index">The position of the member, as <see cref="IndexOf"/> reports it.</param>
        /// <param name="source">Where the label comes from.</param>
        /// <returns>The label, falling back to the member name when the attribute is absent or blank.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the source is not one this reads — <see cref="EnumNameSource.Raw"/> is the
        /// value's own <c>ToString</c> and never reaches here.
        /// </exception>
        internal static string Label(int index, EnumNameSource source) => source switch
        {
            EnumNameSource.Name => Names[index],
            EnumNameSource.InspectorName => (_inspectorNames ??= BuildLabels(source))[index],
            EnumNameSource.Description => (_descriptions ??= BuildLabels(source))[index],
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };

        // Every member is read at once rather than on demand: the reflection costs the same either
        // way, and one array means one lookup can never be the one that pays for it.
        private static string[] BuildLabels(EnumNameSource source)
        {
            var type = typeof(TEnum);
            var labels = new string[Names.Length];

            var attribute = source is EnumNameSource.InspectorName
                ? typeof(InspectorNameAttribute)
                : typeof(System.ComponentModel.DescriptionAttribute);

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
                    System.ComponentModel.DescriptionAttribute description => description.Description,
                    _ => null
                };

                // An attribute written with no text is a mistake, not a request for a blank label.
                if (!string.IsNullOrEmpty(text)) labels[i] = text!;
            }

            return labels;
        }

        private static ulong[] BuildBits()
        {
            var bits = new ulong[Values.Length];
            for (var i = 0; i < Values.Length; i++) bits[i] = ToBits(Values[i]);

            return bits;
        }

        private static ulong? BuildFlagMask()
        {
            if (!typeof(TEnum).IsDefined(typeof(FlagsAttribute), inherit: false)) return null;

            var mask = 0UL;
            for (var i = 0; i < Bits.Length; i++) mask |= Bits[i];

            return mask;
        }

        // An unconstrained enum has no non-boxing route to its underlying number, and the two
        // conversions cover different halves of the range: ToInt64 takes every signed underlying
        // type without overflowing, ToUInt64 does the same for the unsigned ones, where a member
        // past long.MaxValue would make ToInt64 throw. Both run once per member, at type load.
        private static ulong ToBits(TEnum value) => IsUnsigned
            ? System.Convert.ToUInt64(value)
            : unchecked((ulong)System.Convert.ToInt64(value));

        // The same reading for an incoming integer. A signed enum sign-extends, so -1 is the same
        // 64-bit pattern as the member holding -1.
        //
        // The mask exists only so a NEGATIVE integer reads as the unsigned pattern of its own width:
        // -1 into a uint-backed enum means every bit of a uint, not every bit of a ulong. Applying it
        // to a positive value would wrap it instead — 456 into a byte-backed enum would become 200
        // and match a member that names something else entirely, which is exactly the silent cast
        // this converter exists to refuse.
        private static ulong ToBits(int value) => IsUnsigned && value < 0
            ? unchecked((ulong)(long)value) & UnderlyingMask
            : unchecked((ulong)(long)value);
    }
}
