#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a decibel value to a 0..1 slider position.
    /// </summary>
    /// <remarks>
    /// The other direction of <see cref="AudioLinearToDecibelConverter"/>, for restoring a saved
    /// mixer value onto a slider.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Audio Decibel To Linear", Tooltip = "Converts a decibel value to a 0..1 slider position")]
    public sealed class AudioDecibelToLinearConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The decibel value that maps to silence.")]
        [SerializeField] private float _minDecibels = -80f;

        [Tooltip("The decibel value that maps to full volume.")]
        [SerializeField] private float _maxDecibels;

        [NonSerialized] private AudioLinearToDecibelConverter? _inverse;

        /// <remarks>Default: over -80..0 dB.</remarks>
        public AudioDecibelToLinearConverter() { }

        /// <param name="minDecibels">The decibel value that maps to silence.</param>
        /// <param name="maxDecibels">The decibel value that maps to full volume.</param>
        public AudioDecibelToLinearConverter(float minDecibels, float maxDecibels = 0f)
        {
            _minDecibels = minDecibels;
            _maxDecibels = maxDecibels;
        }

        /// <summary>
        /// Converts the specified decibel value to a slider position.
        /// </summary>
        /// <param name="value">The decibel value.</param>
        /// <returns>The 0..1 slider position.</returns>
        public float Convert(float value) => Inverse.ConvertBack(value);

        /// <summary>
        /// Converts a slider position back to decibels.
        /// </summary>
        /// <param name="value">The 0..1 slider position.</param>
        /// <returns>The decibel value.</returns>
        public float ConvertBack(float value) => Inverse.Convert(value);

        // Sharing one implementation of the curve keeps the two converters from drifting apart.
        private AudioLinearToDecibelConverter Inverse =>
            _inverse ??= new AudioLinearToDecibelConverter(_minDecibels, _maxDecibels);
    }
}
