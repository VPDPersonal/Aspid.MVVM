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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Localization Enum")]
    [AddPropertyContextMenu(typeof(LocalizeStringEvent), "m_StringReference")]
    [AddComponentContextMenu(typeof(LocalizeStringEvent),"Add Text Binder/Text Binder - Localization Enum")]
    public sealed class TextLocalizationEnumMonoBinder : EnumMonoBinder<LocalizeStringEvent, string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif