#nullable enable
using Aspid.FastTools.Types;
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
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Animation Curve", Tooltip = "Passes a number through an AnimationCurve")]
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

        /// <remarks>Default: with a linear curve.</remarks>
        public AnimationCurveConverter() { }

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
}
