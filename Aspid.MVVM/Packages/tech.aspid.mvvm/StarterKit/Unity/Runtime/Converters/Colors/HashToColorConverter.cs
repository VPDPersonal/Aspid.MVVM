#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Derives a stable colour from a string.
    /// </summary>
    /// <remarks>
    /// Per-player colours in a lobby or chat without the server sending any, and the same name always
    /// produces the same colour.
    /// </remarks>
    [Serializable]
    public sealed class HashToColorConverter : IConverter<string?, Color>
    {
        [Tooltip("The saturation of the produced colour.")]
        [SerializeField, Range(0f, 1f)] private float _saturation = 0.6f;

        [Tooltip("The brightness of the produced colour.")]
        [SerializeField, Range(0f, 1f)] private float _value = 0.9f;

        [Tooltip("Used for a null or empty string.")]
        [SerializeField] private Color _fallback = Color.gray;

        public HashToColorConverter() { }

        /// <summary>
        /// Derives a colour from the specified string.
        /// </summary>
        /// <param name="value">The string to hash.</param>
        /// <returns>The derived colour, or the fallback for a blank string.</returns>
        public Color Convert(string? value)
        {
            if (string.IsNullOrEmpty(value)) return _fallback;

            // FNV-1a: string.GetHashCode is randomised per process in modern runtimes, so the same
            // name would take a different colour on every launch.
            var hash = 2166136261u;
            foreach (var character in value!)
            {
                hash ^= character;
                hash *= 16777619u;
            }

            return Color.HSVToRGB(hash % 360u / 360f, _saturation, _value);
        }
    }
}
