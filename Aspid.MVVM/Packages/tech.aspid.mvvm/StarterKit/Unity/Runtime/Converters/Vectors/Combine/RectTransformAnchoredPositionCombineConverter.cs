#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="RectTransform"/>'s anchored position.
    /// </summary>
    /// <remarks>
    /// An anchored position is two-dimensional unless the third component is asked for by name, so
    /// the configured space decides whether the reference vector carries the element's depth at all.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Rect Transform Anchored Position",
        Tooltip = "Combines a vector with a rect transform's anchored position")]
    public sealed class RectTransformAnchoredPositionCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The rect transform whose anchored position the bound vector is combined with.")]
        [SerializeField] private RectTransform? _transform;

        [Tooltip("Which space the anchored position is read in. Self has no depth, so Z reads as zero.")]
        [SerializeField] private Space _space = Space.World;

        /// <inheritdoc/>
        protected override Component? Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is
        /// <see cref="RectTransform.anchoredPosition3D"/> in <see cref="Space.World"/> or the
        /// two-component <see cref="RectTransform.anchoredPosition"/> in <see cref="Space.Self"/>,
        /// according to the configured space.
        /// </summary>
        /// <remarks>
        /// In <see cref="Space.Self"/> the reference vector is widened with a zero Z, so a mode that
        /// leaves Z to the reference lands the element at depth zero rather than holding the depth
        /// it was authored with.
        /// </remarks>
        protected override Vector3 VectorTo => _transform!.GetAnchoredPosition(_space);
    }
}
