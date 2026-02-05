#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize EnumGroup")]
    public sealed class TextFontSizeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text>
    {
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _selectedValue;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(TMP_Text element) =>
            element.fontSize = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(TMP_Text element) =>
            element.fontSize = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}
#endif