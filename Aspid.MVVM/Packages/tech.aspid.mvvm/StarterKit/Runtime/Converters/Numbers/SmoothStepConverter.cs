using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Converts a 0..1 position to a value in a range, eased in and out at the ends.
    /// </summary>
    /// <remarks>The incoming position is always held inside 0..1; there is no unclamped mode.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Smooth Step",
        Tooltip = "Converts a 0..1 position to a value in a range, eased in and out at the ends")]
    public sealed class SmoothStepConverter : IConverter<float, float>, IConverter<double, double>
    {
        [Tooltip("The value 0 maps to.")]
        [SerializeField] private float _from;

        [Tooltip("The value 1 maps to.")]
        [SerializeField] private float _to = 1f;

        /// <remarks>Default: over 0..1.</remarks>
        public SmoothStepConverter() { }

        /// <param name="from">The value 0 maps to.</param>
        /// <param name="to">The value 1 maps to.</param>
        public SmoothStepConverter(
            float from,
            float to)
        {
            _to = to;
            _from = from;
        }

        /// <summary>
        /// Converts the specified position to an eased value in the range.
        /// </summary>
        /// <param name="value">The 0..1 position. A position outside it is held at the nearer end.</param>
        /// <returns>The eased value at that position.</returns>
        public float Convert(float value) =>
            Mathf.SmoothStep(_from, _to, value);

        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
