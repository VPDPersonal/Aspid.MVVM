#nullable enable
using System;
using UnityEngine;

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
    public sealed class AngleToQuaternionConverter : ITwoWayConverter<float, Quaternion>
    {
        [Tooltip("The axis the angle turns around. Z is the one a 2D UI element spins on.")]
        [SerializeField] private RotationAxis _axis = RotationAxis.Z;

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
                _ => throw new ArgumentOutOfRangeException(nameof(_axis), _axis, null)
            };
        }

        /// <summary>
        /// Reads the angle back off a rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The angle, in degrees.</returns>
        public float ConvertBack(Quaternion value)
        {
            var euler = value.eulerAngles;

            var angle = _axis switch
            {
                RotationAxis.X => euler.x,
                RotationAxis.Y => euler.y,
                RotationAxis.Z => euler.z,
                _ => throw new ArgumentOutOfRangeException(nameof(_axis), _axis, null)
            };

            angle -= _offset;
            return _clockwise ? -angle : angle;
        }
    }
}
