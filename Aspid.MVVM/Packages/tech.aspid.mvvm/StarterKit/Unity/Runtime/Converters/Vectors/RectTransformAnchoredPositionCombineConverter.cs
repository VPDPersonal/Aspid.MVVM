using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="RectTransform"/>'s anchored position.
    /// </summary>
    [Serializable]
    public sealed class RectTransformAnchoredPositionCombineConverter : Vector3CombineConverter
    {
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Space _space = Space.World;

        /// <summary>
        /// Gets the reference vector to combine with, which is the rect transform's anchored position.
        /// </summary>
        protected override Vector3 VectorTo => _transform.GetAnchoredPosition(_space);
    }
}