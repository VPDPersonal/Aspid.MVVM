#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector2CombineConverter"/> that reads the reference vector from a
    /// <see cref="Transform"/>'s current position, dropping its depth.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Transform Position 2D",
        Tooltip = "Combines a 2D vector with a transform's current position")]
    public sealed class TransformPosition2DCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The transform whose position the bound vector is combined with.")]
        [SerializeField] private Transform? _transform;

        [Tooltip("Which space the position is read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override Component? Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is <see cref="Transform.position"/> in
        /// <see cref="Space.World"/> or <see cref="Transform.localPosition"/> in
        /// <see cref="Space.Self"/>, according to the configured space, with its Z dropped.
        /// </summary>
        protected override Vector2 VectorTo => _transform!.GetPosition(_space);
    }
}
