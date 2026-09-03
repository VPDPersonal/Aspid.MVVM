#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector2CombineConverter"/> that reads the reference vector from a
    /// <see cref="BoxCollider2D"/>'s size.
    /// </summary>
    /// <remarks>
    /// A collider size is unscaled: the transform's scale multiplies it afterward.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Box Collider 2D Size",
        Tooltip = "Combines a 2D vector with a 2D box collider's size")]
    public sealed class BoxCollider2DSizeCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The collider whose size the bound vector is combined with.")]
        [SerializeField] private BoxCollider2D? _collider;

        /// <inheritdoc/>
        protected override Component? Target => _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's <see cref="BoxCollider2D.size"/>.
        /// </summary>
        protected override Vector2 VectorTo => _collider!.size;
    }
}
