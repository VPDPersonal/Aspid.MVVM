#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The bit pattern of every declared member of <typeparamref name="TEnum"/>.
    /// </summary>
    /// <typeparam name="TEnum">The enum type described.</typeparam>
    /// <remarks>
    /// An unconstrained enum has no non-boxing route to its underlying number, so reading the member list
    /// per call would allocate once per member on every push. A static field of a generic type is per
    /// closed type, so each enum pays for the table once, at type load.
    /// </remarks>
    internal static class EnumBits<TEnum>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// The declared members, in the order <c>Enum.GetValues</c> returns them.
        /// </summary>
        internal static readonly TEnum[] Values = (TEnum[])Enum.GetValues(typeof(TEnum));

        /// <summary>
        /// Whether the enum is marked <see cref="FlagsAttribute"/>, and so meant to be read as bits.
        /// </summary>
        internal static readonly bool IsFlags = typeof(TEnum).IsDefined(typeof(FlagsAttribute), inherit: false);

        // Read by BuildBits below. Static initialisers run in declaration order, so moving this under
        // the field that needs it would leave that one built from a false.
        private static readonly bool IsUnsigned = Type.GetTypeCode(Enum.GetUnderlyingType(typeof(TEnum)))
            is TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64;

        /// <summary>
        /// The bit pattern of each member, in the same order as <see cref="Values"/>.
        /// </summary>
        internal static readonly ulong[] Bits = BuildBits();

        /// <summary>
        /// Reads a value as a bit pattern.
        /// </summary>
        /// <param name="value">The value to read.</param>
        /// <returns>Its underlying number, widened to 64 bits.</returns>
        /// <remarks>
        /// The two conversions cover different halves of the range: <c>ToInt64</c> takes every signed
        /// underlying type without overflowing, <c>ToUInt64</c> does the same for the unsigned ones,
        /// where a member past <c>long.MaxValue</c> would make <c>ToInt64</c> throw. Both box,
        /// which is why every caller here reaches this only when its cache has missed.
        /// </remarks>
        internal static ulong BitsOf(TEnum value) => IsUnsigned
            ? System.Convert.ToUInt64(value)
            : unchecked((ulong)System.Convert.ToInt64(value));

        /// <summary>
        /// Builds the value a bit pattern stands for.
        /// </summary>
        /// <param name="bits">The bit pattern.</param>
        /// <returns>
        /// The enum value holding those bits. Bits above the enum's underlying width are dropped, the
        /// same way an assignment in code would drop them.
        /// </returns>
        internal static TEnum FromBits(ulong bits) => (TEnum)Enum.ToObject(typeof(TEnum), bits);

        private static ulong[] BuildBits()
        {
            var bits = new ulong[Values.Length];
            for (var i = 0; i < Values.Length; i++) bits[i] = BitsOf(Values[i]);

            return bits;
        }
    }
}
