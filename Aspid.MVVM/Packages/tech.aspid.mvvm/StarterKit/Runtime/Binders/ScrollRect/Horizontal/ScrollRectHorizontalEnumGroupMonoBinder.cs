using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="ScrollRect.horizontal"/> on each
    /// element.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Horizontal", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Enabled EnumGroup")]
    public sealed class ScrollRectHorizontalEnumGroupMonoBinder : EnumGroupMonoBinder<ScrollRect, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(ScrollRect element, bool value) =>
            element.horizontal = value;
    }
}
