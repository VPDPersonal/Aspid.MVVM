#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/UI/Text/Text Binder - EnumGroup")]
    public sealed class TextEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text>
    {
        [Header("Parameters")]
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _selectedValue;
        
        [Header("Converters")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<string, string> _defaultValueConverter;
#else
        private IConverterString _defaultValueConverter;
#endif
        
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<string, string> _selectedValueConverter;
#else
        private IConverterString _selectedValueConverter;
#endif

        protected override void SetDefaultValue(TMP_Text element) =>
            element.text = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(TMP_Text element) =>
            element.text = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}
#endif