using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="BoxCollider"/>'s size.
    /// </summary>
    [Serializable]
    public sealed class BoxColliderSizeCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private BoxCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's size.
        /// </summary>
        protected override Vector3 VectorTo => _collider.size;
    }
}