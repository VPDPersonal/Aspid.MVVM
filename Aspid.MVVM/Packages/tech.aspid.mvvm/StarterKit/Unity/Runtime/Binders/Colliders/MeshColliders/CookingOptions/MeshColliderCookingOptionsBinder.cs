#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{T1, T2}">TargetBinder&lt;MeshCollider, MeshColliderCookingOptions&gt;</see> that binds
    /// <see cref="MeshCollider.cookingOptions"/>.
    /// </summary>
    /// <remarks>
    /// Writing this re-cooks the mesh; avoid setting it on a per-frame basis.
    /// </remarks>
    [Serializable]
    public class MeshColliderCookingOptionsBinder : TargetBinder<MeshCollider, MeshColliderCookingOptions>
    {
        /// <inheritdoc/>
        protected sealed override MeshColliderCookingOptions Property
        {
            get => Target.cookingOptions;
            set => Target.cookingOptions = value;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/> — the property raises no change event to listen to.</exception>
        public MeshColliderCookingOptionsBinder(MeshCollider target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}
