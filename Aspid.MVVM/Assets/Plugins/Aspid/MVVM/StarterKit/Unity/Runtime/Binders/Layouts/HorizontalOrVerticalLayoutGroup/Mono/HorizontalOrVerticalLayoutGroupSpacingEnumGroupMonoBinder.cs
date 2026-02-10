using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(HorizontalOrVerticalLayoutGroup), serializePropertyNames: "m_Spacing", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LayoutGroup/HorizontalOrVertical/HorizontalOrVerticalLayoutGroup Binder – Spacing EnumGroup")]
    public sealed class HorizontalOrVerticalLayoutGroupSpacingEnumGroupMonoBinder : EnumGroupMonoBinder<HorizontalOrVerticalLayoutGroup>
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(HorizontalOrVerticalLayoutGroup element) =>
            element.spacing = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(HorizontalOrVerticalLayoutGroup element) =>
            element.spacing = _defaultValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}