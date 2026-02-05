using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Layout/Horizontal Or Vertical Layout Group/HorizontalOrVerticalLayoutGroup Binder – Spacing EnumGroup")]
    public sealed class HorizontalOrVerticalLayoutGroupSpacingEnumGroupMonoBinder : EnumGroupMonoBinder<HorizontalOrVerticalLayoutGroup>
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;

        protected override void SetDefaultValue(HorizontalOrVerticalLayoutGroup element) =>
            element.spacing = _defaultValue;

        protected override void SetSelectedValue(HorizontalOrVerticalLayoutGroup element) =>
            element.spacing = _selectedValue;
    }
}