#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using UnityEngine.UI;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the alpha of every colour in a <see cref="ColorBlock"/>.
    /// </summary>
    /// <remarks>Dimming a whole interactive element without touching its hues.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Block Alpha", Tooltip = "Changes the alpha of every colour in a ColorBlock")]
    public sealed class ColorBlockAlphaConverter : IConverterColorBlock
    {
        [Tooltip("The alpha applied to every state.")]
        [SerializeField, Range(0f, 1f)] private float _alpha = 1f;

        [Tooltip("How the alpha is applied.")]
        [SerializeField] private AlphaMode _mode = AlphaMode.Multiply;

        /// <remarks>Default: at full opacity.</remarks>
        public ColorBlockAlphaConverter() { }

        /// <param name="alpha">The alpha applied to every state.</param>
        /// <param name="mode">How the alpha is applied.</param>
        public ColorBlockAlphaConverter(float alpha, AlphaMode mode = AlphaMode.Multiply)
        {
            _alpha = alpha;
            _mode = mode;
        }

        /// <summary>
        /// Changes the alpha of every state of the specified block.
        /// </summary>
        /// <param name="value">The block to adjust.</param>
        /// <returns>The adjusted block.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            value.normalColor = Fade(value.normalColor);
            value.highlightedColor = Fade(value.highlightedColor);
            value.pressedColor = Fade(value.pressedColor);
            value.selectedColor = Fade(value.selectedColor);
            value.disabledColor = Fade(value.disabledColor);

            return value;
        }

        // Through the static rather than a ColorAlphaConverter instance: this runs on every push, and
        // an instance per push is an allocation per notification for arithmetic that holds no state.
        private Color Fade(Color color) => ColorAlphaConverter.Apply(color, _alpha, _mode);
    }
}
