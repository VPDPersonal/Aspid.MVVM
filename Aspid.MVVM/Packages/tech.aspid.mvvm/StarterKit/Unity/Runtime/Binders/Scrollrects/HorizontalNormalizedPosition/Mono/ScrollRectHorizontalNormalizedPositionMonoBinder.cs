using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder<ScrollRect>"/> that binds <see cref="ScrollRect.horizontalNormalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// The horizontal counterpart. Scroll position as a fraction: 0 is one end of the content, 1 the other. Clamped to that range before it is written — a value outside it is silently clamped by Unity anyway, and a non-finite one would leave the content nowhere.
    /// </remarks>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Scroll")]
    public class ScrollRectHorizontalNormalizedPositionMonoBinder : ComponentFloatMonoBinder<ScrollRect>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.horizontalNormalizedPosition;
            set => CachedComponent.horizontalNormalizedPosition = BinderMath.SafeClamp01(value);
        }
    }
}
