#if ASPID_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;
using UnityEngine.Localization.Components;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Texts.Localization
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization")]
    public partial class TextLocalizationMonoBinder : ComponentMonoBinder<LocalizeStringEvent>, IBinder<string>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterStringToString Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = Converter?.Convert(value) ?? value;
    }
}
#endif