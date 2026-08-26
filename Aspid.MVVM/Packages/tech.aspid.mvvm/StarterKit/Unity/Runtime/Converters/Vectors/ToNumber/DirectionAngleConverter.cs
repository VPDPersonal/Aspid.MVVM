#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the angle a direction points in, and turns an angle back into a direction.
    /// </summary>
    /// <remarks>
    /// Both directions read the same unit, offset and winding, so the round trip returns the angle it
    /// was given; only the length of the direction is the reverse pass's own setting.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/To Number",
        Name = "Direction To Angle",
        Tooltip = "Reads the angle a direction points in, and turns an angle back into a direction")]
    public sealed class DirectionAngleConverter :
        ITwoWayConverter<Vector2, float>,
        IConverter<Vector2, double>,
        ITwoWayConverter<float, Vector2>,
        IConverter<double, Vector2>
    {
        [Tooltip("Report the angle in degrees rather than radians.")]
        [SerializeField] private bool _degrees = true;

        [Tooltip("Added to the angle, in the unit the angle is reported in.")]
        [SerializeField] private float _offset;

        [Tooltip("Measure clockwise rather than counter-clockwise.")]
        [SerializeField] private bool _clockwise;

        [Tooltip("How long a direction built from an angle is.")]
        [SerializeField] private float _magnitude = 1f;

        /// <remarks>Default: reporting degrees.</remarks>
        public DirectionAngleConverter() { }

        /// <param name="offset">Added to the angle, in the unit the angle is reported in.</param>
        /// <param name="clockwise">Whether to measure clockwise.</param>
        /// <param name="degrees">
        /// Whether to report the angle in degrees rather than radians. When omitted, degrees.
        /// </param>
        /// <param name="magnitude">
        /// How long a direction built from an angle is. When omitted, one.
        /// </param>
        public DirectionAngleConverter(
            float offset,
            bool clockwise = false,
            bool degrees = true,
            float magnitude = 1f)
        {
            _offset = offset;
            _clockwise = clockwise;
            _degrees = degrees;
            _magnitude = magnitude;
        }

        /// <summary>
        /// Reads the angle of the specified direction.
        /// </summary>
        /// <param name="value">The direction to read.</param>
        /// <returns>
        /// The angle. A direction shorter than Unity's 1e-5 length floor reads as the offset alone.
        /// </returns>
        public float Convert(Vector2 value)
        {
            // Unity's == on vectors compares with a tolerance, so a very short direction reads as zero.
            if (value == Vector2.zero) return _offset;

            var radians = Mathf.Atan2(value.y, value.x);
            var angle = _degrees ? radians * Mathf.Rad2Deg : radians;

            return (_clockwise ? -angle : angle) + _offset;
        }

        /// <summary>
        /// Turns the specified angle back into a direction.
        /// </summary>
        /// <param name="value">The angle.</param>
        /// <returns>The direction, as long as the authored magnitude.</returns>
        public Vector2 ConvertBack(float value)
        {
            var angle = value - _offset;
            if (_clockwise) angle = -angle;

            var radians = _degrees ? angle * Mathf.Deg2Rad : angle;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * _magnitude;
        }

        // The same mapping read from the other side, so a binder starting from the angle picks this one.
        Vector2 IConverter<float, Vector2>.Convert(float value) => ConvertBack(value);

        float ITwoWayConverter<float, Vector2>.ConvertBack(Vector2 value) => Convert(value);

        // Unity's math is float, so the double overloads carry a float's precision.
        double IConverter<Vector2, double>.Convert(Vector2 value) => Convert(value);

        Vector2 IConverter<double, Vector2>.Convert(double value) =>
            ConvertBack(NumericSaturation.ToFloat(value));
    }
}
