#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="Transform"/>'s local scale.
    /// </summary>
    /// <remarks>
    /// The reference vector is always the local scale — Unity's world-space scale is a read-only
    /// approximation — so unbound axes come back in the parent's terms.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Transform Scale",
        Tooltip = "Combines a vector with a transform's local scale")]
    public sealed class TransformScaleCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The transform whose local scale the bound vector is combined with.")]
        [SerializeField] private Transform? _transform;

        /// <inheritdoc/>
        protected override Component? Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the transform's
        /// <see cref="Transform.localScale"/>.
        /// </summary>
        protected override Vector3 VectorTo => _transform!.localScale;
    }
}
