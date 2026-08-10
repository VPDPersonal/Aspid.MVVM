using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{SpriteRenderer, int}"/> that sets <see cref="SpriteRenderer.sortingOrder"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_SortingOrder", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sorting Order Enum")]
    public sealed class SpriteRendererSortingOrderEnumMonoBinder : EnumMonoBinder<SpriteRenderer, int>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        protected override void SetValue(int value) =>
            CachedComponent.sortingOrder = value;
    }
}
