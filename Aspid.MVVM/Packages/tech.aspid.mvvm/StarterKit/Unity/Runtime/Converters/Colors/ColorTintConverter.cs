#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound color with an authored one.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Tint",
        Tooltip = "Combines a bound color with an authored one")]
    public sealed class ColorTintConverter : IConverter<Color, Color>
    {
        [Tooltip("The color the bound one is combined with.")]
        [SerializeField] private Color _tint = Color.white;

        [Tooltip("How the two are combined.")]
        [SerializeField] private ColorBlend _blend = ColorBlend.Multiply;

        [Tooltip("How far toward the tint to move, for the Lerp blend.")]
        [SerializeField] [Range(0f, 1f)] private float _amount = 1f;

        /// <remarks>Default: a multiply by white, which changes nothing.</remarks>
        public ColorTintConverter() { }

        /// <param name="tint">The color the bound one is combined with.</param>
        /// <param name="blend">How the two are combined.</param>
        /// <param name="amount">How far toward the tint to move, for <see cref="ColorBlend.Lerp"/>.</param>
        public ColorTintConverter(Color tint, ColorBlend blend = ColorBlend.Multiply, float amount = 1f)
        {
            _tint = tint;
            _blend = blend;
            _amount = amount;
        }

        /// <summary>
        /// Combines the specified color with the authored tint.
        /// </summary>
        /// <param name="value">The color to tint.</param>
        /// <returns>
        /// The combined color. Its alpha follows the blend: <see cref="ColorBlend.Multiply"/> and
        /// <see cref="ColorBlend.Lerp"/> take the tint's alpha into account, the other two leave the
        /// bound color's alpha alone. A blend that is not a declared <see cref="ColorBlend"/> value
        /// reports an error and the color passes through unchanged.
        /// </returns>
        public Color Convert(Color value) => Blend(this, value, _tint, _blend, _amount);

        /// <summary>
        /// Combines a color with a tint in the specified blend.
        /// </summary>
        /// <param name="reporter">The converter the blend was authored on — the report names it.</param>
        /// <param name="value">The color to tint.</param>
        /// <param name="tint">The color it is combined with.</param>
        /// <param name="blend">How the two are combined.</param>
        /// <param name="amount">How far toward the tint to move, for <see cref="ColorBlend.Lerp"/>.</param>
        /// <returns>
        /// The combined color, or the color unchanged when <paramref name="blend"/> is not a declared
        /// <see cref="ColorBlend"/> value.
        /// </returns>
        // Shared with ColorBlockTintConverter, which tints five colors without allocating an instance.
        internal static Color Blend(IConverter reporter, Color value, Color tint, ColorBlend blend, float amount) => blend switch
        {
            ColorBlend.Multiply => value * tint,
            ColorBlend.Add => new Color(
                Mathf.Clamp01(value.r + tint.r),
                Mathf.Clamp01(value.g + tint.g),
                Mathf.Clamp01(value.b + tint.b),
                value.a),
            ColorBlend.Lerp => Color.Lerp(value, tint, amount),
            ColorBlend.Replace => new Color(tint.r, tint.g, tint.b, value.a),
            _ => Undeclared(reporter, value, blend)
        };

        // The blend arrives from the calling converter's serialized field, so an undeclared one is a
        // broken asset.
        private static Color Undeclared(IConverter reporter, Color value, ColorBlend blend)
        {
            reporter.LogError($"the blend {blend.Describe()} is not a declared {nameof(ColorBlend)}",
                "Returning the color unchanged.");

            return value;
        }
    }
}
