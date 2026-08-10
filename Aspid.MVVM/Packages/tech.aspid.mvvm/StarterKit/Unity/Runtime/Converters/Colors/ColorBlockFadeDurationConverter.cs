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
    /// Sets how long a <see cref="Selectable"/> takes to change state.
    /// </summary>
    /// <remarks>
    /// A reduce-motion accessibility setting bound straight to the fade, which no other converter
    /// reaches without rebuilding the whole block.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Block Fade Duration", Tooltip = "Sets how long a ColorBlock takes to change state")]
    public sealed class ColorBlockFadeDurationConverter : IConverterColorBlock
    {
        [Tooltip("How long a state change takes.")]
        [SerializeField] private float _fadeDuration = 0.1f;

        public ColorBlockFadeDurationConverter() { }

        /// <param name="fadeDuration">How long a state change takes.</param>
        public ColorBlockFadeDurationConverter(float fadeDuration)
        {
            _fadeDuration = fadeDuration;
        }

        /// <summary>
        /// Sets the fade duration of the specified block.
        /// </summary>
        /// <param name="value">The block to adjust.</param>
        /// <returns>The adjusted block.</returns>
        public ColorBlock Convert(ColorBlock value)
        {
            value.fadeDuration = _fadeDuration;
            return value;
        }
    }
}
