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
        protected sealed override float Property
        {
            get => Target.verticalNormalizedPosition;
            set => Target.verticalNormalizedPosition = BinderMath.SafeClamp01(value);
        }

        /// <inheritdoc/>
        public ScrollRectVerticalNormalizedPositionBinder(
            ScrollRect target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
