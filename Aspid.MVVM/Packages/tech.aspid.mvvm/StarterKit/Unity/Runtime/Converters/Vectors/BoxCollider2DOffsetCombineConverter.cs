#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a 2D vector with a 2D box collider's offset.
    /// </summary>
    /// <remarks>
    /// Shifting a hitbox along one axis — a crouch that drops it, an attack that pushes it forward —
    /// while the other axis keeps whatever the collider was authored with.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Box Collider 2D Offset Combine", Tooltip = "Combines a 2D vector with a 2D box collider's offset")]
    public sealed class BoxCollider2DOffsetCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The collider whose offset the bound vector is combined with.")]
        [SerializeField] private BoxCollider2D _collider;

        /// <inheritdoc/>
        protected override Component Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's offset.
        /// </summary>
        protected override Vector2 VectorTo => _collider.offset;
    }
}
