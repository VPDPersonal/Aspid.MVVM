#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a 2D vector with a rect transform's anchored position.
    /// </summary>
    /// <remarks>
    /// An anchored position is a <see cref="Vector2"/> to begin with, so this is the form the value
    /// arrives in — binding one axis of a panel's placement and leaving the other to the layout no
    /// longer means widening to three dimensions and back.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Rect Transform Anchored Position 2D Combine", Tooltip = "Combines a 2D vector with a rect transform's anchored position")]
    public sealed class RectTransformAnchoredPosition2DCombineConverter : Vector2CombineConverter
    {
        [Tooltip("The rect transform whose anchored position the bound vector is combined with.")]
        [SerializeField] private RectTransform _transform;

        /// <inheritdoc/>
        protected override Component Target => _transform;

        /// <summary>
        /// Gets the reference vector to combine with, which is the rect transform's anchored position.
        /// </summary>
        protected override Vector2 VectorTo => _transform.anchoredPosition;
    }
}
