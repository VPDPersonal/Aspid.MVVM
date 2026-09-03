#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="BoxCollider"/>'s size.
    /// </summary>
    /// <remarks>
    /// A collider size is unscaled: the transform's scale multiplies it afterward.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Box Collider Size",
        Tooltip = "Combines a vector with a box collider's size")]
    public sealed class BoxColliderSizeCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose size the bound vector is combined with.")]
        [SerializeField] private BoxCollider? _collider;

        /// <inheritdoc/>
        protected override Component? Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's <see cref="BoxCollider.size"/>.
        /// </summary>
        protected override Vector3 VectorTo => _collider!.size;
    }
}
