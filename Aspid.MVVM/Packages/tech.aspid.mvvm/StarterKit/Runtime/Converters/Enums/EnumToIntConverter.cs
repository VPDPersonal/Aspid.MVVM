using Aspid.FastTools.Types;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts an enum value to its underlying integer.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being converted.</typeparam>
    /// <remarks>
    /// A dropdown's selected index is an <see cref="int"/>, so binding one to an enum property took
    /// a conversion the ViewModel had to expose itself.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum To Int", Tooltip = "Converts an enum value to its underlying integer")]
    public sealed class EnumToIntConverter<TEnum> : ITwoWayConverter<TEnum, int>
        where TEnum : struct, Enum
    {
        /// <summary>
        /// Converts the specified enum value to its underlying integer.
        /// </summary>
        /// <param name="value">The enum value to convert.</param>
        /// <returns>The underlying integer.</returns>
        public int Convert(TEnum value) => System.Convert.ToInt32(value);

        /// <summary>
        /// Converts an integer back to the enum value it represents.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>The enum value, which need not be a declared member.</returns>
        public TEnum ConvertBack(int value) => (TEnum)Enum.ToObject(typeof(TEnum), value);
    }
}
