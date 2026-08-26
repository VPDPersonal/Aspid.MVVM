#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector2CombineConverter"/> that reads the reference vector from a
    /// <see cref="RectTransform"/>'s anchored position.
    /// </summary>
    /// <remarks>
    /// The anchored position is measured from the anchors, so it is a screen point only while the
    /// anchors sit in the parent's corner.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Rect Transform Anchored Position 2D",
        Tooltip = "Combines a 2D vector with a rect transform's anchored position")]
    public sealed class RectTransformAnchoredPosition2DCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The rect transform whose anchored position the bound vector is combined with.")]
        [SerializeField] private RectTransform? _transform;

        /// <inheritdoc/>
        protected override Component? Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the rect transform's
        /// <see cref="RectTransform.anchoredPosition"/>.
        /// </summary>
        protected override Vector2 VectorTo => _transform!.anchoredPosition;
    }
}
