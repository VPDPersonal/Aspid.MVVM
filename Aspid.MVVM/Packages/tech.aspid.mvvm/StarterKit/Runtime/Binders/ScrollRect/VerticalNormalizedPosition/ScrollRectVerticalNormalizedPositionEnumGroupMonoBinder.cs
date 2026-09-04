using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets
    /// <see cref="ScrollRect.verticalNormalizedPosition"/> on each element.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Scroll EnumGroup")]
    public sealed class ScrollRectVerticalNormalizedPositionEnumGroupMonoBinder
        : EnumGroupMonoBinder<ScrollRect, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(ScrollRect element, float value) =>
            element.verticalNormalizedPosition = this.SafeClamp01(value);
    }
}
