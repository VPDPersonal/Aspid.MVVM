#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the angle a rotation carries around one axis.
    /// </summary>
    /// <remarks>
    /// The angle is folded into 0..360, or ±180 when signed.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Quaternion/To Number",
        Name = "To Angle",
        Tooltip = "Reads the angle a rotation carries around one axis")]
    public sealed class QuaternionToAngleConverter :
        IConverter<Quaternion, float>,
        IConverter<Quaternion, double>
    {
        [Tooltip("The axis the angle is read around. Z is the one a 2D UI element spins on.")]
        [SerializeField] private RotationAxis _axis = RotationAxis.Z;

        [Tooltip("The axis the angle is read around when Axis is set to Custom.")]
        [SerializeField] private Vector3 _customAxis = Vector3.up;

        [Tooltip("Report the angle as -180..180 rather than Unity's 0..360.")]
        [SerializeField] private bool _signed = true;

        /// <remarks>Default: reading Z.</remarks>
        public QuaternionToAngleConverter() { }

        /// <param name="axis">The axis the angle is read around.</param>
        /// <param name="signed">Whether to report the angle as -180..180.</param>
        public QuaternionToAngleConverter(RotationAxis axis, bool signed = true)
        {
            _axis = axis;
            _signed = signed;
        }

        /// <param name="customAxis">
        /// The axis the angle is read around. A zero vector reports an error and the angle reads zero.
        /// </param>
        /// <param name="signed">Whether to report the angle as -180..180.</param>
        public QuaternionToAngleConverter(Vector3 customAxis, bool signed = true)
        {
            _signed = signed;
            _customAxis = customAxis;
            _axis = RotationAxis.Custom;
        }

        /// <summary>
        /// Reads the angle off the specified rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>
        /// The angle, in degrees. A zero custom axis, or an axis that is not a declared
        /// <see cref="RotationAxis"/>, reports an error and reads zero.
        /// </returns>
        public float Convert(Quaternion value)
        {
            var euler = value.eulerAngles;

            var angle = _axis switch
            {
                RotationAxis.X => euler.x,
                RotationAxis.Y => euler.y,
                RotationAxis.Z => euler.z,
                RotationAxis.Custom => CustomAngle(value),
                _ => Undeclared()
            };

            var wrapped = Mathf.Repeat(angle, 360f);
            return _signed && wrapped > 180f ? wrapped - 360f : wrapped;
        }
        
        // Unity's math is float, so the double overload carries a float's precision.
        double IConverter<Quaternion, double>.Convert(Quaternion value) => Convert(value);

        private float Undeclared()
        {
            this.LogError(
                problem: $"the axis {_axis.Describe()} is not a declared {nameof(RotationAxis)}",
                consequence: "Reading the angle as zero.");

            return 0f;
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
