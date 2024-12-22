#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using Aspid.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Text/Text Binder - Localization Enum")]
    public sealed class TextLocalizationEnumMonoBinder : EnumComponentMonoBinder<LocalizeStringEvent, string>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<string, string> _converter;
#else
        private IConverterString _converter;
#endif
        
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif