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
        /// Sets <see cref="UnityEngine.UI.HorizontalOrVerticalLayoutGroup.spacing"/> to <paramref name="value"/> if it is finite.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value)
        {
            if (!BinderMath.IsFinite(value)) return;
            CachedComponent.spacing = value;
        }
    }
}