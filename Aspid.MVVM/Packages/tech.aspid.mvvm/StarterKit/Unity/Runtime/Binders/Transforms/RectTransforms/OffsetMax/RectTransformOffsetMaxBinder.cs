#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{RectTransform}"/> that binds <see cref="RectTransform.offsetMax"/>.
    /// </summary>
    /// <remarks>
    /// The distance between this corner of the element and the anchor it is pinned to, in pixels. It is what
    /// a stretched panel exposes instead of a size: padding that changes with a safe area, a notch, or a
    /// sidebar that slides in.
    /// <para/>
    /// Negative values are ordinary — they push the element past its parent's edge — so only a non-finite
    /// one is refused.
    /// </remarks>
    [Serializable]
    public class RectTransformOffsetMaxBinder : TargetVector2Binder<RectTransform>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.offsetMax;
            set
            {
                // Отрицательные отступы — норма: так элемент выходит за края родителя.
                if (!BinderMath.IsFinite(value.x) || !BinderMath.IsFinite(value.y)) return;
                Target.offsetMax = value;
            }
        }

        /// <inheritdoc/>
        public RectTransformOffsetMaxBinder(
            RectTransform target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
