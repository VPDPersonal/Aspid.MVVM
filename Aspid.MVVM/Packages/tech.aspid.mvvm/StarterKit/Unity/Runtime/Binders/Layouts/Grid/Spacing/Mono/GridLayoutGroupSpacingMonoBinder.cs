using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentVector2MonoBinder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// The gap between cells, on both axes. Unlike the cell size it is meaningfully negative — cards dealt
    /// into a hand overlap — so only a non-finite value is refused, and it is refused rather than clamped:
    /// one NaN would put the whole layout at NaN.
    /// </remarks>
    [AddBinderContextMenu(typeof(GridLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/Grid/GridLayoutGroup Binder – Spacing")]
    public class GridLayoutGroupSpacingMonoBinder : ComponentVector2MonoBinder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => CachedComponent.spacing;
            set
            {
                // Отрицательный отступ — не ошибка: так карты в руке кладут с перекрытием.
                // Отбрасывается только нефинитное значение, иначе весь layout уходит в NaN.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                CachedComponent.spacing = value;
            }
        }
    }
}
