using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{Canvas}"/> that binds <see cref="Canvas.sortingOrder"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(Canvas), serializePropertyNames: "m_SortingOrder")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas/Canvas Binder – Sorting Order")]
    public class CanvasSortingOrderMonoBinder : ComponentIntMonoBinder<Canvas>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.sortingOrder;
            set => CachedComponent.sortingOrder = value;
        }
    }
}
