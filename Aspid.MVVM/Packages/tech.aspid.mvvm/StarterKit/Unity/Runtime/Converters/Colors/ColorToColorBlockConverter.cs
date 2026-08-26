#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Builds a full <see cref="ColorBlock"/> out of one color.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "To Color Block",
        Tooltip = "Builds a full ColorBlock out of one color")]
    public sealed class ColorToColorBlockConverter : IConverter<Color, ColorBlock>
    {
        [Tooltip("Scales the color for the highlighted state.")]
        [SerializeField] private float _highlightedMultiplier = 1.1f;

        [Tooltip("Scales the color for the pressed state.")]
        [SerializeField] private float _pressedMultiplier = 0.9f;

        [Tooltip("Scales the color for the selected state.")]
        [SerializeField] private float _selectedMultiplier = 1f;

        [Tooltip("Scales the color for the disabled state.")]
        [SerializeField] private float _disabledMultiplier = 0.5f;

        [Tooltip("The alpha of the disabled state.")]
        [SerializeField] [Range(0f, 1f)] private float _disabledAlpha = 0.5f;

        [Tooltip("How long a state change takes, in seconds.")]
        [SerializeField] [Min(0f)] private float _fadeDuration = 0.1f;

        [Tooltip("The overall multiplier UGUI applies on top.")]
        [SerializeField] [Range(1f, 5f)] private float _colorMultiplier = 1f;

        /// <remarks>Default: the state scaling a fresh <see cref="Selectable"/> is authored with.</remarks>
        public ColorToColorBlockConverter() { }

        /// <param name="highlightedMultiplier">Scales the color for the highlighted state.</param>
        /// <param name="pressedMultiplier">Scales the color for the pressed state.</param>
        /// <param name="selectedMultiplier">Scales the color for the selected state.</param>
        /// <param name="disabledMultiplier">Scales the color for the disabled state.</param>
        /// <param name="disabledAlpha">The alpha of the disabled state.</param>
        /// <param name="fadeDuration">
        /// How long a state change takes, in seconds. A duration that is negative or not a number is
        /// reported as an error and zero is used instead.
        /// </param>
        /// <param name="colorMultiplier">
        /// The overall multiplier UGUI applies on top. A value outside 1..5 is reported and held to
        /// that range.
        /// </param>
        public ColorToColorBlockConverter(
            float highlightedMultiplier,
            float pressedMultiplier = 0.9f,
            float selectedMultiplier = 1f,
            float disabledMultiplier = 0.5f,
            float disabledAlpha = 0.5f,
            float fadeDuration = 0.1f,
            float colorMultiplier = 1f)
        {
            _highlightedMultiplier = highlightedMultiplier;
            _pressedMultiplier = pressedMultiplier;
            _selectedMultiplier = selectedMultiplier;
            _disabledMultiplier = disabledMultiplier;
            _disabledAlpha = disabledAlpha;
            _fadeDuration = fadeDuration;
            _colorMultiplier = colorMultiplier;
        }

        /// <summary>
        /// Builds a <see cref="ColorBlock"/> from the specified color.
        /// </summary>
        /// <param name="value">The color the states are derived from.</param>
        /// <returns>
        /// The full block of state colors: the normal state is the bound color as it arrived, the
        /// derived states are held to 0..1. The fade is instant when the configured duration is
        /// negative or not a number, and a multiplier outside 1..5 is held to that range.
        /// </returns>
        public ColorBlock Convert(Color value) => new()
        {
            normalColor = value,
            highlightedColor = Scale(value, _highlightedMultiplier),
            pressedColor = Scale(value, _pressedMultiplier),
            selectedColor = Scale(value, _selectedMultiplier),
            disabledColor = Fade(Scale(value, _disabledMultiplier), _disabledAlpha),
            colorMultiplier = ResolveMultiplier(_colorMultiplier),
            fadeDuration = ColorBlockFadeDurationConverter.Resolve(this, _fadeDuration),
        };

        // The [Range] screens the field but not a constructor argument, and UGUI renders a
        // Selectable black at zero.
        private float ResolveMultiplier(float colorMultiplier)
        {
            // Testing the good case rather than the bad one catches a NaN as well as either end.
            if (colorMultiplier is >= 1f and <= 5f) return colorMultiplier;

            this.LogError(
                $"the color multiplier is {colorMultiplier.Describe()}, which is outside 1..5",
                "Holding it to that range.");

            return colorMultiplier > 5f ? 5f : 1f;
        }

        private static Color Scale(Color color, float multiplier) => new(
            Mathf.Clamp01(color.r * multiplier),
            Mathf.Clamp01(color.g * multiplier),
            Mathf.Clamp01(color.b * multiplier),
            color.a);

        private static Color Fade(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}
