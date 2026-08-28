#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{ScrollRect}"/> that binds <see cref="ScrollRect.horizontalNormalizedPosition"/>.
    /// </summary>
    [Serializable]
    public class ScrollRectHorizontalNormalizedPositionBinder : TargetFloatBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.horizontalNormalizedPosition;
            set => Target.horizontalNormalizedPosition = this.SafeClamp01(value, Target);
        }

        /// <inheritdoc/>
        public ScrollRectHorizontalNormalizedPositionBinder(
            ScrollRect target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
