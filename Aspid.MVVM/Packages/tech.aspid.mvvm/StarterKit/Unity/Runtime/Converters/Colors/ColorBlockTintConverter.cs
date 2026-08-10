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
    /// Tints the chosen colours of a <see cref="ColorBlock"/>.
    /// </summary>
    /// <remarks>
    /// Theming a whole button at once — faction colours, a disabled palette.
    /// <para>
    /// The mask is what keeps the disabled colour out of the theme. A faction tint that also
    /// recolours the disabled state makes an unavailable button look like an available one in
    /// another faction's colours, which is the one thing that state exists to say.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Block Tint", Tooltip = "Tints the chosen colours of a ColorBlock")]
    public sealed class ColorBlockTintConverter : IConverterColorBlock
    {
        [Tooltip("The colour the chosen states are combined with.")]
        [SerializeField] private Color _tint = Color.white;

        [Tooltip("How the two are combined.")]
        [SerializeField] private ColorBlend _blend = ColorBlend.Multiply;

        [Tooltip("How far towards the tint to move, for the Lerp blend.")]
        [SerializeField, Range(0f, 1f)] private float _amount = 1f;

        [Tooltip("Which states are tinted. The rest pass through untouched.")]
        [SerializeField] private SelectableStates _states = SelectableStates.All;

        public ColorBlockTintConverter() { }

        /// <param name="tint">The colour the chosen states are combined with.</param>
        /// <param name="blend">How the two are combined.</param>
        /// <param name="states">Which states are tinted.</param>
        public ColorBlockTintConverter(
            Color tint,
            ColorBlend blend = ColorBlend.Multiply,
            SelectableStates states = SelectableStates.All)
        {
            _tint = tint;
            _blend = blend;
            _states = states;
        }

        /// <summary>
        /// Tints the chosen states of the specified block.
        /// </summary>
        /// <param name="value">The block to tint.</param>
        /// <returns>The tinted block, with the states outside the mask unchanged.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            if (_states.HasFlag(SelectableStates.Normal)) value.normalColor = Tint(value.normalColor);
            if (_states.HasFlag(SelectableStates.Highlighted)) value.highlightedColor = Tint(value.highlightedColor);
            if (_states.HasFlag(SelectableStates.Pressed)) value.pressedColor = Tint(value.pressedColor);
            if (_states.HasFlag(SelectableStates.Selected)) value.selectedColor = Tint(value.selectedColor);
            if (_states.HasFlag(SelectableStates.Disabled)) value.disabledColor = Tint(value.disabledColor);

            return value;
        }

        // Through the static rather than a ColorTintConverter instance: this runs on every push, and
        // an instance per push is an allocation per notification for arithmetic that holds no state.
        private Color Tint(Color color) => ColorTintConverter.Blend(color, _tint, _blend, _amount);
    }
}
