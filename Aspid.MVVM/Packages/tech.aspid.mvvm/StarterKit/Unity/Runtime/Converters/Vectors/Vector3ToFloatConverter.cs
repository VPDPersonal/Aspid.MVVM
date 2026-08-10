#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Measures one number out of a vector.
    /// </summary>
    /// <remarks>Driving a bar or a label from one axis, or from how long the vector is.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 To Float", Tooltip = "Measures one number out of a vector")]
    public sealed class Vector3ToFloatConverter : IConverter<Vector3, float>
    {
        [Tooltip("Which number to take.")]
        [SerializeField] private VectorComponent _component = VectorComponent.Magnitude;

        [Tooltip("The direction Dot measures along. Keep it unit length to read a plain distance.")]
        [SerializeField] private Vector3 _dotAgainst = Vector3.up;

        /// <remarks>Default: measuring length.</remarks>
        public Vector3ToFloatConverter() { }

        /// <param name="component">Which number to take.</param>
        public Vector3ToFloatConverter(VectorComponent component)
        {
            _component = component;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToFloatConverter"/> class measuring
        /// along a direction.
        /// </summary>
        /// <param name="dotAgainst">The direction to measure along.</param>
        public Vector3ToFloatConverter(Vector3 dotAgainst)
        {
            _component = VectorComponent.Dot;
            _dotAgainst = dotAgainst;
        }

        /// <summary>
        /// Measures the specified vector.
        /// </summary>
        /// <param name="value">The vector to measure.</param>
        /// <returns>
        /// The measurement. <see cref="VectorComponent.Dot"/> is the raw dot product, so a unit
        /// direction reads as the signed distance along it and a longer one scales that reading.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the component is not a declared value.</exception>
        public float Convert(Vector3 value) => _component switch
        {
            VectorComponent.X => value.x,
            VectorComponent.Y => value.y,
            VectorComponent.Z => value.z,
            VectorComponent.Magnitude => value.magnitude,
            VectorComponent.SqrMagnitude => value.sqrMagnitude,
            VectorComponent.Dot => Vector3.Dot(value, _dotAgainst),
            _ => throw new ArgumentOutOfRangeException(nameof(_component), _component, null)
        };
    }
}
