using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="Collider.excludeLayers"/>.
    /// </summary>
    /// <remarks>
    /// The mask travels as an <see langword="int"/>, which <see cref="LayerMask"/> converts to and from.
    /// </remarks>
    [GenerateSerializableBinder]
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
