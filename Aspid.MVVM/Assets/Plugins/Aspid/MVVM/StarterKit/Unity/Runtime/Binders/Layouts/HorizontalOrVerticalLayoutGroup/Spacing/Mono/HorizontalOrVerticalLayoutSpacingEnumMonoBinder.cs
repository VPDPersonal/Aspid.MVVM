using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{HorizontalOrVerticalLayoutGroup}"/> that sets the
    /// <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> property to a value
    /// resolved from the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing Enum")]
    public sealed class HorizontalOrVerticalLayoutSpacingEnumMonoBinder : EnumFloatMonoBinder<HorizontalOrVerticalLayoutGroup>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> directly.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.spacing = value;
    }
}