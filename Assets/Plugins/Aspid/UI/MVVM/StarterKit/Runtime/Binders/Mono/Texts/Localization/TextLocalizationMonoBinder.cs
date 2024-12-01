#if ASPID_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization")]
    public partial class TextLocalizationMonoBinder : ComponentMonoBinder<LocalizeStringEvent>, IBinder<string>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<string, string> _converter;
#else
        [SerializeReference] private IConverterStringToString _converter;
#endif
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif