#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Passes a number through an <see cref="AnimationCurve"/>.
    /// </summary>
    /// <remarks>
    /// An arbitrary transfer function edited with Unity's own curve editor, which is a better place
    /// to shape a response than a C# file — and the only converter here a designer can author without
    /// asking for one.
    /// </remarks>
    [Serializable]
    public sealed class AnimationCurveConverter : IConverterFloat
    {
        [Tooltip("The curve the value is passed through.")]
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Tooltip("Map the input range onto the curve's 0..1 domain before evaluating.")]
        [SerializeField] private bool _normalizeInput;

        [Tooltip("The input value that maps to the start of the curve.")]
        [SerializeField] private float _inputMin;

        [Tooltip("The input value that maps to the end of the curve.")]
        [SerializeField] private float _inputMax = 1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCurveConverter"/> class with a linear curve.
        /// </summary>
        public AnimationCurveConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationCurveConverter"/> class.
        /// </summary>
        /// <param name="curve">The curve the value is passed through.</param>
        public AnimationCurveConverter(AnimationCurve curve)
        {
            _curve = curve;
        }

        /// <summary>
        /// Evaluates the curve at the specified value.
        /// </summary>
        /// <param name="value">The value to evaluate at.</param>
        /// <returns>The curve's value there, or the input unchanged when no curve is assigned.</returns>
        public float Convert(float value)
        {
            if (_curve is null || _curve.length == 0) return value;

            return _curve.Evaluate(_normalizeInput ? Normalize(value) : value);
        }

        private float Normalize(float value)
        {
            var span = _inputMax - _inputMin;
            return span == 0f ? 0f : Mathf.Clamp01((value - _inputMin) / span);
        }
    }

    /// <summary>
    /// Converts a 0..1 slider position to the decibels an <see cref="AudioMixer"/> expects.
    /// </summary>
    /// <remarks>
    /// A volume slider wired straight to a mixer sounds wrong: the mixer is logarithmic, so the top
    /// tenth of the slider carries most of the audible change and the bottom half does almost
    /// nothing. This is the conversion that makes a linear slider sound linear.
    /// </remarks>
    [Serializable]
    public sealed class AudioLinearToDecibelConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The decibel value silence maps to.")]
        [SerializeField] private float _minDecibels = -80f;

        [Tooltip("The decibel value full volume maps to.")]
        [SerializeField] private float _maxDecibels;

        [Tooltip("Slider positions at or below this are treated as silence.")]
        [SerializeField] private float _silenceThreshold = 0.0001f;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioLinearToDecibelConverter"/> class over -80..0 dB.
        /// </summary>
        public AudioLinearToDecibelConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioLinearToDecibelConverter"/> class.
        /// </summary>
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
            if (normalized <= _silenceThreshold) return _minDecibels;

            return Mathf.Clamp(Mathf.Log10(normalized) * 20f + _maxDecibels, _minDecibels, _maxDecibels);
        }

        /// <summary>
        /// Converts a decibel value back to a slider position.
        /// </summary>
        /// <param name="value">The decibel value.</param>
        /// <returns>The 0..1 slider position.</returns>
        public float ConvertBack(float value)
        {
            if (value <= _minDecibels) return 0f;
            return Mathf.Clamp01(Mathf.Pow(10f, (value - _maxDecibels) / 20f));
        }
    }

    /// <summary>
    /// Converts a decibel value to a 0..1 slider position.
    /// </summary>
    /// <remarks>
    /// The other direction of <see cref="AudioLinearToDecibelConverter"/>, for restoring a saved
    /// mixer value onto a slider.
    /// </remarks>
    [Serializable]
    public sealed class AudioDecibelToLinearConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("The decibel value that maps to silence.")]
        [SerializeField] private float _minDecibels = -80f;

        [Tooltip("The decibel value that maps to full volume.")]
        [SerializeField] private float _maxDecibels;

        [NonSerialized] private AudioLinearToDecibelConverter? _inverse;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDecibelToLinearConverter"/> class over -80..0 dB.
        /// </summary>
        public AudioDecibelToLinearConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDecibelToLinearConverter"/> class.
        /// </summary>
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
