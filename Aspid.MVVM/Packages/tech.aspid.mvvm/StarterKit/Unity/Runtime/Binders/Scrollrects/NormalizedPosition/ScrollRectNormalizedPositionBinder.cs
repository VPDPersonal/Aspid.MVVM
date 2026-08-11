#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetVector2Binder{ScrollRect}"/> that binds <see cref="ScrollRect.normalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// Both axes at once, for the case the two scalar binders cannot express: restoring a remembered
    /// position, or scrolling a grid back to a corner. Each component is clamped to 0..1 separately —
    /// Unity clamps them silently anyway, and a non-finite one would leave the content nowhere.
    /// </remarks>
    [Serializable]
    public class ScrollRectNormalizedPositionBinder : TargetVector2Binder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.normalizedPosition;
            set => Target.normalizedPosition = new Vector2(BinderMath.SafeClamp01(value.x), BinderMath.SafeClamp01(value.y));
        }

        /// <inheritdoc/>
        public ScrollRectNormalizedPositionBinder(
            ScrollRect target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
