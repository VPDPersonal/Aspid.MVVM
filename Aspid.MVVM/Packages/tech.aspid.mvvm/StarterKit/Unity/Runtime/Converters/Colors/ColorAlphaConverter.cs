#nullable enable
using System;
using UnityEngine;

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
        public Color Convert(Color value)
        {
            value.a = _mode switch
            {
                AlphaMode.Set => _alpha,
                AlphaMode.Multiply => Mathf.Clamp01(value.a * _alpha),
                AlphaMode.Add => Mathf.Clamp01(value.a + _alpha),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };

            return value;
        }
    }
}
