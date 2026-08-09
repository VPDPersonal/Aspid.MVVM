#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads the angle a direction points in.
    /// </summary>
    /// <remarks>
    /// Off-screen enemy indicators, waypoint arrows, minimap markers — everything that has a
    /// direction and needs a rotation.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Direction To Angle", Tooltip = "Reads the angle a direction points in")]
    public sealed class DirectionToAngleConverter : IConverter<Vector2, float>
    {
        [Tooltip("Report the angle in degrees rather than radians.")]
        [SerializeField] private bool _degrees = true;

        [Tooltip("Added to the angle.")]
        [SerializeField] private float _offset;

        [Tooltip("Measure clockwise rather than counter-clockwise.")]
        [SerializeField] private bool _clockwise;

        /// <remarks>Default: reporting degrees.</remarks>
        public DirectionToAngleConverter() { }

        /// <param name="offset">Added to the angle.</param>
        /// <param name="clockwise">Whether to measure clockwise.</param>
        public DirectionToAngleConverter(float offset, bool clockwise = false)
        {
            _offset = offset;
            _clockwise = clockwise;
        }

        /// <summary>
        /// Reads the angle of the specified direction.
        /// </summary>
        /// <param name="value">The direction to read.</param>
        /// <returns>The angle. A zero-length direction reads as the offset alone.</returns>
        public float Convert(Vector2 value)
        {
            if (value == Vector2.zero) return _offset;

            var radians = Mathf.Atan2(value.y, value.x);
            var angle = _degrees ? radians * Mathf.Rad2Deg : radians;

            return (_clockwise ? -angle : angle) + _offset;
        }
    }
}
