using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets
    /// <see cref="ScrollRect.horizontalNormalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Scroll Enum")]
    public sealed class ScrollRectHorizontalNormalizedPositionEnumMonoBinder : EnumMonoBinder<ScrollRect, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.horizontalNormalizedPosition = this.SafeClamp01(value);
    }
}
