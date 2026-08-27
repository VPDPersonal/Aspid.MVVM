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
