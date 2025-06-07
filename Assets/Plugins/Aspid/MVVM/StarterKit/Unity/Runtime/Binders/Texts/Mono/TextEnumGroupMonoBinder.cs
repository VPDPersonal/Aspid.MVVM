#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(TMP_Text), "m_text")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Text EnumGroup")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - Text EnumGroup")]
    public sealed class TextEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text>
    {
        [Header("Parameters")]
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;

        protected override void SetDefaultValue(TMP_Text element) =>
            element.text = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(TMP_Text element) =>
            element.text = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}
#endif