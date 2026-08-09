#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="Transform"/>'s local scale.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Scale Combine", Tooltip = "Combines a vector with a 's local scale")]
    public sealed class TransformScaleCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose local scale the bound vector is combined with.")]
        [SerializeField] private Transform _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's local scale.
        /// </summary>
        protected override Vector3 VectorTo => _transform.localScale;
    }
}