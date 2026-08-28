#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{ScrollRect}"/> that binds <see cref="ScrollRect.verticalNormalizedPosition"/>.
    /// </summary>
    [Serializable]
    public class ScrollRectVerticalNormalizedPositionBinder : TargetFloatBinder<ScrollRect>
    {
        /// <inheritdoc/>
        public ScrollRectVerticalNormalizedPositionBinder(
            ScrollRect target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }

        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.verticalNormalizedPosition;
            set => Target.verticalNormalizedPosition = this.SafeClamp01(value, Target);
        }
    }
}
