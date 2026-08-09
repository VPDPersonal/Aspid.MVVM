#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="Transform"/>'s Euler angles.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Euler Angles Combine", Tooltip = "Combines a vector with a 's Euler angles")]
    public sealed class TransformEulerAnglesCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose Euler angles the bound vector is combined with.")]
        [SerializeField] private Transform _transform;
        [Tooltip("Which space the angles are read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's Euler angles.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetEulerAngles(_space);
    }
}