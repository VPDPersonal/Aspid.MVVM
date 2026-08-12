using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{ScrollRect, Single}"/> that sets <see cref="ScrollRect.horizontalNormalizedPosition"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Scroll Enum")]
    public sealed class ScrollRectHorizontalNormalizedPositionEnumMonoBinder : EnumMonoBinder<ScrollRect, float>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.horizontalNormalizedPosition = BinderMath.SafeClamp01(value);
    }
}
