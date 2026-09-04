using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="ScrollRect.vertical"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Vertical", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Enabled EnumGroup")]
    public sealed class ScrollRectVerticalEnumGroupMonoBinder : EnumGroupMonoBinder<ScrollRect, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(ScrollRect element, bool value) =>
            element.vertical = value;
    }
}
