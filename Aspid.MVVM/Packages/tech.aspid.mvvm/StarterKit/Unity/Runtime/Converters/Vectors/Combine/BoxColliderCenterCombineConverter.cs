#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="BoxCollider"/>'s center.
    /// </summary>
    /// <remarks>
    /// A collider center is an offset in the object's own space, not a point in the scene.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Box Collider Center",
        Tooltip = "Combines a vector with a box collider's center point")]
    public sealed class BoxColliderCenterCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose center the bound vector is combined with.")]
        [SerializeField] private BoxCollider? _collider;

        /// <inheritdoc/>
        protected override Component? Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's <see cref="BoxCollider.center"/>.
        /// </summary>
        protected override Vector3 VectorTo => _collider!.center;
    }
}
