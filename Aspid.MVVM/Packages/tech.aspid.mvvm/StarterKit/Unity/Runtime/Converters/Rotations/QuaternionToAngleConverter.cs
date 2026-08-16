#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the angle a rotation carries around one axis.
    /// </summary>
    /// <remarks>
    /// Not <see cref="AngleToQuaternionConverter"/>'s <c>ConvertBack</c> spelled as a converter of its
    /// own: it reads the axis the same way, but carries no offset and no clockwise flag, and folds the
    /// result into 0..360 or ±180. Reading back a rotation that converter built with an offset, or set
    /// clockwise, therefore does not return the angle that went in.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Quaternion To Angle", Tooltip = "Reads the angle a rotation carries around one axis")]
    public sealed class QuaternionToAngleConverter : IConverter<Quaternion, float>
    {
        [Tooltip("The axis the angle is read around. Z is the one a 2D UI element spins on.")]
        [SerializeField] private RotationAxis _axis = RotationAxis.Z;

        [Tooltip("The axis the angle is read around when the axis above is set to Custom.")]
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

        /// <summary>
        /// Reads the angle off the specified rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The angle, in degrees.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the axis is not a declared value.</exception>
        public float Convert(Quaternion value)
        {
            var euler = value.eulerAngles;

            var angle = _axis switch
            {
                RotationAxis.X => euler.x,
                RotationAxis.Y => euler.y,
                RotationAxis.Z => euler.z,
                RotationAxis.Custom => CustomAngle(value),
                _ => throw new ArgumentOutOfRangeException(nameof(_axis), _axis, null)
            };

            var wrapped = Mathf.Repeat(angle, 360f);
            return _signed && wrapped > 180f ? wrapped - 360f : wrapped;
        }

        // Same reading as AngleToQuaternionConverter performs: ToAngleAxis reports a positive turn
        // around whichever axis makes it positive, so the dot decides whether that axis is the
        // authored one or its opposite.
        private float CustomAngle(Quaternion value)
        {
            if (_customAxis.sqrMagnitude <= Mathf.Epsilon) return 0f;

            value.ToAngleAxis(out var angle, out var axis);
            return Vector3.Dot(axis, _customAxis) < 0f ? -angle : angle;
        }
    }
}
