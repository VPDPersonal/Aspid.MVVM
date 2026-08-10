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
    /// A dial or a steering wheel reporting where the player left it. A
    /// <see cref="BindMode.OneWay"/> binding never calls <c>ConvertBack</c>, so a ViewModel that
    /// wants an angle out of a rotation had nothing it could pick.
    /// <para>
    /// It is not <see cref="AngleToQuaternionConverter"/>'s <c>ConvertBack</c> spelled as a
    /// converter of its own. It reads the axis the same way, but it carries no offset and no
    /// clockwise flag of its own, and it folds the result into 0..360 or ±180. Reading back a
    /// rotation an <see cref="AngleToQuaternionConverter"/> built with an offset, or set clockwise,
    /// therefore does not return the angle that went in — that converter undoes both in its
    /// <c>ConvertBack</c>, and this one has neither to undo.
    /// </para>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionToAngleConverter"/> class reading Z.
        /// </summary>
        public QuaternionToAngleConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionToAngleConverter"/> class.
        /// </summary>
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
