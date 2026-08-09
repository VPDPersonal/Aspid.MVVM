#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="Transform"/>'s current position.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Position Combine", Tooltip = "Combines a vector with a 's current position")]
    public sealed class TransformPositionCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose position the bound vector is combined with.")]
        [SerializeField] private Transform _transform;
        [Tooltip("Which space the position is read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's current position.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetPosition(_space);
    }
}
