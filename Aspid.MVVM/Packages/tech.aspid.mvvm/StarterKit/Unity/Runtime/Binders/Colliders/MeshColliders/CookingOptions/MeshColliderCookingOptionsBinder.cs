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
    /// What the engine is allowed to clean up while building the collision mesh. A project that swaps meshes
    /// at runtime pays for cooking every time, and this is the flag set that decides how much. Writing it
    /// re-cooks the mesh, so it belongs to a quality setting rather than to a per-frame value.
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
