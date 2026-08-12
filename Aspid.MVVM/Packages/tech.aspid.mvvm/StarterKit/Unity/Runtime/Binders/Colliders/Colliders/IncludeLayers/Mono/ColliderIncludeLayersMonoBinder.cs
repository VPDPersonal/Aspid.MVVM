using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{Collider}"/> that binds <see cref="Collider.includeLayers"/>.
    /// </summary>
    /// <remarks>
    /// A per-collider layer mask, on top of the global collision matrix — how a ghost passes through walls
    /// while a living character does not, without a second prefab. The mask travels as an <see langword="int"/>,
    /// which is what <see cref="LayerMask"/> converts to and from.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_IncludeLayers")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Include Layers")]
    public class ColliderIncludeLayersMonoBinder : ComponentIntMonoBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.includeLayers;
            set => CachedComponent.includeLayers = value;
        }
    }
}
