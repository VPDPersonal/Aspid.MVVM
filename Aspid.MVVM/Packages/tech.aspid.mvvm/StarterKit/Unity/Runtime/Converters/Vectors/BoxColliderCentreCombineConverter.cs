using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="BoxCollider"/>'s center point.
    /// </summary>
    [Serializable]
    public sealed class BoxColliderCentreCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose current value fills the components the bound value does not supply.")]
        [SerializeField] private BoxCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's center point.
        /// </summary>
        protected override Vector3 VectorTo => _collider.center;
    }
}