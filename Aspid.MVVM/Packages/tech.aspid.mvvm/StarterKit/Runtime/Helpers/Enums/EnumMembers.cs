#nullable enable
using System;
using UnityEngine;
using System.ComponentModel;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
// ReSharper disable StaticMemberInGenericType
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks up the declared members of <typeparamref name="TEnum"/> by position, number and label.
    /// </summary>
    /// <typeparam name="TEnum">The enum type described.</typeparam>
    /// <remarks>Static initializers run in declaration order, so the fields below stay in this order.</remarks>
    internal static class EnumMembers<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// The declared member names, in the same order as <see cref="EnumBits{TEnum}.Values"/>.
        /// </summary>
        internal static readonly string[] Names = Enum.GetNames(typeof(TEnum));

        private static readonly ulong _underlyingMask = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(TEnum))) switch
        {
            TypeCode.SByte or TypeCode.Byte => byte.MaxValue,
            TypeCode.Int16 or TypeCode.UInt16 => ushort.MaxValue,
            TypeCode.Int32 or TypeCode.UInt32 => uint.MaxValue,
            _ => ulong.MaxValue,
        };

        private static readonly ulong[] _bits = BuildBits();
        private static readonly ulong? _flagMask = BuildFlagMask();

        private static string[]? _inspectorNames;
        private static string[]? _descriptions;

        /// <summary>
        /// Finds where a value sits in <see cref="EnumBits{TEnum}.Values"/>.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        /// <returns>Its position, or -1 when it is not a declared member.</returns>
        internal static int IndexOf(TEnum value)
        {
            var values = EnumBits<TEnum>.Values;
            var comparer = EqualityComparer<TEnum>.Default;

            for (var i = 0; i < values.Length; i++)
                if (comparer.Equals(values[i], value)) return i;

            return -1;
        }

        /// <summary>
        /// Reads the member at a position, if the position is inside the enum.
        /// </summary>
        /// <param name="index">The position to read.</param>
        /// <param name="member">The member at that position, or <see langword="default"/>.</param>
        /// <returns><see langword="true"/> if the position is inside the enum; otherwise, <see langword="false"/>.</returns>
        internal static bool TryAt(int index, out TEnum member)
        {
            var values = EnumBits<TEnum>.Values;

            if (index >= 0 && index < values.Length)
            {
                member = values[index];
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

            for (var i = 0; i < _bits.Length; i++)
                if (_bits[i] == bits) return EnumBits<TEnum>.Values[i];

            return _flagMask is { } mask && (bits & ~mask) == 0
                ? (TEnum)Enum.ToObject(typeof(TEnum), value)
                : fallback;
        }

        /// <summary>
        /// Reads the label of the member at a position, if the source is declared.
        /// </summary>
        /// <param name="index">The position of the member, as <see cref="IndexOf"/> reports it.</param>
        /// <param name="source">Where the label comes from. <see cref="EnumNameSource.Raw"/> is not supported.</param>
        /// <param name="label">The label, or the member name when the attribute is absent or blank.</param>
        /// <returns><see langword="true"/> if <paramref name="source"/> is declared; otherwise, <see langword="false"/>.</returns>
        internal static bool TryLabel(int index, EnumNameSource source, out string label)
        {
            switch (source)
            {
                case EnumNameSource.Name:
                    label = Names[index];
                    return true;

                case EnumNameSource.InspectorName:
                    label = (_inspectorNames ??= BuildLabels(source))[index];
                    return true;

                case EnumNameSource.Description:
                    label = (_descriptions ??= BuildLabels(source))[index];
                    return true;

                default:
                    label = Names[index];
                    return false;
            }
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

                var text = attributes[0] switch
                {
                    InspectorNameAttribute inspector => inspector.displayName,
                    DescriptionAttribute description => description.Description,
                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(text)) labels[i] = text;
            }

            return labels;
        }

        private static ulong[] BuildBits()
        {
            var source = EnumBits<TEnum>.Bits;
            var bits = new ulong[source.Length];

            for (var i = 0; i < bits.Length; i++)
                bits[i] = source[i] & _underlyingMask;

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

        // A negative number is masked to the underlying width; a positive one must not be, or 456 into a byte enum would wrap to 200.
        private static ulong ToBits(long value) => value < 0
            ? unchecked((ulong)value) & _underlyingMask
            : (ulong)value;
    }
}
