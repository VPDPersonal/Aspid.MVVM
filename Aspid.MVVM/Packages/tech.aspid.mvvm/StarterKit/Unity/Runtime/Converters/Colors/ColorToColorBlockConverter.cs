#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Builds a full <see cref="ColorBlock"/> out of one colour.
    /// </summary>
    /// <remarks>
    /// A <see cref="Selectable"/> keeps five colours and a fade duration; these derive all five from
    /// the one colour that varies, so the ViewModel does not have to model UGUI interaction states.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color To Color Block", Tooltip = "Builds a full  out of one colour")]
    public sealed class ColorToColorBlockConverter : IConverter<Color, ColorBlock>
    {
        [Tooltip("Scales the colour for the highlighted state.")]
        [SerializeField] private float _highlightedMultiplier = 1.1f;

        [Tooltip("Scales the colour for the pressed state.")]
        [SerializeField] private float _pressedMultiplier = 0.9f;

        [Tooltip("Scales the colour for the selected state.")]
        [SerializeField] private float _selectedMultiplier = 1f;

        [Tooltip("Scales the colour for the disabled state.")]
        [SerializeField] private float _disabledMultiplier = 0.5f;

        [Tooltip("The alpha of the disabled state.")]
        [SerializeField, Range(0f, 1f)] private float _disabledAlpha = 0.5f;

        [Tooltip("How long a state change takes.")]
        [SerializeField] private float _fadeDuration = 0.1f;

        [Tooltip("The overall multiplier UGUI applies on top.")]
        [SerializeField] private float _colorMultiplier = 1f;

        /// <remarks>Default: with UGUI-like defaults.</remarks>
        public ColorToColorBlockConverter() { }

        /// <summary>
        /// Builds a <see cref="ColorBlock"/> from the specified colour.
        /// </summary>
        /// <param name="value">The colour the states are derived from.</param>
        /// <returns>The full block of state colours.</returns>
        public ColorBlock Convert(Color value) => new()
        {
            normalColor = value,
            highlightedColor = Scale(value, _highlightedMultiplier),
            pressedColor = Scale(value, _pressedMultiplier),
            selectedColor = Scale(value, _selectedMultiplier),
            disabledColor = Fade(Scale(value, _disabledMultiplier), _disabledAlpha),
            colorMultiplier = _colorMultiplier,
            fadeDuration = _fadeDuration,
        };

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
