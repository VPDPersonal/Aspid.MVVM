#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(LocalizeStringEvent), serializePropertyNames: "m_StringReference")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder – Entry EnumGroup")]
    public sealed class LocalizeStringEventEntryEnumGroupMonoBinder : EnumGroupMonoBinder<LocalizeStringEvent>
    {
        [Header("Values")]
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