using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{SpriteRenderer, int}"/> that sets <see cref="SpriteRenderer.sortingOrder"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(SpriteRenderer), serializePropertyNames: "m_SortingOrder", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/SpriteRenderer/SpriteRenderer Binder – Sorting Order EnumGroup")]
    public sealed class SpriteRendererSortingOrderEnumGroupMonoBinder : EnumGroupMonoBinder<SpriteRenderer, int>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        protected override void SetValue(SpriteRenderer element, int value) =>
            element.sortingOrder = value;
    }
}
