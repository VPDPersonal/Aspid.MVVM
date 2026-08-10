#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="BoxCollider"/>'s size.
    /// </summary>
    /// <remarks>
    /// A collider size is unscaled: the transform's scale multiplies it afterwards, so the axes the
    /// mode selects are the authored extents rather than the box the player actually collides with.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Box Collider Size Combine", Tooltip = "Combines a vector with a 's size")]
    public sealed class BoxColliderSizeCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose size the bound vector is combined with.")]
        [SerializeField] private BoxCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's
        /// <see cref="BoxCollider.size"/>. When the collider is not assigned, logs an error
        /// and returns <see cref="Vector3.zero"/>.
        /// </summary>
        protected override Vector3 VectorTo
        {
            get
            {
                if (_collider == null)
                {
                    Debug.LogError($"{nameof(BoxColliderSizeCombineConverter)}: no collider assigned. Using {nameof(Vector3)}.{nameof(Vector3.zero)}.");
                    return Vector3.zero;
                }

                return _collider.size;
            }
        }
    }
}