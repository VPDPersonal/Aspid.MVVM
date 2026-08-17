#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 slider position to the decibels an <see cref="UnityEngine.Audio.AudioMixer"/> expects.
    /// </summary>
    /// <remarks>
    /// A volume slider wired straight to a mixer sounds wrong: the mixer is logarithmic, so the top
    /// tenth of the slider carries most of the audible change and the bottom half does almost
    /// nothing. This is the conversion that makes a linear slider sound linear.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Audio Linear To Decibel", Tooltip = "Converts a 0..1 slider position to the decibels an AudioMixer expects")]
    public sealed class AudioLinearToDecibelConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The decibel value silence maps to.")]
        [SerializeField] private float _minDecibels = -80f;

        [Tooltip("The decibel value full volume maps to.")]
        [SerializeField] private float _maxDecibels;

        [Tooltip("Slider positions at or below this are treated as silence.")]
        [SerializeField] private float _silenceThreshold = 0.0001f;

        /// <remarks>Default: over -80..0 dB.</remarks>
        public AudioLinearToDecibelConverter() { }

        /// <param name="minDecibels">The decibel value silence maps to.</param>
        /// <param name="maxDecibels">The decibel value full volume maps to.</param>
        public AudioLinearToDecibelConverter(float minDecibels, float maxDecibels = 0f)
        {
            _minDecibels = minDecibels;
            _maxDecibels = maxDecibels;
        }

        /// <summary>
        /// Converts the specified slider position to decibels.
        /// </summary>
        /// <param name="value">The 0..1 slider position.</param>
        /// <returns>The decibel value.</returns>
        public float Convert(float value)
        {
            var normalized = Mathf.Clamp01(value);
            
            return normalized <= _silenceThreshold 
                ? _minDecibels 
                : Mathf.Clamp(Mathf.Log10(normalized) * 20f + _maxDecibels, _minDecibels, _maxDecibels);
        }

        /// <summary>
        /// Converts a decibel value back to a slider position.
        /// </summary>
        /// <param name="value">The decibel value.</param>
        /// <returns>The 0..1 slider position.</returns>
        public float ConvertBack(float value)
        {
            return value <= _minDecibels 
                ? 0f
                : Mathf.Clamp01(Mathf.Pow(10f, (value - _maxDecibels) / 20f));
        }
    }
}
