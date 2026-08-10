#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Changes the alpha of a colour, leaving its hue alone.
    /// </summary>
    /// <remarks>
    /// Fading a single element without a <see cref="CanvasGroup"/>, which fades everything under it.
    /// This is the most common edit anyone makes to a bound colour, and until now the colour picker
    /// on every binder was empty.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Alpha", Tooltip = "Changes the alpha of a colour, leaving its hue alone")]
    public sealed class ColorAlphaConverter : IConverterColor
    {
        [Tooltip("The alpha applied to the colour.")]
        [SerializeField, Range(0f, 1f)] private float _alpha = 1f;

        [Tooltip("How the alpha is applied.")]
        [SerializeField] private AlphaMode _mode = AlphaMode.Set;

        /// <remarks>Default: at full opacity.</remarks>
        public ColorAlphaConverter() { }

        /// <param name="alpha">The alpha applied to the colour.</param>
        /// <param name="mode">How the alpha is applied.</param>
        public ColorAlphaConverter(float alpha, AlphaMode mode = AlphaMode.Set)
        {
            _alpha = alpha;
            _mode = mode;
        }

        /// <summary>
        /// Applies the configured alpha to the specified colour.
        /// </summary>
        /// <param name="value">The colour to adjust.</param>
        /// <returns>The colour with its alpha changed.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public Color Convert(Color value) => Apply(value, _alpha, _mode);

        // Static because ColorBlockAlphaConverter needs the same arithmetic for five colours on every
        // push, and reaching it through an instance meant constructing one converter per notification.
        internal static Color Apply(Color value, float alpha, AlphaMode mode)
        {
            value.a = mode switch
            {
                AlphaMode.Set => alpha,
                AlphaMode.Multiply => Mathf.Clamp01(value.a * alpha),
                AlphaMode.Add => Mathf.Clamp01(value.a + alpha),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            return value;
        }
    }
}
