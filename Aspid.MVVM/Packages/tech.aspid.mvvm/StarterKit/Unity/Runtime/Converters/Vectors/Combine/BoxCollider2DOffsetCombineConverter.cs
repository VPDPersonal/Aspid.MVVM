#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector2CombineConverter"/> that reads the reference vector from a
    /// <see cref="BoxCollider2D"/>'s offset.
    /// </summary>
    /// <remarks>
    /// A collider offset is a shift in the object's own space, not a point in the scene.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Box Collider 2D Offset",
        Tooltip = "Combines a 2D vector with a 2D box collider's offset")]
    public sealed class BoxCollider2DOffsetCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The collider whose offset the bound vector is combined with.")]
        [SerializeField] private BoxCollider2D? _collider;

        /// <inheritdoc/>
        protected override Component? Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's <see cref="BoxCollider2D.offset"/>.
        /// </summary>
        protected override Vector2 VectorTo => _collider!.offset;
    }
}
