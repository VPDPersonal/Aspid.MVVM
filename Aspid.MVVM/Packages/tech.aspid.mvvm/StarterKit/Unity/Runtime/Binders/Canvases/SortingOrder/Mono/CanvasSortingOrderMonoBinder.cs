using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder<Canvas>"/> that binds <see cref="Canvas.sortingOrder"/>.
    /// </summary>
    /// <remarks>
    /// Which canvas draws on top. Bringing a panel to the front from the ViewModel had no binder.
    /// </remarks>
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
