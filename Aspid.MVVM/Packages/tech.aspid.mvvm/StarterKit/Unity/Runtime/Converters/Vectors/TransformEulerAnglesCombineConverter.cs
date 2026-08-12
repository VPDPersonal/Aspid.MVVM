using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="Transform"/>'s Euler angles.
    /// </summary>
    [Serializable]
    public sealed class TransformEulerAnglesCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's Euler angles.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetEulerAngles(_space);
    }
}