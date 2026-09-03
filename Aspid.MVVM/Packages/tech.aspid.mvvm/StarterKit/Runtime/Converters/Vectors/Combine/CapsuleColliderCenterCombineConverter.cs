#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="CapsuleCollider"/>'s center.
    /// </summary>
    /// <remarks>
    /// The center is the only vector a capsule collider exposes, its height and radius are single
    /// floats, and which axis the capsule runs along is chosen by
    /// <see cref="CapsuleCollider.direction"/>, not by the mode configured here.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Capsule Collider Center",
        Tooltip = "Combines a vector with a capsule collider's center point")]
    public sealed class CapsuleColliderCenterCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose center the bound vector is combined with.")]
        [SerializeField] private CapsuleCollider? _collider;

        /// <inheritdoc/>
        protected override Component? Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's <see cref="CapsuleCollider.center"/>.
        /// </summary>
        protected override Vector3 VectorTo => _collider!.center;
    }
}
