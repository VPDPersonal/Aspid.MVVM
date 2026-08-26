using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps an enum value to an authored value.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being mapped from.</typeparam>
    /// <typeparam name="T">The type being mapped to.</typeparam>
    /// <remarks>
    /// A member listed more than once is answered by its first entry, and the duplicate is logged as
    /// an error.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Enum/To Value",
        Name = "To Value",
        Tooltip = "Maps an enum value to an authored value")]
    public class EnumToValueConverter<TEnum, T> : IConverter<TEnum, T?>
        where TEnum : struct, Enum
    {
        [Tooltip("The value returned for each enum member. " +
            "A member listed twice is answered by its first entry, and the duplicate is logged as an error.")]
        [SerializeField] private Entry[] _map = Array.Empty<Entry>();

        [Tooltip("Returned for an enum member the map does not list.")]
        [SerializeField] private T? _fallback;

        /// <remarks>Default: an empty map.</remarks>
        public EnumToValueConverter() { }

        /// <param name="map">
        /// The value returned for each enum member. A member listed twice is answered by its first
        /// entry, and the duplicate is logged as an error.
        /// </param>
        /// <param name="fallback">Returned for a member <paramref name="map"/> does not list.</param>
        public EnumToValueConverter(Entry[]? map, T? fallback)
        {
            // The array is copied so a caller holding on to it cannot rewrite the map afterward.
            _map = map is null ? Array.Empty<Entry>() : (Entry[])map.Clone();
            _fallback = fallback;
        }

        /// <summary>
        /// Looks the specified enum value up in the map.
        /// </summary>
        /// <param name="value">The enum value to look up.</param>
        /// <returns>The mapped value, or the fallback when the map does not list it.</returns>
        public T? Convert(TEnum value)
        {
            // These maps are a handful of entries; a dictionary would be rebuilt on every deserialization.
            var comparer = EqualityComparer<TEnum>.Default;
            var found = -1;

            for (var i = 0; i < _map.Length; i++)
            {
                if (!comparer.Equals(_map[i].Key, value)) continue;

                if (found < 0)
                {
                    found = i;
                    continue;
                }

                this.LogError(
                    problem: $"{value.Describe()} is listed more than once in the map",
                    consequence: "Using the first entry that names it.");

                break;
            }

            return found < 0
                ? _fallback
                : _map[found].Value;
        }

        /// <summary>
        /// One entry of an <see cref="EnumToValueConverter{TEnum, T}"/> map.
        /// </summary>
        [Serializable]
        public struct Entry
        {
            /// <summary>
            /// Gets the enum value this entry matches.
            /// </summary>
            [field: Tooltip("The enum value this entry matches.")]
            [field: SerializeField] 
            public TEnum Key { get; private set; }

            /// <summary>
            /// Gets the value returned for <see cref="Key"/>.
            /// </summary>
            [field: Tooltip("The value returned for the key.")]
            [field: SerializeField]
            public T Value { get; private set; }

            /// <param name="key">The enum value this entry matches.</param>
            /// <param name="value">The value returned for the key.</param>
            public Entry(TEnum key, T value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
