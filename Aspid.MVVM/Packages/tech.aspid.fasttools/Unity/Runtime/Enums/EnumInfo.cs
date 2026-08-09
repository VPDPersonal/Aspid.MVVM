#nullable enable
using System;
using Unity.Collections.LowLevel.Unsafe;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Enums
{
    /// <summary>
    /// Numeric conversions for boxed <see cref="Enum"/> values — the runtime-typed counterpart
    /// of <see cref="EnumInfo{TEnum}"/>, used where the enum type is only known at runtime.
    /// </summary>
    internal static class EnumInfo
    {
        /// <summary>
        /// Converts a boxed enum value to its underlying integral value, widened to
        /// <see cref="long"/> with the underlying type's own signedness.
        /// Unboxes directly — no intermediate allocation.
        /// </summary>
        public static long ToInt64(Enum value) => Type.GetTypeCode(value.GetType()) switch
        {
            TypeCode.SByte => (sbyte)(object)value,
            TypeCode.Byte => (byte)(object)value,
            TypeCode.Int16 => (short)(object)value,
            TypeCode.UInt16 => (ushort)(object)value,
            TypeCode.Int32 => (int)(object)value,
            TypeCode.UInt32 => (uint)(object)value,
            TypeCode.Int64 => (long)(object)value,
            TypeCode.UInt64 => unchecked((long)(ulong)(object)value),
            _ => throw new InvalidOperationException($"Unsupported enum underlying type '{Enum.GetUnderlyingType(value.GetType())}'."),
        };
    }

    /// <summary>
    /// Per-enum-type reflection info cached once per closed generic, plus a boxing-free
    /// conversion of <typeparamref name="TEnum"/> values to their underlying integral value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type the info describes.</typeparam>
    internal static class EnumInfo<TEnum> where TEnum : struct, Enum
    {
        /// <summary>
        /// Whether <typeparamref name="TEnum"/> is a <c>[Flags]</c> enum.
        /// </summary>
        public static readonly bool IsFlags = typeof(TEnum).IsDefined(typeof(FlagsAttribute), false);

        private static readonly TypeCode _underlyingTypeCode = Type.GetTypeCode(typeof(TEnum));

        /// <summary>
        /// Reinterprets <paramref name="value"/> as its underlying integral value without boxing,
        /// widened to <see cref="long"/> with the underlying type's own signedness.
        /// </summary>
        public static long ToInt64(TEnum value) => _underlyingTypeCode switch
        {
            TypeCode.SByte => UnsafeUtility.As<TEnum, sbyte>(ref value),
            TypeCode.Byte => UnsafeUtility.As<TEnum, byte>(ref value),
            TypeCode.Int16 => UnsafeUtility.As<TEnum, short>(ref value),
            TypeCode.UInt16 => UnsafeUtility.As<TEnum, ushort>(ref value),
            TypeCode.Int32 => UnsafeUtility.As<TEnum, int>(ref value),
            TypeCode.UInt32 => UnsafeUtility.As<TEnum, uint>(ref value),
            TypeCode.Int64 => UnsafeUtility.As<TEnum, long>(ref value),
            TypeCode.UInt64 => (long)UnsafeUtility.As<TEnum, ulong>(ref value),
            _ => throw new InvalidOperationException($"Unsupported enum underlying type '{Enum.GetUnderlyingType(typeof(TEnum))}'."),
        };
    }
}
