#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes one authored color into the chosen states of a <see cref="ColorBlock"/>.
    /// </summary>
    /// <remarks>
    /// The states are a mask rather than a single choice, so one converter can pin several states
    /// to the same color.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Color Block State",
        Tooltip = "Writes one authored color into the chosen states of a ColorBlock")]
    public sealed class ColorBlockStateConverter : IConverter<ColorBlock, ColorBlock>
    {
        [Tooltip("Which states the color is written into. The rest pass through untouched.")]
        [SerializeField] private SelectableStates _states = SelectableStates.Disabled;

        [Tooltip("The color written into the chosen states.")]
        [SerializeField] private Color _color = Color.gray;

        /// <remarks>Default: gray into <see cref="SelectableStates.Disabled"/> alone.</remarks>
        public ColorBlockStateConverter() { }

        /// <param name="states">
        /// Which states the color is written into. The rest pass through untouched.
        /// </param>
        /// <param name="color">The color written into the chosen states.</param>
        public ColorBlockStateConverter(SelectableStates states, Color color)
        {
            _states = states;
            _color = color;
        }

        /// <summary>
        /// Writes the authored color into the chosen states of the specified block.
        /// </summary>
        /// <param name="value">The block to override.</param>
        /// <returns>The block, with the states outside the mask unchanged.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            if (Writes(SelectableStates.Normal)) value.normalColor = _color;
            if (Writes(SelectableStates.Highlighted)) value.highlightedColor = _color;
            if (Writes(SelectableStates.Pressed)) value.pressedColor = _color;
            if (Writes(SelectableStates.Selected)) value.selectedColor = _color;
            if (Writes(SelectableStates.Disabled)) value.disabledColor = _color;

            return value;
        }

        private bool Writes(SelectableStates state) =>
            (_states & state) != 0;
    }
}
