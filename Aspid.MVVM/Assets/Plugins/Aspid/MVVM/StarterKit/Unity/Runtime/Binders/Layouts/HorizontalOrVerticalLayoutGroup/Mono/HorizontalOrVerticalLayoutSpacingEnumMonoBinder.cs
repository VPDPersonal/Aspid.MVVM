using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/Horizontal Or Vertical Layout Group/HorizontalOrVerticalLayoutGroup Binder – Spacing Enum")]
    public sealed class HorizontalOrVerticalLayoutSpacingEnumMonoBinder : EnumMonoBinder<HorizontalOrVerticalLayoutGroup, float>
    {
        protected override void SetValue(float value) =>
            CachedComponent.spacing = value;
    }
}