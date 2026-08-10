#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a 2D vector with a rect transform's size delta.
    /// </summary>
    /// <remarks>
    /// A bar that grows in width while its height stays with the layout: the X selection does the
    /// work and the Y comes back off the element itself, so nothing has to know the authored height.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Rect Transform Size Delta Combine", Tooltip = "Combines a 2D vector with a rect transform's size delta")]
    public sealed class RectTransformSizeDeltaCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The rect transform whose size delta the bound vector is combined with.")]
        [SerializeField] private RectTransform _transform;

        /// <inheritdoc/>
        protected override Component Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the rect transform's size delta.
        /// </summary>
        protected override Vector2 VectorTo => _transform.sizeDelta;
    }
}
