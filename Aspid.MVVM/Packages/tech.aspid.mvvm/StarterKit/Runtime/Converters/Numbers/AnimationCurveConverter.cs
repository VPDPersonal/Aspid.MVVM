#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Passes a number through an <see cref="AnimationCurve"/>.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Animation Curve",
        Tooltip = "Passes a number through an AnimationCurve")]
    public sealed class AnimationCurveConverter : IConverter<float, float>, IConverter<double, double>
    {
        [Tooltip("The curve the value is passed through.")]
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Tooltip("Map the input range onto the curve's 0..1 domain before evaluating.")]
        [SerializeField] private bool _normalizeInput;

        [Tooltip("The input value that maps to the start of the curve. Read only while Normalize Input is on.")]
        [SerializeField] private float _inputMin;

        [Tooltip("The input value that maps to the end of the curve. Read only while Normalize Input is on.")]
        [SerializeField] private float _inputMax = 1f;

        /// <remarks>Default: with a linear curve.</remarks>
        public AnimationCurveConverter() { }

        /// <param name="curve">
        /// The curve the value is passed through. One with no keys is reported as an error and the
        /// value passes through unchanged.
        /// </param>
        public AnimationCurveConverter(AnimationCurve curve)
        {
            _curve = curve;
        }

        /// <summary>
        /// Evaluates the curve at the specified value.
        /// </summary>
        /// <param name="value">The value to evaluate at.</param>
        /// <returns>
        /// The curve's value there. A curve with no keys is reported as an error and the input passes
        /// through unchanged.
        /// </returns>
        public float Convert(float value)
        {
            if (_curve is not { length: > 0 })
            {
                this.LogError(
                    problem: "no curve is assigned",
                    consequence: "Passing the value through unchanged.");

                return value;
            }

            return _curve.Evaluate(_normalizeInput ? Normalize(value) : value);
        }

        private float Normalize(float value)
        {
            var span = _inputMax - _inputMin;
            if (span is not 0f) return Mathf.Clamp01((value - _inputMin) / span);

            this.LogError(
                problem: $"the input range is empty (min and max are both {_inputMin})",
                consequence: "Reading the curve at its start.");

            return 0f;
        }

        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
