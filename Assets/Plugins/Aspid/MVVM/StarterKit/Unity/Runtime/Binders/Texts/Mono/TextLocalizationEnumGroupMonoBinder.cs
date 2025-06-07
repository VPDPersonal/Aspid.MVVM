#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Localization EnumGroup")]
    [AddPropertyContextMenu(typeof(LocalizeStringEvent), "m_StringReference")]
    [AddComponentContextMenu(typeof(LocalizeStringEvent),"Add Text Binder/Text Binder - Localization EnumGroup")]
    public sealed class TextLocalizationEnumGroupMonoBinder : EnumGroupMonoBinder<LocalizeStringEvent>
    {
        [Header("Parameters")]
        [SerializeField] private string _defaultValue;
        [SerializeField] private string _selectedValue;
        
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _defaultValueConverter;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _selectedValueConverter;
        
        protected override void SetDefaultValue(LocalizeStringEvent element) =>
            element.StringReference.TableEntryReference = _defaultValueConverter?.Convert(_defaultValue) ?? _defaultValue;

        protected override void SetSelectedValue(LocalizeStringEvent element) =>
            element.StringReference.TableEntryReference = _selectedValueConverter?.Convert(_selectedValue) ?? _selectedValue;
    }
}
#endif