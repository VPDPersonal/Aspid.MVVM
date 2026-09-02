using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement,TValue}">EnumGroupMonoBinder&lt;HorizontalOrVerticalLayoutGroup, float&gt;</see> that sets the
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property on each element
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing EnumGroup")]
    public sealed class HorizontalOrVerticalLayoutSpacingEnumGroupMonoBinder : EnumGroupMonoBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        /// <summary>
        /// Sets <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> on <paramref name="element"/>
        /// to <paramref name="value"/> if it is finite.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(HorizontalOrVerticalLayoutGroup element, float value)
        {
            if (!this.RequireFinite(value)) return;
            element.spacing = value;
        }
    }
}