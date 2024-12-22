#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Text/Text Binder - Localization EnumGroup")]
    public sealed class TextLocalizationEnumGroupMonoBinder : EnumGroupMonoBinder<LocalizeStringEvent>
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
        
        protected override void SetDefaultValue(LocalizeStringEvent element) =>
            element.StringReference.TableEntryReference = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(LocalizeStringEvent element) =>
            element.StringReference.TableEntryReference = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}
#endif