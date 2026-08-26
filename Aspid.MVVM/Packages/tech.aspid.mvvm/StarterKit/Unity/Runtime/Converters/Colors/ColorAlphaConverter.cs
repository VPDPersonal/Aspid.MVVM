#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the alpha of a color, leaving its hue alone.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Alpha",
        Tooltip = "Changes the alpha of a color, leaving its hue alone")]
    public sealed class ColorAlphaConverter : IConverter<Color, Color>
    {
        [Tooltip("The alpha applied to the color. The result is held to 0..1 whichever mode is used.")]
        [SerializeField] [Range(0f, 1f)] private float _alpha = 1f;

        [Tooltip("How the alpha is applied.")]
        [SerializeField] private AlphaMode _mode = AlphaMode.Set;

        /// <remarks>Default: setting the alpha to one, which leaves an opaque color opaque.</remarks>
        public ColorAlphaConverter() { }

        /// <param name="alpha">The alpha applied to the color. The result is held to 0..1 whichever mode is used.</param>
        /// <param name="mode">How the alpha is applied.</param>
        public ColorAlphaConverter(float alpha, AlphaMode mode = AlphaMode.Set)
        {
            _alpha = alpha;
            _mode = mode;
        }

        /// <summary>
        /// Applies the configured alpha to the specified color.
        /// </summary>
        /// <param name="value">The color to adjust.</param>
        /// <returns>
        /// The color with its alpha changed, held to 0..1. A mode that is not a declared
        /// <see cref="AlphaMode"/> value reports an error and the alpha is left as it arrived.
        /// </returns>
        public Color Convert(Color value) => Apply(this, value, _alpha, _mode);

        /// <summary>
        /// Applies an alpha to a color in the specified mode.
        /// </summary>
        /// <param name="reporter">The converter the mode was authored on — the report names it.</param>
        /// <param name="value">The color to adjust.</param>
        /// <param name="alpha">The alpha applied to the color.</param>
        /// <param name="mode">How the alpha is applied.</param>
        /// <returns>
        /// The color with its alpha changed, held to 0..1, or with the alpha as it arrived when
        /// <paramref name="mode"/> is not a declared <see cref="AlphaMode"/> value.
        /// </returns>
        // Shared with ColorBlockAlphaConverter, which fades five colors without allocating an instance.
        internal static Color Apply(IConverter reporter, Color value, float alpha, AlphaMode mode)
        {
            value.a = mode switch
            {
                AlphaMode.Set => Mathf.Clamp01(alpha),
                AlphaMode.Multiply => Mathf.Clamp01(value.a * alpha),
                AlphaMode.Add => Mathf.Clamp01(value.a + alpha),
                _ => Undeclared(reporter, value.a, mode)
            };

            return value;
        }

        // The mode arrives from the calling converter's serialized field, so an undeclared one is a
        // broken asset.
        private static float Undeclared(IConverter reporter, float alpha, AlphaMode mode)
        {
            reporter.LogError($"the mode {mode.Describe()} is not a declared {nameof(AlphaMode)}",
                "Leaving the alpha unchanged.");

            return alpha;
        }
    }
}
