#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures one number out of a 2D vector.
    /// </summary>
    /// <remarks>
    /// The 2D counterpart of <see cref="Vector3ToFloatConverter"/>: a joystick's throw driving a
    /// speed readout, a drag offset driving a bar.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector2 To Float", Tooltip = "Measures one number out of a 2D vector")]
    public sealed class Vector2ToFloatConverter : IConverter<Vector2, float>
    {
        [Tooltip("Which number to take.")]
        [SerializeField] private Vector2Component _component = Vector2Component.Magnitude;

        // Up rather than right, so the same-named setting on the 3D converter and on this one starts
        // pointing the same way.
        [Tooltip("The direction Dot measures along. Keep it unit length to read a plain distance.")]
        [SerializeField] private Vector2 _dotAgainst = Vector2.up;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ToFloatConverter"/> class measuring length.
        /// </summary>
        public Vector2ToFloatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ToFloatConverter"/> class.
        /// </summary>
        /// <param name="component">Which number to take.</param>
        public Vector2ToFloatConverter(Vector2Component component)
        {
            _component = component;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2ToFloatConverter"/> class measuring
        /// along a direction.
        /// </summary>
        /// <param name="dotAgainst">The direction to measure along.</param>
        public Vector2ToFloatConverter(Vector2 dotAgainst)
        {
            _component = Vector2Component.Dot;
            _dotAgainst = dotAgainst;
        }

        /// <summary>
        /// Measures the specified vector.
        /// </summary>
        /// <param name="value">The vector to measure.</param>
        /// <returns>
        /// The measurement. <see cref="Vector2Component.Dot"/> is the raw dot product, so a unit
        /// direction reads as the signed distance along it and a longer one scales that reading.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the component is not a declared value.</exception>
        public float Convert(Vector2 value) => _component switch
        {
            Vector2Component.X => value.x,
            Vector2Component.Y => value.y,
            Vector2Component.Magnitude => value.magnitude,
            Vector2Component.SqrMagnitude => value.sqrMagnitude,
            Vector2Component.Dot => Vector2.Dot(value, _dotAgainst),
            _ => throw new ArgumentOutOfRangeException(nameof(_component), _component, null)
        };
    }
}
