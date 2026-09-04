using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Renderer.sortingOrder"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_SortingOrder", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sorting Order EnumGroup")]
    public sealed class SpriteRendererSortingOrderEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(SpriteRenderer element, int value) =>
            element.sortingOrder = value;
    }
}
