#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts between degrees and radians.
    /// </summary>
    /// <remarks>Physics and backend data usually speak radians; Unity's Inspector speaks degrees.</remarks>
    [Serializable]
    public sealed class DegreesToRadiansConverter : ITwoWayConverter<float, float>
    {
        /// <summary>
        /// Converts the specified angle to radians.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>The angle, in radians.</returns>
        public float Convert(float value) => value * Mathf.Deg2Rad;

        /// <summary>
        /// Converts the specified angle to degrees.
        /// </summary>
        /// <param name="value">The angle, in radians.</param>
        /// <returns>The angle, in degrees.</returns>
        public float ConvertBack(float value) => value * Mathf.Rad2Deg;
    }
}
