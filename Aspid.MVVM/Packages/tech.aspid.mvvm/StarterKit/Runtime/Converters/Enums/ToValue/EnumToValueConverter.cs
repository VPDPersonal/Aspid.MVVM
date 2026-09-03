#nullable enable
using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps an enum value to an authored value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being mapped from.</typeparam>
    /// <typeparam name="T">The type being mapped to.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To Value",
        Name = "To Value",
        Tooltip = "Maps an enum value to an authored value")]
    public class EnumToValueConverter<TEnum, T> : DictionaryLookupConverter<TEnum, T>
        where TEnum : struct, Enum
    {
        /// <remarks>Default: an empty map.</remarks>
        public EnumToValueConverter() { }

        /// <param name="map">The value for each member. A duplicate member is reported, its first row wins. The array is copied.</param>
        /// <param name="fallback">Returned for a member <paramref name="map"/> does not list.</param>
        public EnumToValueConverter(
            LookupEntry<TEnum, T?>[]? map,
            T? fallback = default)
            : base(map, fallback) { }
    }
}
