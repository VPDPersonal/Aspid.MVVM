#nullable enable
using System;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{ScrollRect}"/> that binds <see cref="ScrollRect.verticalNormalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// Scrolling a list to the top or bottom from the ViewModel — the usual reason to reach for a ScrollRect at all — had no binder. Scroll position as a fraction: 0 is one end of the content, 1 the other. Clamped to that range before it is written — a value outside it is silently clamped by Unity anyway, and a non-finite one would leave the content nowhere.
    /// </remarks>
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
