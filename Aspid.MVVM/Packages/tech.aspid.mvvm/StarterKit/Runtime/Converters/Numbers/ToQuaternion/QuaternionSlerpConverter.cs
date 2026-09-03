#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns between two rotations by a 0..1 amount.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Quaternion",
        Name = "Slerp",
        Tooltip = "Turns between two rotations by a 0..1 amount")]
    public sealed class QuaternionSlerpConverter : IConverter<float, Quaternion>, IConverter<double, Quaternion>
    {
        [Tooltip("The rotation at 0, in Euler degrees.")]
        [SerializeField] private Vector3 _fromEuler;

        [Tooltip("The rotation at 1, in Euler degrees.")]
        [SerializeField] private Vector3 _toEuler;

        [Tooltip("Shapes the amount before the turn. Leave it empty for an even sweep.")]
        [SerializeField] private AnimationCurve? _curve;

        [Tooltip("Hold the incoming amount inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: both endpoints are identity, so every amount reads as no rotation.</remarks>
        public QuaternionSlerpConverter() { }

        /// <param name="fromEuler">The rotation at 0, in Euler degrees.</param>
        /// <param name="toEuler">The rotation at 1, in Euler degrees.</param>
        /// <param name="curve">Shapes the amount before the turn, or <see langword="null"/> for an even sweep.</param>
        public QuaternionSlerpConverter(
            Vector3 fromEuler,
            Vector3 toEuler,
            AnimationCurve? curve = null)
        {
            _fromEuler = fromEuler;
            _toEuler = toEuler;
            _curve = curve;
        }

        /// <summary>
        /// Reads the rotation at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>The rotation there.</returns>
        public Quaternion Convert(float value)
        {
            var amount = _curve is { length: > 0 } ? _curve.Evaluate(value) : value;

            var from = Quaternion.Euler(_fromEuler);
            var to = Quaternion.Euler(_toEuler);

            return _clamp
                ? Quaternion.Slerp(from, to, amount)
                : Quaternion.SlerpUnclamped(from, to, amount);
        }

        Quaternion IConverter<double, Quaternion>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
