#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="SphereCollider"/>'s center.
    /// </summary>
    /// <remarks>
    /// The center is the only vector a sphere collider exposes — its radius is a single float and no
    /// combine converter reaches it, so binding the size of one is a job for a float binder.
    /// A collider center is an offset in the object's own space, not a point in the scene.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Sphere Collider Center",
        Tooltip = "Combines a vector with a sphere collider's center point")]
    public sealed class SphereColliderCenterCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose center the bound vector is combined with.")]
        [SerializeField] private SphereCollider? _collider;

        /// <inheritdoc/>
        protected override Component? Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's <see cref="SphereCollider.center"/>.
        /// </summary>
        protected override Vector3 VectorTo => _collider!.center;
    }
}
