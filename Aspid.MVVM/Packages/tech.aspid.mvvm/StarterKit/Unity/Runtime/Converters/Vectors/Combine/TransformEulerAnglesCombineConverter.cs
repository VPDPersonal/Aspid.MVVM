#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="Transform"/>'s Euler angles.
    /// </summary>
    /// <remarks>
    /// Unity reconstructs Euler angles from the stored quaternion on read, so the axes taken from
    /// the transform may be an equivalent triple wrapped into 0–360, not the numbers assigned to it.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Transform Euler Angles",
        Tooltip = "Combines a vector with a transform's Euler angles")]
    public sealed class TransformEulerAnglesCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose Euler angles the bound vector is combined with.")]
        [SerializeField] private Transform? _transform;

        [Tooltip("Which space the angles are read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override Component? Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is <see cref="Transform.eulerAngles"/> in
        /// <see cref="Space.World"/> or <see cref="Transform.localEulerAngles"/> in
        /// <see cref="Space.Self"/>, according to the configured space.
        /// </summary>
        protected override Vector3 VectorTo => _transform!.GetEulerAngles(_space);
    }
}
