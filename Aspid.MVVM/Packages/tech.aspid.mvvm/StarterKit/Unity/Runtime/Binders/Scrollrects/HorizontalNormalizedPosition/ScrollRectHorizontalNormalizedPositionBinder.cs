#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder<ScrollRect>"/> that binds <see cref="ScrollRect.horizontalNormalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// The horizontal counterpart. Scroll position as a fraction: 0 is one end of the content, 1 the other. Clamped to that range before it is written — a value outside it is silently clamped by Unity anyway, and a non-finite one would leave the content nowhere.
    /// </remarks>
    [Serializable]
    public class ScrollRectHorizontalNormalizedPositionBinder : TargetFloatBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.horizontalNormalizedPosition;
            set => Target.horizontalNormalizedPosition = BinderMath.SafeClamp01(value);
        }

        /// <inheritdoc/>
        public ScrollRectHorizontalNormalizedPositionBinder(
            ScrollRect target,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
