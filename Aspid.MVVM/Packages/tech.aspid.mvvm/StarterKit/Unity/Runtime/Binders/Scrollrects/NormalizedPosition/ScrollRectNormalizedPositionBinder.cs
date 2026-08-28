#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{ScrollRect, Vector2}"/> that binds <see cref="ScrollRect.normalizedPosition"/>.
    /// </summary>
    [Serializable]
    public class ScrollRectNormalizedPositionBinder : TargetBinder<ScrollRect, Vector2>, IVector2Binder
    {
        /// <inheritdoc/>
        protected sealed override Vector2 Property
        {
            get => Target.normalizedPosition;
            set => Target.normalizedPosition = new Vector2(this.SafeClamp01(value.x, Target), this.SafeClamp01(value.y, Target));
        }

        /// <inheritdoc/>
        public ScrollRectNormalizedPositionBinder(
            ScrollRect target,
            IConverter<Vector2, Vector2>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
