#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reduces a vector to its direction.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Normalize",
        Tooltip = "Reduces a vector to its direction")]
    public sealed class VectorNormalizeConverter :
        IConverter<Vector2, Vector2>,
        IConverter<Vector3, Vector3>,
        IConverter<Vector4, Vector4>
    {
        /// <summary>
        /// Normalizes the specified vector.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>
        /// The unit vector pointing the same way, or zero for an input no longer than 1e-5, the floor
        /// Unity's own <c>normalized</c> uses instead of producing a NaN.
        /// </returns>
        public Vector3 Convert(Vector3 value) =>
            value.normalized;

        Vector2 IConverter<Vector2, Vector2>.Convert(Vector2 value) =>
            value.normalized;

        Vector4 IConverter<Vector4, Vector4>.Convert(Vector4 value) =>
            value.normalized;
    }
}
