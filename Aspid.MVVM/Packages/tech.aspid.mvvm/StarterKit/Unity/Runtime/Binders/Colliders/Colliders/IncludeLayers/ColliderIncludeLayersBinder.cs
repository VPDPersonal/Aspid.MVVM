#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{Collider}"/> that binds <see cref="Collider.includeLayers"/>.
    /// </summary>
    /// <remarks>
    /// A per-collider layer mask, on top of the global collision matrix — how a ghost passes through walls
    /// while a living character does not, without a second prefab. The mask travels as an <see langword="int"/>,
    /// which is what <see cref="LayerMask"/> converts to and from.
    /// </remarks>
    [Serializable]
    public class ColliderIncludeLayersBinder : TargetIntBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.includeLayers;
            set => Target.includeLayers = value;
        }

        /// <inheritdoc/>
        public ColliderIncludeLayersBinder(
            Collider target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
