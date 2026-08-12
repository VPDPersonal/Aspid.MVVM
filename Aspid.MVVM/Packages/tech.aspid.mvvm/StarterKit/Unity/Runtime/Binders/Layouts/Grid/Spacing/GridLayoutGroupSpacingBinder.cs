#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{GridLayoutGroup}"/> that binds <see cref="GridLayoutGroup.spacing"/>.
    /// </summary>
    /// <remarks>
    /// The gap between cells, on both axes. Unlike the cell size it is meaningfully negative — cards dealt
    /// into a hand overlap — so only a non-finite value is refused, and it is refused rather than clamped:
    /// one NaN would put the whole layout at NaN.
    /// </remarks>
    [Serializable]
    public class GridLayoutGroupSpacingBinder : TargetVector2Binder<GridLayoutGroup>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.spacing;
            set
            {
                // Отрицательный отступ — не ошибка: так карты в руке кладут с перекрытием.
                // Отбрасывается только нефинитное значение, иначе весь layout уходит в NaN.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.spacing = value;
            }
        }

        /// <inheritdoc/>
        public GridLayoutGroupSpacingBinder(
            GridLayoutGroup target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
