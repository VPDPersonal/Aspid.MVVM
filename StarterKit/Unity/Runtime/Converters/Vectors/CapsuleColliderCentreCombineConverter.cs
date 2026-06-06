using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="CapsuleCollider"/>'s center point.
    /// </summary>
    [Serializable]
    public sealed class CapsuleColliderCentreCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private CapsuleCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's center point.
        /// </summary>
        protected override Vector3 VectorTo => _collider.center;
    }
}