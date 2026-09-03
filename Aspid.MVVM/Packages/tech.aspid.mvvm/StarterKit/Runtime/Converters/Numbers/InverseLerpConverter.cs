using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a value in a range to its 0..1 position within it.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Inverse Lerp",
        Tooltip = "Converts a value in a range to its 0..1 position within it")]
    public sealed class InverseLerpConverter :
        ITwoWayConverter<float, float>,
        ITwoWayConverter<double, double>
    {
        [Tooltip("The value that maps to 0.")]
        [SerializeField] private float _min;

        [Tooltip("The value that maps to 1.")]
        [SerializeField] private float _max = 1f;

        [Tooltip("Hold the result inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: over 0..1.</remarks>
        public InverseLerpConverter() { }

        /// <param name="min">The value that maps to 0.</param>
        /// <param name="max">The value that maps to 1.</param>
        /// <param name="clamp">If <see langword="true"/>, holds the result inside 0..1.</param>
        public InverseLerpConverter(
            float min,
            float max,
            bool clamp = true)
        {
            _min = min;
            _max = max;
            _clamp = clamp;
        }

        /// <summary>
        /// Converts the specified value to its position in the range.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>Its 0..1 position. A degenerate range yields 0.</returns>
        public float Convert(float value) =>
            RemapNumberConverter.Map(value, _min, _max, 0f, 1f, _clamp);

        /// <summary>
        /// Converts a 0..1 position back to a value in the range.
        /// </summary>
        /// <param name="value">The position to convert.</param>
        /// <returns>The value at that position.</returns>
        public float ConvertBack(float value) =>
            RemapNumberConverter.Map(value, 0f, 1f, _min, _max, _clamp);

        double IConverter<double, double>.Convert(double value) =>
            RemapNumberConverter.Map(value, _min, _max, 0d, 1d, _clamp);

        double ITwoWayConverter<double, double>.ConvertBack(double value) =>
            RemapNumberConverter.Map(value, 0d, 1d, _min, _max, _clamp);
    }
}
