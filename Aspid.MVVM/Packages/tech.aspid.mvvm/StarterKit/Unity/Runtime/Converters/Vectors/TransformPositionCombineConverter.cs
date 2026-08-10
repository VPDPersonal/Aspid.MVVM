#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="Transform"/>'s current position.
    /// </summary>
    /// <remarks>
    /// <see cref="TransformPosition2DCombineConverter"/> is the same reading for values that arrive
    /// as a <see cref="Vector2"/>.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Position Combine", Tooltip = "Combines a vector with a 's current position")]
    public sealed class TransformPositionCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose position the bound vector is combined with.")]
        [SerializeField] private Transform _transform;
        [Tooltip("Which space the position is read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is <see cref="Transform.position"/> in
        /// <see cref="Space.World"/> or <see cref="Transform.localPosition"/> in
        /// <see cref="Space.Self"/>, according to the configured space.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetPosition(_space);
    }
}
