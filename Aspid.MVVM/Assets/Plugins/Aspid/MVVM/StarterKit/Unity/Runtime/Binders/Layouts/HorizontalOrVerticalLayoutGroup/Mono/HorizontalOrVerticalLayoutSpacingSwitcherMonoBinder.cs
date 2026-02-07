using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/Horizontal Or Vertical Layout Group/HorizontalOrVerticalLayoutGroup Binder – Spacing Switcher")]
    public sealed class HorizontalOrVerticalLayoutSpacingSwitcherMonoBinder : SwitcherMonoBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.spacing = value;
    }
}