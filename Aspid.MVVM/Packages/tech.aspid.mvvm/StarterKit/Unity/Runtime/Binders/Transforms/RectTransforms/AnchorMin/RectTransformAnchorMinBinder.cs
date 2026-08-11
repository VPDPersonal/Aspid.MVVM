#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{RectTransform}"/> that binds <see cref="RectTransform.anchorMin"/>.
    /// </summary>
    /// <remarks>
    /// Where the element's lower-left corner is pinned inside its parent, as a fraction. Moving the anchors is how a
    /// panel switches between hugging one edge and stretching across the whole parent — a layout decision a ViewModel
    /// makes when the screen or the mode changes.
    /// <para/>
    /// Values outside 0..1 are legal — that is how an element is stretched past its parent — so only a
    /// non-finite one is refused: the rect is computed from these numbers and one <c>NaN</c> takes the element
    /// off the screen.
    /// </remarks>
    [Serializable]
    public class RectTransformAnchorMinBinder : TargetVector2Binder<RectTransform>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.anchorMin;
            set
            {
                // Значения вне 0..1 законны — так растягивают элемент за границы родителя. Отбрасывается
                // только нефинитное: rect считается из этих чисел, и один NaN убирает элемент с экрана.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.anchorMin = value;
            }
        }

        /// <inheritdoc/>
        public RectTransformAnchorMinBinder(
            RectTransform target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
