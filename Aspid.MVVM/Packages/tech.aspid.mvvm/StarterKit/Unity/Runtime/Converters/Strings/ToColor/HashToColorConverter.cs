#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Derives a stable color from a string.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/String/To Color",
        Name = "Hash To Color",
        Tooltip = "Derives a stable color from a string")]
    public sealed class HashToColorConverter : IConverter<string?, Color>
    {
        [Tooltip("The saturation of the produced color.")]
        [SerializeField] [Range(0f, 1f)] private float _saturation = 0.6f;

        [Tooltip("The brightness of the produced color.")]
        [SerializeField] [Range(0f, 1f)] private float _value = 0.9f;

        [Tooltip("Used for a blank string.")]
        [SerializeField] private Color _fallback = Color.gray;

        /// <remarks>Default: a soft, bright color, falling back to gray for a blank string.</remarks>
        public HashToColorConverter() { }

        /// <param name="saturation">The saturation of the produced color.</param>
        /// <param name="value">The brightness of the produced color.</param>
        /// <param name="fallback">Used for a blank string. When omitted, gray.</param>
        public HashToColorConverter(float saturation, float value = 0.9f, Color? fallback = null)
        {
            _value = value;
            _saturation = saturation;
            _fallback = fallback ?? _fallback;
        }

        /// <summary>
        /// Derives a color from the specified string.
        /// </summary>
        /// <param name="value">The string to hash.</param>
        /// <returns>The derived color, or the fallback for a blank string.</returns>
        public Color Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return _fallback;

            // FNV-1a: string.GetHashCode is randomized per process in modern runtimes, so the same
            // name would take a different color on every launch.
            var hash = 2166136261u;

            foreach (var character in value)
            {
                hash ^= character;
                hash *= 16777619u;
            }

            return Color.HSVToRGB(
                hash % 360u / 360f,
                Mathf.Clamp01(_saturation),
                Mathf.Clamp01(_value));
        }
    }
}
