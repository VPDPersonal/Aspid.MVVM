#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="Transform"/>'s local scale.
    /// </summary>
    /// <remarks>
    /// Alone among the transform converters in the family this one offers no space choice: Unity's
    /// world-space scale is a read-only approximation, so the reference vector is always the local
    /// scale and the axes the mode leaves unbound come back in the parent's terms, not the world's.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Transform Scale Combine", Tooltip = "Combines a vector with a 's local scale")]
    public sealed class TransformScaleCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose local scale the bound vector is combined with.")]
        [SerializeField] private Transform _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's
        /// <see cref="Transform.localScale"/>.
        /// </summary>
        protected override Vector3 VectorTo => _transform.localScale;
    }
}