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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Localization Switcher")]
    [AddPropertyContextMenu(typeof(LocalizeStringEvent), "m_StringReference")]
    [AddComponentContextMenu(typeof(LocalizeStringEvent),"Add Text Binder/Text Binder - Localization Switcher")]
    public sealed class TextLocalizationSwitcherMonoBinder : SwitcherMonoBinder<LocalizeStringEvent, string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif