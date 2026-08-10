using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{ScrollRect, Boolean}"/> that sets <see cref="ScrollRect.horizontal"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Horizontal", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Horizontal Enabled Enum")]
    public sealed class ScrollRectHorizontalEnumMonoBinder : EnumMonoBinder<ScrollRect, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        protected override void SetValue(bool value) =>
            CachedComponent.horizontal = value;
    }
}
