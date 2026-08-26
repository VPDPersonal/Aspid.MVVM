#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector2CombineConverter"/> that reads the reference vector from a
    /// <see cref="RectTransform"/>'s size delta.
    /// </summary>
    /// <remarks>
    /// A size delta is the difference from the size the anchors give, so it is the element's own
    /// size only while the anchors sit on a point.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/Combine",
        Name = "Rect Transform Size Delta",
        Tooltip = "Combines a 2D vector with a rect transform's size delta")]
    public sealed class RectTransformSizeDeltaCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The rect transform whose size delta the bound vector is combined with.")]
        [SerializeField] private RectTransform? _transform;

        /// <inheritdoc/>
        protected override Component? Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the rect transform's
        /// <see cref="RectTransform.sizeDelta"/>.
        /// </summary>
        protected override Vector2 VectorTo => _transform!.sizeDelta;
    }
}
