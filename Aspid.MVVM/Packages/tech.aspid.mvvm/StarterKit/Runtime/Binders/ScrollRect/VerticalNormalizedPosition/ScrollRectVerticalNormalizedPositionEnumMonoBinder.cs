using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets
    /// <see cref="ScrollRect.verticalNormalizedPosition"/>.
    /// </summary>
    /// <remarks>
    /// The value is clamped to [0, 1].
    /// </remarks>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Scroll Enum")]
    public sealed class ScrollRectVerticalNormalizedPositionEnumMonoBinder : EnumMonoBinder<ScrollRect, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.verticalNormalizedPosition = this.SafeClamp01(value);
    }
}
