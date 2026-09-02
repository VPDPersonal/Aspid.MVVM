using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{SpriteRenderer}"/> that binds <see cref="Renderer.sortingOrder"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_SortingOrder")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sorting Order")]
    public class SpriteRendererSortingOrderMonoBinder : ComponentIntMonoBinder<SpriteRenderer>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.sortingOrder;
            set => CachedComponent.sortingOrder = value;
        }
    }
}
