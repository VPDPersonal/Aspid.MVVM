#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="RectTransform"/>'s anchored position.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Rect Transform Anchored Position Combine", Tooltip = "Combines a vector with a 's anchored position")]
    public sealed class RectTransformAnchoredPositionCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The rect transform whose anchored position the bound vector is combined with.")]
        [SerializeField] private RectTransform _transform;
        [Tooltip("Which space the anchored position is read in.")]
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is the rect transform's anchored position.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetAnchoredPosition(_space);
    }
}