#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;
using UnityEngine.Localization.Components;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Texts.Localization
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization")]
    public partial class TextLocalizationMonoBinder : ComponentMonoBinder<LocalizeStringEvent>, IBinder<string>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterStringToString Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = Converter?.Convert(value) ?? value;
    }
}
#endif