#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="Vector3CombineConverter"/> that reads the reference vector from a
    /// <see cref="SphereCollider"/>'s centre.
    /// </summary>
    /// <remarks>
    /// The centre is the only vector a sphere collider exposes — its radius is a single float and no
    /// combine converter reaches it, so binding the size of one is a job for a float binder.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Sphere Collider Centre Combine", Tooltip = "Combines a vector with a 's center point")]
    public sealed class SphereColliderCentreCombineConverter : Vector3CombineConverter
    {
        [Tooltip("The collider whose centre the bound vector is combined with.")]
        [SerializeField] private SphereCollider _collider;

        /// <summary>
        /// Gets the reference vector to combine with, which is the collider's
        /// <see cref="SphereCollider.center"/>. When the collider is not assigned, logs an error
        /// and returns <see cref="Vector3.zero"/>.
        /// </summary>
        protected override Vector3 VectorTo
        {
            get
            {
                if (_collider == null)
                {
                    Debug.LogError($"{nameof(SphereColliderCentreCombineConverter)}: no collider assigned. Using {nameof(Vector3)}.{nameof(Vector3.zero)}.");
                    return Vector3.zero;
                }

                return _collider.center;
            }
        }
    }
}