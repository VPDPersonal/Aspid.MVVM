using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{RectTransform}"/> that binds <see cref="RectTransform.anchorMax"/>.
    /// </summary>
    /// <remarks>
    /// Where the element's upper-right corner is pinned inside its parent, as a fraction. Together with the minimum
    /// anchor it decides whether the element keeps a size or stretches with its parent.
    /// <para/>
    /// Values outside 0..1 are legal — that is how an element is stretched past its parent — so only a
    /// non-finite one is refused: the rect is computed from these numbers and one <c>NaN</c> takes the element
    /// off the screen.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_AnchorMax")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – AnchorMax")]
    public class RectTransformAnchorMaxMonoBinder : ComponentVector2MonoBinder<RectTransform>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.anchorMax;
            set
            {
                // Значения вне 0..1 законны — так растягивают элемент за границы родителя. Отбрасывается
                // только нефинитное: rect считается из этих чисел, и один NaN убирает элемент с экрана.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                CachedComponent.anchorMax = value;
            }
        }
    }
}
