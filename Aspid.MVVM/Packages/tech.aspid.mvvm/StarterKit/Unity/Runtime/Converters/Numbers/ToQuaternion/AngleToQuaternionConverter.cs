#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns a single angle into a rotation.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Quaternion",
        Name = "Angle To Quaternion",
        Tooltip = "Turns a single angle into a rotation")]
    public sealed class AngleToQuaternionConverter :
        ITwoWayConverter<float, Quaternion>,
        ITwoWayConverter<double, Quaternion>
    {
        [Tooltip("The axis the angle turns around. Z is the one a 2D UI element spins on.")]
        [SerializeField] private RotationAxis _axis = RotationAxis.Z;

        [Tooltip("The axis the angle turns around when Axis is set to Custom.")]
        [SerializeField] private Vector3 _customAxis = Vector3.up;

        [Tooltip("Added to the angle before it is applied.")]
        [SerializeField] private float _offset;

        [Tooltip("Turn the other way.")]
        [SerializeField] private bool _clockwise;

        /// <remarks>Default: turning around Z.</remarks>
        public AngleToQuaternionConverter() { }

        /// <param name="axis">The axis the angle turns around.</param>
        /// <param name="offset">Added to the angle before it is applied.</param>
        /// <param name="clockwise">If <see langword="true"/>, turns the other way.</param>
        public AngleToQuaternionConverter(RotationAxis axis, float offset = 0f, bool clockwise = false)
        {
            _axis = axis;
            _offset = offset;
            _clockwise = clockwise;
        }

        /// <param name="customAxis">
        /// The axis the angle turns around. A zero vector reports an error and the rotation turns nowhere.
        /// </param>
        /// <param name="offset">Added to the angle before it is applied.</param>
        /// <param name="clockwise">If <see langword="true"/>, turns the other way.</param>
        public AngleToQuaternionConverter(Vector3 customAxis, float offset = 0f, bool clockwise = false)
        {
            _offset = offset;
            _clockwise = clockwise;
            _customAxis = customAxis;
            _axis = RotationAxis.Custom;
        }

        /// <summary>
        /// Turns the specified angle into a rotation.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>
        /// The rotation. An undeclared axis reports an error and turns around Z; a zero custom axis
        /// reports an error and turns nowhere.
        /// </returns>
        public Quaternion Convert(float value)
        {
            var angle = (_clockwise ? -value : value) + _offset;

            return _axis switch
            {
                RotationAxis.X => Quaternion.Euler(angle, 0f, 0f),
                RotationAxis.Y => Quaternion.Euler(0f, angle, 0f),
                RotationAxis.Z => Quaternion.Euler(0f, 0f, angle),
                RotationAxis.Custom => CustomRotation(angle),
                _ => UndeclaredRotation(angle)
            };
        }

        // Unity's math is float, so the double overload carries a float's precision.
        Quaternion IConverter<double, Quaternion>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));

        double ITwoWayConverter<double, Quaternion>.ConvertBack(Quaternion value) => 
            ConvertBack(value);

        /// <summary>
        /// Reads the angle back off a rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>
        /// The angle, in degrees. An undeclared axis reports an error and reads the angle off Z; a
        /// zero custom axis reports an error and reads the angle as zero.
        /// </returns>
        public float ConvertBack(Quaternion value)
        {
            var euler = value.eulerAngles;

            var angle = _axis switch
            {
                RotationAxis.X => euler.x,
                RotationAxis.Y => euler.y,
                RotationAxis.Z => euler.z,
                RotationAxis.Custom => CustomAngle(value),
                _ => UndeclaredAngle(euler)
            };

            angle -= _offset;
            return _clockwise ? -angle : angle;
        }

        // Z is the axis a new converter starts on, so it is the likeliest reading.
        private Quaternion UndeclaredRotation(float angle)
        {
            this.LogError(
                problem: $"the axis {_axis.Describe()} is not a declared {nameof(RotationAxis)}",
                consequence: "Turning around Z.");

            return Quaternion.Euler(0f, 0f, angle);
        }

        private float UndeclaredAngle(Vector3 euler)
        {
            this.LogError(
                problem: $"the axis {_axis.Describe()} is not a declared {nameof(RotationAxis)}",
                consequence: "Reading the angle off Z.");

            return euler.z;
        }

        private Quaternion CustomRotation(float angle)
        {
            if (_customAxis.sqrMagnitude <= Mathf.Epsilon)
            {
                this.LogError(
                    problem: "the custom axis is zero",
                    consequence: "Turning nowhere.");
                
                return Quaternion.identity;
            }

            return Quaternion.AngleAxis(angle, _customAxis);
        }

        // ToAngleAxis always reports a positive turn, so the dot tells the authored axis from its opposite.
        private float CustomAngle(Quaternion value)
        {
            if (_customAxis.sqrMagnitude <= Mathf.Epsilon)
            {
                this.LogError(
                    problem: "the custom axis is zero",
                    consequence: "Reading the angle as zero.");
                
                return 0f;
            }

            value.ToAngleAxis(out var angle, out var axis);
            return Vector3.Dot(axis, _customAxis) < 0f ? -angle : angle;
        }
    }
}
