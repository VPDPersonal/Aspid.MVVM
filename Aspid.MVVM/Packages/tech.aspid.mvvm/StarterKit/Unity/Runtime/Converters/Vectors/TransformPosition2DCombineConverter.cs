#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a 2D vector with a transform's current position.
    /// </summary>
    /// <remarks>
    /// The 2D reading of <see cref="TransformPositionCombineConverter"/>, for a sprite or a canvas
    /// element whose depth is set once in the scene and never bound.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Position 2D Combine", Tooltip = "Combines a 2D vector with a transform's current position")]
    public sealed class TransformPosition2DCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The transform whose position the bound vector is combined with.")]
        [SerializeField] private Transform _transform;
        [Tooltip("Which space the position is read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override Component Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's position with its
        /// depth dropped.
        /// </summary>
        protected override Vector2 VectorTo => _transform.GetPosition(_space);
    }
}
