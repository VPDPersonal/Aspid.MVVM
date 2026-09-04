using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Renderer.sortingOrder"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_SortingOrder", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sorting Order Enum")]
    public sealed class SpriteRendererSortingOrderEnumMonoBinder : EnumMonoBinder<SpriteRenderer, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.sortingOrder = value;
    }
}
