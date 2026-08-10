#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="BoxCollider"/>'s centre.
    /// </summary>
    /// <remarks>
    /// A collider centre is an offset in the object's own space, not a point in the scene, so the
    /// axes the mode leaves to the reference keep the hitbox where it was authored relative to the
    /// GameObject however the object itself moves.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Box Collider Centre Combine", Tooltip = "Combines a vector with a 's center point")]
    public sealed class BoxColliderCentreCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose centre the bound vector is combined with.")]
        [SerializeField] private BoxCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's
        /// <see cref="BoxCollider.center"/>. When the collider is not assigned, logs an error
        /// and returns <see cref="Vector3.zero"/>.
        /// </summary>
        protected override Vector3 VectorTo
        {
            get
            {
                if (_collider == null)
                {
                    Debug.LogError($"{nameof(BoxColliderCentreCombineConverter)}: no collider assigned. Using {nameof(Vector3)}.{nameof(Vector3.zero)}.");
                    return Vector3.zero;
                }

                return _collider.center;
            }
        }
    }
}