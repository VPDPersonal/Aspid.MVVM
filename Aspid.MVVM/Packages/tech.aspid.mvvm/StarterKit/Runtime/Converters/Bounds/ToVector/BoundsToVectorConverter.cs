#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads one vector of a bounding box: its middle, its size or its half-size.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Bounds/To Vector",
        Name = "Bounds To Vector",
        Tooltip = "Reads the middle, the size or the half-size of a bounding box")]
    public sealed class BoundsToVectorConverter : IConverter<Bounds, Vector3>
    {
        [Tooltip("Which vector of the box to read.")]
        [SerializeField] private BoundsVector _vector;

        /// <remarks>Default: reading the middle.</remarks>
        public BoundsToVectorConverter() { }

        /// <param name="vector">Which vector of the box to read.</param>
        public BoundsToVectorConverter(BoundsVector vector)
        {
            _vector = vector;
        }

        /// <summary>
        /// Reads the configured vector of the specified box.
        /// </summary>
        /// <param name="value">The box to read.</param>
        /// <returns>
        /// The middle, the size or the half-size, in the space the bounds were measured in. Reports an
        /// error and returns the middle when the configured vector is not a declared
        /// <see cref="BoundsVector"/> value.
        /// </returns>
        public Vector3 Convert(Bounds value)
        {
            return _vector switch
            {
                BoundsVector.Center => value.center,
                BoundsVector.Size => value.size,
                BoundsVector.Extents => value.extents,
                _ => Undeclared(value)
            };
        }

        private Vector3 Undeclared(Bounds value)
        {
            this.LogError(
                problem: $"the vector {_vector.Describe()} is not a declared {nameof(BoundsVector)}",
                consequence: "Returning the middle of the box.");

            return value.center;
        }
    }
}
