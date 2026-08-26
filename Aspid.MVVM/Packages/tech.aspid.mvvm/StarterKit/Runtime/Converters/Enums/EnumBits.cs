using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The bit pattern of every declared member of <typeparamref name="TEnum"/>.
    /// </summary>
    /// <typeparam name="TEnum">The enum type described.</typeparam>
    internal static class EnumBits<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// The declared members, in the order <c>Enum.GetValues</c> returns them.
        /// </summary>
        internal static readonly TEnum[] Values = (TEnum[])Enum.GetValues(typeof(TEnum));

        /// <summary>
        /// Whether the enum is marked <see cref="FlagsAttribute"/>.
        /// </summary>
        internal static readonly bool IsFlags = typeof(TEnum).IsDefined(typeof(FlagsAttribute), inherit: false);

        /// <summary>
        /// Whether the underlying type is unsigned.
        /// </summary>
        // Static initializers run in declaration order, so moving this under the field that needs it
        // would leave that one built from a false.
        // ReSharper disable once StaticMemberInGenericType
        internal static readonly bool IsUnsigned = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(TEnum)))
            is TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64;

        /// <summary>
        /// The bit pattern of each member, in the same order as <see cref="Values"/>.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        internal static readonly ulong[] Bits = BuildBits();

        // The two conversions cover different halves of the range: ToInt64 takes every signed
        // underlying type without overflowing, ToUInt64 the unsigned ones, where a member past
        // long.MaxValue would make ToInt64 throw.
        /// <summary>
        /// Reads a value as a bit pattern.
        /// </summary>
        /// <param name="value">The value to read.</param>
        /// <returns>Its underlying number, widened to 64 bits.</returns>
        /// <remarks>The conversion boxes, so callers cache the result.</remarks>
        internal static ulong BitsOf(TEnum value) => IsUnsigned
            ? Convert.ToUInt64(value)
            : unchecked((ulong)Convert.ToInt64(value));

        /// <summary>
        /// Builds the value a bit pattern stands for.
        /// </summary>
        /// <param name="bits">The bit pattern.</param>
        /// <returns>
        /// The enum value holding those bits; bits above the enum's underlying width are dropped.
        /// </returns>
        internal static TEnum FromBits(ulong bits) =>
            (TEnum)Enum.ToObject(typeof(TEnum), bits);

        private static ulong[] BuildBits()
        {
            var bits = new ulong[Values.Length];

            for (var i = 0; i < Values.Length; i++)
                bits[i] = BitsOf(Values[i]);

            return bits;
        }
    }
}
