#nullable enable
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
    /// A <see cref="Selectable"/> keeps five colours and a fade duration, so binding a theme colour
    /// to a button meant the ViewModel producing all five — teaching it how UGUI models interaction
    /// states. This derives them from the one colour that actually varies.
    /// <para>
    /// <c>IConverterColorBlock</c> has been declared since the first release with no implementation
    /// behind it, so the picker on six binders has always been empty.
    /// </para>
    /// </remarks>
    [Serializable]
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorToColorBlockConverter"/> class with UGUI-like defaults.
        /// </summary>
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
