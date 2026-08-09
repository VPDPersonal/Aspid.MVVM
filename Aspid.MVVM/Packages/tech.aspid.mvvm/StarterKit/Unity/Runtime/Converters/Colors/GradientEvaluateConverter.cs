#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a colour off a <see cref="Gradient"/>.
    /// </summary>
    /// <remarks>
    /// The health-bar colour converter. A gradient is edited with Unity's own editor, so the whole
    /// green-to-red ramp is authored rather than written, and the ViewModel keeps sending the one
    /// number it already has.
    /// </remarks>
    [Serializable]
    public sealed class GradientEvaluateConverter : IConverter<float, Color>
    {
        [Tooltip("The gradient the value is read from.")]
        [SerializeField] private Gradient _gradient = new();

        [Tooltip("The input value that maps to the start of the gradient.")]
        [SerializeField] private float _inputMin;

        [Tooltip("The input value that maps to the end of the gradient.")]
        [SerializeField] private float _inputMax = 1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradientEvaluateConverter"/> class over 0..1.
        /// </summary>
        public GradientEvaluateConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GradientEvaluateConverter"/> class.
        /// </summary>
        /// <param name="gradient">The gradient the value is read from.</param>
        /// <param name="inputMin">The input value that maps to the start of the gradient.</param>
        /// <param name="inputMax">The input value that maps to the end of the gradient.</param>
        public GradientEvaluateConverter(Gradient gradient, float inputMin = 0f, float inputMax = 1f)
        {
            _gradient = gradient;
            _inputMin = inputMin;
            _inputMax = inputMax;
        }

        /// <summary>
        /// Reads the colour at the specified value.
        /// </summary>
        /// <param name="value">The value to read at.</param>
        /// <returns>The colour there, or white when no gradient is assigned.</returns>
        public Color Convert(float value)
        {
            if (_gradient is null) return Color.white;

            var span = _inputMax - _inputMin;
            var time = span == 0f ? 0f : Mathf.Clamp01((value - _inputMin) / span);

            return _gradient.Evaluate(time);
        }
    }
}
