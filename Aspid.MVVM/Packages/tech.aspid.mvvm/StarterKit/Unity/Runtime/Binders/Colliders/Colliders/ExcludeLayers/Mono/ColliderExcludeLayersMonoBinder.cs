using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{Collider}"/> that binds <see cref="Collider.excludeLayers"/>.
    /// </summary>
    /// <remarks>
    /// The other half of the per-collider layer mask: the layers this collider refuses even when the global
    /// matrix allows them. The mask travels as an <see langword="int"/>, which is what
    /// <see cref="LayerMask"/> converts to and from.
    /// </remarks>
    [AddBinderContextMenu(typeof(Collider), serializePropertyNames: "m_ExcludeLayers")]
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Collider Binder – Exclude Layers")]
    public class ColliderExcludeLayersMonoBinder : ComponentIntMonoBinder<Collider>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.excludeLayers;
            set => CachedComponent.excludeLayers = value;
        }
    }
}
