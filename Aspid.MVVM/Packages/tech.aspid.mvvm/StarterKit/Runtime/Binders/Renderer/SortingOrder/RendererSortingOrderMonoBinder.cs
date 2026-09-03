using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="Renderer.sortingOrder"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(Renderer), serializePropertyNames: "m_SortingOrder")]
    [AddComponentMenu("Aspid/MVVM/Binders/Renderer/Renderer Binder – Sorting Order")]
    public class RendererSortingOrderMonoBinder : ComponentIntMonoBinder<Renderer>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.sortingOrder;
            set => CachedComponent.sortingOrder = value;
        }
    }
}
