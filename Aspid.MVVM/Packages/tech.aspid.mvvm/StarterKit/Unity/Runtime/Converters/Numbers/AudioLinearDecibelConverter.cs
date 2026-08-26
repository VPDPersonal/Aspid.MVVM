#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 slider position to the decibels an <see cref="UnityEngine.Audio.AudioMixer"/>
    /// expects, or the other way around.
    /// </summary>
    /// <remarks>
    /// The mixer's attenuation is logarithmic, so the mapping is a log curve rather than a lerp.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Linear To Decibel",
        Tooltip = "Converts a 0..1 slider position to the decibels an AudioMixer expects, or the other way around")]
    public sealed class AudioLinearDecibelConverter :
        ITwoWayConverter<float, float>,
        ITwoWayConverter<double, double>
    {
        // The decibels silence and full volume map to when the authored pair is not a range.
        private const float DefaultMinDecibels = -80f;
        private const float DefaultMaxDecibels = 0f;

        [Tooltip("The decibel value silence maps to. It must be below the value for full volume; a " +
            "pair that is not a range is reported as an error and -80..0 dB is used instead.")]
        [SerializeField] private float _minDecibels = DefaultMinDecibels;

        [Tooltip("The decibel value full volume maps to. It must be above the value for silence; a " +
            "pair that is not a range is reported as an error and -80..0 dB is used instead.")]
        [SerializeField] private float _maxDecibels = DefaultMaxDecibels;

        [Tooltip("Slider positions at or below this are treated as silence.")]
        [SerializeField] [Range(0f, 1f)] private float _silenceThreshold = 0.0001f;

        [Tooltip("Convert decibels to a slider position instead.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>Default: slider position to decibels, over -80..0 dB.</remarks>
        public AudioLinearDecibelConverter() { }

        /// <param name="isInvert">If <see langword="true"/>, converts decibels to a slider position instead.</param>
        public AudioLinearDecibelConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <param name="minDecibels">
        /// The decibel value silence maps to. It must be below <paramref name="maxDecibels"/>; a pair
        /// that is not a range is reported as an error and -80..0 dB is used instead.
        /// </param>
        /// <param name="maxDecibels">
        /// The decibel value full volume maps to. It must be above <paramref name="minDecibels"/>.
        /// </param>
        /// <param name="isInvert">If <see langword="true"/>, converts decibels to a slider position instead.</param>
        public AudioLinearDecibelConverter(float minDecibels, float maxDecibels = DefaultMaxDecibels, bool isInvert = false)
        {
            _minDecibels = minDecibels;
            _maxDecibels = maxDecibels;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts the specified value in the authored direction.
        /// </summary>
        /// <param name="value">The 0..1 slider position, or the decibel value when inverted.</param>
        /// <returns>The decibel value, or the 0..1 slider position when inverted.</returns>
        public float Convert(float value) => _isInvert
            ? ToLinear(value)
            : ToDecibels(value);

        /// <summary>
        /// Converts a value back in the opposite direction.
        /// </summary>
        /// <param name="value">The decibel value, or the 0..1 slider position when inverted.</param>
        /// <returns>The 0..1 slider position, or the decibel value when inverted.</returns>
        public float ConvertBack(float value) => _isInvert
            ? ToDecibels(value)
            : ToLinear(value);

        private float ToDecibels(float value)
        {
            var (min, max) = Range();
            var normalized = Mathf.Clamp01(value);

            // 20·log10 reaches 0 dB at full volume, so the authored maximum is what anchors the
            // curve; the minimum only floors it.
            return normalized <= _silenceThreshold
                ? min
                : Mathf.Clamp(Mathf.Log10(normalized) * 20f + max, min, max);
        }

        private float ToLinear(float value)
        {
            var (min, max) = Range();

            return value <= min
                ? 0f
                : Mathf.Clamp01(Mathf.Pow(10f, (value - max) / 20f));
        }

        // With min at or above max the clamp collapses: every slider position would answer with the
        // same number, so the fader looks wired up and moves nothing.
        private (float Min, float Max) Range()
        {
            if (_minDecibels < _maxDecibels) return (_minDecibels, _maxDecibels);

            this.LogError($"the decibel range is not a range (silence at {_minDecibels} dB is not below full " +
                $"volume at {_maxDecibels} dB)",
                $"Using {DefaultMinDecibels}..{DefaultMaxDecibels} dB instead.");

            return (DefaultMinDecibels, DefaultMaxDecibels);
        }

        // The work is Unity's own float math, so the double width runs through it and carries a
        // float's precision.
        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));

        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            ConvertBack(NumericSaturation.ToFloat(value));
    }
}
