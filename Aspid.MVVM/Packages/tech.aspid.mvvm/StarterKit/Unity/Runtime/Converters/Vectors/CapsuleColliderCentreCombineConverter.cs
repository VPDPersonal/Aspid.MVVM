#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Combines a vector with a <see cref="CapsuleCollider"/>'s center point.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Capsule Collider Centre Combine", Tooltip = "Combines a vector with a 's center point")]
    public sealed class CapsuleColliderCentreCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose centre the bound vector is combined with.")]
        [SerializeField] private CapsuleCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's center point.
        /// When the collider is not assigned, logs an error and returns <see cref="Vector3.zero"/>.
        /// </summary>
        protected override Vector3 VectorTo
        {
            get
            {
                if (_collider == null)
                {
                    Debug.LogError($"{nameof(CapsuleColliderCentreCombineConverter)}: no collider assigned. Using {nameof(Vector3)}.{nameof(Vector3.zero)}.");
                    return Vector3.zero;
                }

                return _collider.center;
            }
        }
    }
}