#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

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
    /// <see cref="RectTransformAnchoredPosition2DCombineConverter"/> is the same reading for values
    /// that arrive as a <see cref="Vector2"/> and never had a depth to lose.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Rect Transform Anchored Position Combine", Tooltip = "Combines a vector with a 's anchored position")]
    public sealed class RectTransformAnchoredPositionCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The rect transform whose anchored position the bound vector is combined with.")]
        [SerializeField] private RectTransform _transform;
        [Tooltip("Which space the anchored position is read in.")]
        [SerializeField] private Space _space = Space.World;

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
        protected override Vector3 VectorTo => _transform.GetAnchoredPosition(_space);
    }
}