using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="Transform"/>'s local scale.
    /// </summary>
    [Serializable]
    public sealed class TransformScaleCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private Transform _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's local scale.
        /// </summary>
        protected override Vector3 VectorTo => _transform.localScale;
    }
}