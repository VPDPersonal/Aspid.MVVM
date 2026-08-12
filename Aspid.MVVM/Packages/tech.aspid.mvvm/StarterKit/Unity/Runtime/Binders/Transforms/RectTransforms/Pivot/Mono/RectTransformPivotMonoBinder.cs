using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{RectTransform}"/> that binds <see cref="RectTransform.pivot"/>.
    /// </summary>
    /// <remarks>
    /// The point the element rotates and scales around, as a fraction of its own rect. A menu that grows from the
    /// corner it was opened at moves its pivot rather than its position.
    /// <para/>
    /// Values outside 0..1 are legal — that is how an element is stretched past its parent — so only a
    /// non-finite one is refused: the rect is computed from these numbers and one <c>NaN</c> takes the element
    /// off the screen.
    /// </remarks>
    [AddBinderContextMenu(typeof(RectTransform), serializePropertyNames: "m_Pivot")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/RectTransform/RectTransform Binder – Pivot")]
    public class RectTransformPivotMonoBinder : ComponentVector2MonoBinder<RectTransform>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.pivot;
            set
            {
                // Значения вне 0..1 законны — так растягивают элемент за границы родителя. Отбрасывается
                // только нефинитное: rect считается из этих чисел, и один NaN убирает элемент с экрана.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                CachedComponent.pivot = value;
            }
        }
    }
}
