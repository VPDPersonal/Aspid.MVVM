#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Folds an angle into a standard range.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Angle Wrap",
        Tooltip = "Folds an angle into a standard range")]
    public sealed class AngleWrapConverter : IConverter<float, float>, IConverter<double, double>
    {
        [Tooltip("Which range to report in.")]
        [SerializeField] private AngleRange _range = AngleRange.Zero360;

        [Tooltip("Added before wrapping.")]
        [SerializeField] private float _offset;

        /// <remarks>Default: reporting 0..360.</remarks>
        public AngleWrapConverter() { }

        /// <param name="range">Which range to report in.</param>
        /// <param name="offset">Added before wrapping.</param>
        public AngleWrapConverter(AngleRange range, float offset = 0f)
        {
            _range = range;
            _offset = offset;
        }

        /// <summary>
        /// Folds the specified angle into the configured range.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>
        /// The folded angle. A range that is not a declared <see cref="AngleRange"/> is reported and
        /// the bound angle is returned unchanged, without the offset.
        /// </returns>
        public float Convert(float value)
        {
            var wrapped = Mathf.Repeat(value + _offset, 360f);

            return _range switch
            {
                AngleRange.Zero360 => wrapped,
                AngleRange.Signed180 => wrapped > 180f ? wrapped - 360f : wrapped,
                _ => Undeclared(value)
            };
        }

        private float Undeclared(float value)
        {
            this.LogError($"the range {_range.Describe()} is not a declared {nameof(AngleRange)}",
                "Returning the bound angle unchanged.");

            return value;
        }

        // Unity's math is float, so the double overload carries a float's precision.
        double IConverter<double, double>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
