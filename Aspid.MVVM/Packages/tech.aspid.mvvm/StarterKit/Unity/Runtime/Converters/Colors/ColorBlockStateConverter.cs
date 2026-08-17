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
    /// Writes one authored colour into the chosen states of a <see cref="ColorBlock"/>.
    /// </summary>
    /// <remarks>
    /// Chained after <see cref="ColorToColorBlockConverter"/> it corrects the one state whose derived
    /// value was wrong. The state is a mask rather than a single choice, so one converter can pin normal
    /// and selected to the same colour.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Block State", Tooltip = "Writes one authored colour into the chosen states of a ColorBlock")]
    public sealed class ColorBlockStateConverter : IConverterColorBlock
    {
        [Tooltip("Which states the colour is written into. The rest pass through untouched.")]
        [SerializeField] private SelectableStates _states = SelectableStates.Disabled;

        [Tooltip("The colour written into the chosen states.")]
        [SerializeField] private Color _color = Color.gray;

        /// <remarks>Default: overriding the disabled state.</remarks>
        public ColorBlockStateConverter() { }

        /// <param name="states">Which states the colour is written into.</param>
        /// <param name="color">The colour written into the chosen states.</param>
        public ColorBlockStateConverter(SelectableStates states, Color color)
        {
            _states = states;
            _color = color;
        }

        /// <summary>
        /// Writes the authored colour into the chosen states of the specified block.
        /// </summary>
        /// <param name="value">The block to override.</param>
        /// <returns>The block, with the states outside the mask unchanged.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            if (_states.HasFlag(SelectableStates.Normal)) value.normalColor = _color;
            if (_states.HasFlag(SelectableStates.Highlighted)) value.highlightedColor = _color;
            if (_states.HasFlag(SelectableStates.Pressed)) value.pressedColor = _color;
            if (_states.HasFlag(SelectableStates.Selected)) value.selectedColor = _color;
            if (_states.HasFlag(SelectableStates.Disabled)) value.disabledColor = _color;

            return value;
        }
    }
}
