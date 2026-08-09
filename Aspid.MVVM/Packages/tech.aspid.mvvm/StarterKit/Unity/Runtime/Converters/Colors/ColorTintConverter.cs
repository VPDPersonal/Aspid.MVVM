#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a bound colour with an authored one.
    /// </summary>
    /// <remarks>Team colours, rarity tints, disabled states.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Tint", Tooltip = "Combines a bound colour with an authored one")]
    public sealed class ColorTintConverter : IConverterColor
    {
        [Tooltip("The colour the bound one is combined with.")]
        [SerializeField] private Color _tint = Color.white;

        [Tooltip("How the two are combined.")]
        [SerializeField] private ColorBlend _blend = ColorBlend.Multiply;

        [Tooltip("How far towards the tint to move, for the Lerp blend.")]
        [SerializeField, Range(0f, 1f)] private float _amount = 1f;

        public ColorTintConverter() { }

        /// <param name="tint">The colour the bound one is combined with.</param>
        /// <param name="blend">How the two are combined.</param>
        /// <param name="amount">How far towards the tint to move, for the Lerp blend.</param>
        public ColorTintConverter(Color tint, ColorBlend blend = ColorBlend.Multiply, float amount = 1f)
        {
            _tint = tint;
            _blend = blend;
            _amount = amount;
        }

        /// <summary>
        /// Combines the specified colour with the authored tint.
        /// </summary>
        /// <param name="value">The colour to tint.</param>
        /// <returns>The combined colour.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the blend is not a declared value.</exception>
        public Color Convert(Color value) => _blend switch
        {
            ColorBlend.Multiply => value * _tint,
            ColorBlend.Add => new Color(
                Mathf.Clamp01(value.r + _tint.r),
                Mathf.Clamp01(value.g + _tint.g),
                Mathf.Clamp01(value.b + _tint.b),
                value.a),
            ColorBlend.Lerp => Color.Lerp(value, _tint, _amount),
            ColorBlend.Replace => new Color(_tint.r, _tint.g, _tint.b, value.a),
            _ => throw new ArgumentOutOfRangeException(nameof(_blend), _blend, null)
        };
    }
}
