#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="Transform"/>'s Euler angles.
    /// </summary>
    /// <remarks>
    /// Unity stores rotation as a quaternion and reconstructs the Euler angles on read, so the axes
    /// the mode takes from the transform are not guaranteed to be the numbers that were assigned to
    /// it — an equivalent triple wrapped into the 0–360 range. Bind through this converter to hold a
    /// rotation, not to round-trip an authored angle.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Euler Angles Combine", Tooltip = "Combines a vector with a 's Euler angles")]
    public sealed class TransformEulerAnglesCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose Euler angles the bound vector is combined with.")]
        [SerializeField] private Transform _transform;
        [Tooltip("Which space the angles are read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is <see cref="Transform.eulerAngles"/> in
        /// <see cref="Space.World"/> or <see cref="Transform.localEulerAngles"/> in
        /// <see cref="Space.Self"/>, according to the configured space.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetEulerAngles(_space);
    }
}