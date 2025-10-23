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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/LocalizeStringEvent/LocalizeStringEvent Binder - Entry Switcher")]
    [AddPropertyContextMenu(typeof(LocalizeStringEvent), "m_StringReference")]
    [AddComponentContextMenu(typeof(LocalizeStringEvent),"Add LocalizeStringEvent Binder/LocalizeStringEvent Binder - Entry Switcher")]
    public sealed class LocalizeStringEventEntrySwitcherMonoBinder : SwitcherMonoBinder<LocalizeStringEvent, string>
    {
        [Header("Converters")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif