#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Tints the chosen colors of a <see cref="ColorBlock"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Color Block Tint",
        Tooltip = "Tints the chosen colors of a ColorBlock")]
    public sealed class ColorBlockTintConverter : IConverter<ColorBlock, ColorBlock>
    {
        [Tooltip("The color the chosen states are combined with.")]
        [SerializeField] private Color _tint = Color.white;

        [Tooltip("How the two are combined.")]
        [SerializeField] private ColorBlendMode _blend = ColorBlendMode.Multiply;

        [Tooltip("How far toward the tint to move, for the Lerp blend.")]
        [SerializeField] [Range(0f, 1f)] private float _amount = 1f;

        [Tooltip("Which states are tinted. The rest pass through untouched.")]
        [SerializeField] private SelectableStates _states = SelectableStates.All;

        /// <remarks>Default: a multiply by white over every state, which changes nothing.</remarks>
        public ColorBlockTintConverter() { }

        /// <param name="tint">The color the chosen states are combined with.</param>
        /// <param name="blend">How the two are combined.</param>
        /// <param name="states">Which states are tinted. The rest pass through untouched.</param>
        /// <param name="amount">How far toward the tint to move, for <see cref="ColorBlendMode.Lerp"/>.</param>
        public ColorBlockTintConverter(
            Color tint,
            ColorBlendMode blend = ColorBlendMode.Multiply,
            SelectableStates states = SelectableStates.All,
            float amount = 1f)
        {
            _tint = tint;
            _blend = blend;
            _states = states;
            _amount = amount;
        }

        /// <summary>
        /// Tints the chosen states of the specified block.
        /// </summary>
        /// <param name="value">The block to tint.</param>
        /// <returns>
        /// The tinted block, with the states outside the mask unchanged. A blend that is not a
        /// declared <see cref="ColorBlendMode"/> value reports an error and the colors pass through
        /// untinted.
        /// </returns>
        public ColorBlock Convert(ColorBlock value)
        {
            if (Tints(SelectableStates.Normal)) value.normalColor = Tint(value.normalColor);
            if (Tints(SelectableStates.Highlighted)) value.highlightedColor = Tint(value.highlightedColor);
            if (Tints(SelectableStates.Pressed)) value.pressedColor = Tint(value.pressedColor);
            if (Tints(SelectableStates.Selected)) value.selectedColor = Tint(value.selectedColor);
            if (Tints(SelectableStates.Disabled)) value.disabledColor = Tint(value.disabledColor);

            return value;
        }

        private bool Tints(SelectableStates state) =>
            (_states & state) != 0;

        private Color Tint(Color color) =>
            ColorTintConverter.Blend(this, color, _tint, _blend, _amount);
    }
}
