#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures how far an angle is from a fixed one.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Angle Difference",
        Tooltip = "Measures how far an angle is from a fixed one")]
    public sealed class AngleDifferenceConverter : IConverter<float, float>, IConverter<double, double>
    {
        [Tooltip("The angle the bound one is measured against, in degrees.")]
        [SerializeField] private float _reference;

        [Tooltip("Keep the sign. Clear it to report how far off the angle is whichever way it went.")]
        [SerializeField] private bool _signed = true;

        /// <remarks>Default: measuring from zero.</remarks>
        public AngleDifferenceConverter() { }

        /// <param name="reference">The angle the bound one is measured against, in degrees.</param>
        /// <param name="signed">Whether to keep the sign of the difference.</param>
        public AngleDifferenceConverter(
            float reference,
            bool signed = true)
        {
            _reference = reference;
            _signed = signed;
        }

        /// <summary>
        /// Measures the specified angle against the reference.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>The shortest way around from the reference to it, in degrees.</returns>
        public float Convert(float value)
        {
            var difference = Mathf.DeltaAngle(_reference, value);
            return _signed ? difference : Mathf.Abs(difference);
        }

        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
