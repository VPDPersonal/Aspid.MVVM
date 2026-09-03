#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a color off a <see cref="Gradient"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Color",
        Name = "Gradient Evaluate",
        Tooltip = "Reads a color off a Gradient")]
    public sealed class GradientEvaluateConverter : IConverter<float, Color>, IConverter<double, Color>
    {
        [Tooltip("The gradient the value is read from.")]
        [SerializeField] private Gradient? _gradient;

        [Tooltip("The input value that maps to the start of the gradient.")]
        [SerializeField] private float _inputMin;

        [Tooltip("The input value that maps to the end of the gradient.")]
        [SerializeField] private float _inputMax = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public GradientEvaluateConverter() { }

        /// <param name="gradient">The gradient the value is read from.</param>
        /// <param name="inputMin">
        /// The input value that maps to the start of the gradient. Equal to <paramref name="inputMax"/>,
        /// the range is reported as an error and the gradient is read at its start.
        /// </param>
        /// <param name="inputMax">
        /// The input value that maps to the end of the gradient. Equal to <paramref name="inputMin"/>,
        /// the range is reported as an error and the gradient is read at its start.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="gradient"/> is <see langword="null"/>.</exception>
        public GradientEvaluateConverter(
            Gradient gradient,
            float inputMin = 0f,
            float inputMax = 1f)
        {
            _inputMin = inputMin;
            _inputMax = inputMax;
            _gradient = gradient ?? throw new ArgumentNullException(nameof(gradient));
        }

        /// <summary>
        /// Reads the color at the specified value.
        /// </summary>
        /// <param name="value">The value to read at.</param>
        /// <returns>The color there, or white when no gradient is assigned.</returns>
        public Color Convert(float value)
        {
            if (_gradient is null)
            {
                this.LogError(
                    problem: "no gradient is assigned",
                    consequence: "Returning white.");

                return Color.white;
            }

            return _gradient.Evaluate(Normalize(value));
        }

        private float Normalize(float value)
        {
            var span = _inputMax - _inputMin;
            if (span is not 0f) return Mathf.Clamp01((value - _inputMin) / span);

            this.LogError(
                problem: $"the input range is empty (min and max are both {_inputMin})",
                consequence: "Reading the gradient at its start.");

            return 0f;
        }

        Color IConverter<double, Color>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
