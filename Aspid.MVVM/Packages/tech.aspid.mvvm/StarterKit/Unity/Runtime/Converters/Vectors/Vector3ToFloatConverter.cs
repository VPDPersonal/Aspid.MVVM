#nullable enable
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
    public sealed class Vector3ToFloatConverter : IConverter<Vector3, float>
    {
        [Tooltip("Which number to take.")]
        [SerializeField] private VectorComponent _component = VectorComponent.Magnitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToFloatConverter"/> class measuring length.
        /// </summary>
        public Vector3ToFloatConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3ToFloatConverter"/> class.
        /// </summary>
        /// <param name="component">Which number to take.</param>
        public Vector3ToFloatConverter(VectorComponent component)
        {
            _component = component;
        }

        /// <summary>
        /// Measures the specified vector.
        /// </summary>
        /// <param name="value">The vector to measure.</param>
        /// <returns>The measurement.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the component is not a declared value.</exception>
        public float Convert(Vector3 value) => _component switch
        {
            VectorComponent.X => value.x,
            VectorComponent.Y => value.y,
            VectorComponent.Z => value.z,
            VectorComponent.Magnitude => value.magnitude,
            VectorComponent.SqrMagnitude => value.sqrMagnitude,
            _ => throw new ArgumentOutOfRangeException(nameof(_component), _component, null)
        };
    }
}
