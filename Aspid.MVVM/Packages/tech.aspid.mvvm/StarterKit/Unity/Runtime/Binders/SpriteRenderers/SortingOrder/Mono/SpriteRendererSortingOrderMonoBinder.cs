using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder<SpriteRenderer>"/> that binds <see cref="SpriteRenderer.sortingOrder"/>.
    /// </summary>
    /// <remarks>
    /// Draw order inside a sorting layer — the usual way a 2D scene expresses depth.
    /// </remarks>
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
