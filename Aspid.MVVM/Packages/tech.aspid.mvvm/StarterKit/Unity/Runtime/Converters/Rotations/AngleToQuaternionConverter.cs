#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns a single angle into a rotation.
    /// </summary>
    /// <remarks>
    /// Compass needles, dials, gauge hands, radar sweeps: one number from the ViewModel, a rotation
    /// on the View. Until now <c>IConverterQuaternion</c> had no implementation at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Angle To Quaternion", Tooltip = "Turns a single angle into a rotation")]
    public sealed class AngleToQuaternionConverter : ITwoWayConverter<float, Quaternion>
    {
        [Tooltip("The axis the angle turns around. Z is the one a 2D UI element spins on.")]
        [SerializeField] private RotationAxis _axis = RotationAxis.Z;

        [Tooltip("The axis the angle turns around when the axis above is set to Custom.")]
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

        /// <remarks>Default: turning around an arbitrary axis.</remarks>
        /// <param name="customAxis">The axis the angle turns around.</param>
        /// <param name="offset">Added to the angle before it is applied.</param>
        /// <param name="clockwise">If <see langword="true"/>, turns the other way.</param>
        public AngleToQuaternionConverter(Vector3 customAxis, float offset = 0f, bool clockwise = false)
        {
            _axis = RotationAxis.Custom;
            _customAxis = customAxis;
            _offset = offset;
            _clockwise = clockwise;
        }

        /// <summary>
        /// Turns the specified angle into a rotation.
        /// </summary>
        /// <param name="value">The angle, in degrees.</param>
        /// <returns>The rotation.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the axis is not a declared value.</exception>
        public Quaternion Convert(float value)
        {
            var angle = (_clockwise ? -value : value) + _offset;

            return _axis switch
            {
                RotationAxis.X => Quaternion.Euler(angle, 0f, 0f),
                RotationAxis.Y => Quaternion.Euler(0f, angle, 0f),
                RotationAxis.Z => Quaternion.Euler(0f, 0f, angle),
                RotationAxis.Custom => CustomRotation(angle),
                _ => throw new ArgumentOutOfRangeException(nameof(_axis), _axis, null)
            };
        }

        /// <summary>
        /// Reads the angle back off a rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The angle, in degrees.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the axis is not a declared value.</exception>
        public float ConvertBack(Quaternion value)
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

            angle -= _offset;
            return _clockwise ? -angle : angle;
        }

        // AngleAxis normalises the axis for us, and it already answers a zero axis with the
        // identity, so this branch changes no result today. It is here because an axis nobody
        // filled in is the ordinary way to reach this method and the identity is what the converter
        // means to return there — said at the call site rather than left resting on a Unity
        // behaviour the reader has to go and confirm.
        private Quaternion CustomRotation(float angle) => _customAxis.sqrMagnitude <= Mathf.Epsilon
            ? Quaternion.identity
            : Quaternion.AngleAxis(angle, _customAxis);

        // ToAngleAxis always reports a positive turn, flipping the axis when that is what it takes,
        // so the axis it returns can be the opposite of the authored one. The dot picks which of the
        // two equivalent readings is the one the author asked for.
        private float CustomAngle(Quaternion value)
        {
            if (_customAxis.sqrMagnitude <= Mathf.Epsilon) return 0f;

            value.ToAngleAxis(out var angle, out var axis);
            return Vector3.Dot(axis, _customAxis) < 0f ? -angle : angle;
        }
    }
}
