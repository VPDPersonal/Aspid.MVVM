#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Sets how long a <see cref="ColorBlock"/> takes to fade between states.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Color",
        Name = "Color Block Fade Duration",
        Tooltip = "Sets how long a ColorBlock takes to fade between states")]
    public sealed class ColorBlockFadeDurationConverter : IConverter<ColorBlock, ColorBlock>
    {
        [Tooltip("How long a state change takes, in seconds.")]
        [SerializeField] [Min(0f)] private float _fadeDuration = 0.1f;

        /// <remarks>Default: a tenth of a second, the same as a fresh <see cref="Selectable"/>.</remarks>
        public ColorBlockFadeDurationConverter() { }

        /// <param name="fadeDuration">
        /// How long a state change takes, in seconds. A duration that is negative or not a number is
        /// reported as an error and zero is used instead.
        /// </param>
        public ColorBlockFadeDurationConverter(float fadeDuration)
        {
            _fadeDuration = fadeDuration;
        }

        /// <summary>
        /// Sets the fade duration of the specified block.
        /// </summary>
        /// <param name="value">The block to adjust.</param>
        /// <returns>
        /// The adjusted block, or the block with an instant fade when the configured duration is
        /// negative or not a number.
        /// </returns>
        public ColorBlock Convert(ColorBlock value)
        {
            value.fadeDuration = Resolve(this, _fadeDuration);
            return value;
        }

        /// <summary>
        /// Screens an authored fade duration.
        /// </summary>
        /// <param name="reporter">The converter the duration was authored on, named in the report.</param>
        /// <param name="fadeDuration">The authored duration, in seconds.</param>
        /// <returns>The duration, or zero when it is negative or not a number.</returns>
        internal static float Resolve(IConverter reporter, float fadeDuration)
        {
            if (fadeDuration >= 0f) return fadeDuration;

            reporter.LogError(
                problem: $"the fade duration is {fadeDuration.Describe()}, which is not a length of time",
                consequence: "Fading instantly instead.");

            return 0f;
        }
    }
}
