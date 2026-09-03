#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between degrees and radians.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Degrees To Radians",
        Tooltip = "Converts between degrees and radians")]
    public sealed class DegreesRadiansConverter :
        ITwoWayConverter<float, float>,
        ITwoWayConverter<double, double>
    {
        [Tooltip("Convert radians to degrees instead.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>Default: degrees to radians.</remarks>
        public DegreesRadiansConverter() { }

        /// <param name="isInvert">If <see langword="true"/>, converts radians to degrees instead.</param>
        public DegreesRadiansConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Converts the specified angle in the authored direction.
        /// </summary>
        /// <param name="value">The angle, in degrees, or radians when inverted.</param>
        /// <returns>The angle, in radians, or degrees when inverted.</returns>
        public float Convert(float value) =>
            value * (_isInvert ? Mathf.Rad2Deg : Mathf.Deg2Rad);

        /// <summary>
        /// Converts the specified angle back in the opposite direction.
        /// </summary>
        /// <param name="value">The angle, in radians, or degrees when inverted.</param>
        /// <returns>The angle, in degrees, or radians when inverted.</returns>
        public float ConvertBack(float value) =>
            value * (_isInvert ? Mathf.Deg2Rad : Mathf.Rad2Deg);

        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));

        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            ConvertBack(NumericSaturation.ToFloat(value));
    }
}
