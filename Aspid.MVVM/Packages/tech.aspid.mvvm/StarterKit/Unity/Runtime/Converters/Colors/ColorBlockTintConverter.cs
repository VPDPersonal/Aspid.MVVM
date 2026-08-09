#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tints every colour of a <see cref="ColorBlock"/>.
    /// </summary>
    /// <remarks>Theming a whole button at once — faction colours, a disabled palette.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Block Tint", Tooltip = "Tints every colour of a ColorBlock")]
    public sealed class ColorBlockTintConverter : IConverterColorBlock
    {
        [Tooltip("The colour every state is combined with.")]
        [SerializeField] private Color _tint = Color.white;

        [Tooltip("How the two are combined.")]
        [SerializeField] private ColorBlend _blend = ColorBlend.Multiply;

        [Tooltip("How far towards the tint to move, for the Lerp blend.")]
        [SerializeField, Range(0f, 1f)] private float _amount = 1f;

        public ColorBlockTintConverter() { }

        /// <param name="tint">The colour every state is combined with.</param>
        /// <param name="blend">How the two are combined.</param>
        public ColorBlockTintConverter(Color tint, ColorBlend blend = ColorBlend.Multiply)
        {
            _tint = tint;
            _blend = blend;
        }

        /// <summary>
        /// Tints every state of the specified block.
        /// </summary>
        /// <param name="value">The block to tint.</param>
        /// <returns>The tinted block.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            var tint = new ColorTintConverter(_tint, _blend, _amount);

            value.normalColor = tint.Convert(value.normalColor);
            value.highlightedColor = tint.Convert(value.highlightedColor);
            value.pressedColor = tint.Convert(value.pressedColor);
            value.selectedColor = tint.Convert(value.selectedColor);
            value.disabledColor = tint.Convert(value.disabledColor);

            return value;
        }
    }
}
