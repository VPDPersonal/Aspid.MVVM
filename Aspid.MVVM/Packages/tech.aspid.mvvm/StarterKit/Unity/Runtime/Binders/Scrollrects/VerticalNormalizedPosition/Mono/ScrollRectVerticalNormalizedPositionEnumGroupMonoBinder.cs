using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{ScrollRect, Single}"/> that sets <see cref="ScrollRect.verticalNormalizedPosition"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Content", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Scroll EnumGroup")]
    public sealed class ScrollRectVerticalNormalizedPositionEnumGroupMonoBinder : EnumGroupMonoBinder<ScrollRect, float>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(ScrollRect element, float value) =>
            element.verticalNormalizedPosition = BinderMath.SafeClamp01(value);
    }
}
