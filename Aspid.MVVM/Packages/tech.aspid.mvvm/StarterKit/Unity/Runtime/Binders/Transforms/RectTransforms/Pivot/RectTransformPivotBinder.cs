#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{RectTransform}"/> that binds <see cref="RectTransform.pivot"/>.
    /// </summary>
    /// <remarks>
    /// The point the element rotates and scales around, as a fraction of its own rect. A menu that grows from the
    /// corner it was opened at moves its pivot rather than its position.
    /// <para/>
    /// Values outside 0..1 are legal — that is how an element is stretched past its parent — so only a
    /// non-finite one is refused: the rect is computed from these numbers and one <c>NaN</c> takes the element
    /// off the screen.
    /// </remarks>
    [Serializable]
    public class RectTransformPivotBinder : TargetVector2Binder<RectTransform>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.pivot;
            set
            {
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.pivot = value;
            }
        }

        /// <inheritdoc/>
        public RectTransformPivotBinder(
            RectTransform target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
