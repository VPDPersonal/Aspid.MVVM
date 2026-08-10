#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a 2D vector with a 2D box collider's size.
    /// </summary>
    /// <remarks>
    /// The 2D physics counterpart of <see cref="BoxColliderSizeCombineConverter"/>: a hitbox that
    /// widens with a charge value while its height stays as authored.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Box Collider 2D Size Combine", Tooltip = "Combines a 2D vector with a 2D box collider's size")]
    public sealed class BoxCollider2DSizeCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The collider whose size the bound vector is combined with.")]
        [SerializeField] private BoxCollider2D _collider;

        /// <inheritdoc/>
        protected override Component Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's size.
        /// </summary>
        protected override Vector2 VectorTo => _collider.size;
    }
}
