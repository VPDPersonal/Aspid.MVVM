using Aspid.FastTools.Types;
using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps an enum value to an authored value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being mapped from.</typeparam>
    /// <typeparam name="T">The type being mapped to.</typeparam>
    /// <remarks>
    /// The <c>Enum</c> binder family holds this map in a binder subclass, which means the map cannot
    /// be reused between the icon that shows a state and the colour that tints it. As a converter it
    /// is data, so it can be shared — and as a <see cref="ConverterAsset{TFrom, TTo}"/> it can be
    /// shared across scenes.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Enum", Name = "Enum To Value", Tooltip = "Maps an enum value to an authored value")]
    public sealed class EnumToValueConverter<TEnum, T> : IConverter<TEnum, T>
        where TEnum : struct, Enum
    {
        [Tooltip("The value returned for each enum member. Members not listed use the fallback.")]
        [SerializeField] private Entry[] _map = Array.Empty<Entry>();

        [Tooltip("Returned for an enum member the map does not list.")]
        [SerializeField] private T _fallback = default!;

        public EnumToValueConverter() { }

        /// <param name="map">The value returned for each enum member.</param>
        /// <param name="fallback">Returned for a member <paramref name="map"/> does not list.</param>
        public EnumToValueConverter(Entry[]? map, T fallback = default!)
        {
            _map = map ?? Array.Empty<Entry>();
            _fallback = fallback;
        }

        /// <summary>
        /// Looks the specified enum value up in the map.
        /// </summary>
        /// <param name="value">The enum value to look up.</param>
        /// <returns>The mapped value, or the fallback when the map does not list it.</returns>
        public T Convert(TEnum value)
        {
            if (_map is null) return _fallback;

            // A linear scan beats a dictionary here: these maps are a handful of entries, and a
            // dictionary would have to be rebuilt after every deserialization anyway.
            for (var i = 0; i < _map.Length; i++)
                if (EqualityComparer<TEnum>.Default.Equals(_map[i].Key, value))
                    return _map[i].Value;

            return _fallback;
        }

        /// <summary>
        /// One entry of an <see cref="EnumToValueConverter{TEnum, T}"/> map.
        /// </summary>
        [Serializable]
        public struct Entry
        {
            /// <summary>
            /// The enum value this entry matches.
            /// </summary>
            [Tooltip("The enum value this entry matches.")]
            public TEnum Key;

            /// <summary>
            /// The value returned for <see cref="Key"/>.
            /// </summary>
            [Tooltip("The value returned for the key.")]
            public T Value;
        }
    }
}
