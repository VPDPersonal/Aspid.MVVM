using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{ScrollRect, Boolean}"/> that sets <see cref="ScrollRect.vertical"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(ScrollRect), serializePropertyNames: "m_Vertical", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/ScrollRect/ScrollRect Binder – Vertical Enabled EnumGroup")]
    public sealed class ScrollRectVerticalEnumGroupMonoBinder : EnumGroupMonoBinder<ScrollRect, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        protected override void SetValue(ScrollRect element, bool value) =>
            element.vertical = value;
    }
}
