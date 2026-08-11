#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{Collider}"/> that binds <see cref="Collider.excludeLayers"/>.
    /// </summary>
    /// <remarks>
    /// The other half of the per-collider layer mask: the layers this collider refuses even when the global
    /// matrix allows them. The mask travels as an <see langword="int"/>, which is what
    /// <see cref="LayerMask"/> converts to and from.
    /// </remarks>
    [Serializable]
    public class ColliderExcludeLayersBinder : TargetIntBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.excludeLayers;
            set => Target.excludeLayers = value;
        }

        /// <inheritdoc/>
        public ColliderExcludeLayersBinder(
            Collider target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
