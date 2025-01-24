#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using UnityEngine.Localization.Components;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Localization")]
    public partial class TextLocalizationMonoBinder : ComponentMonoBinder<LocalizeStringEvent>, IBinder<string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = _converter?.Convert(value) ?? value;
    }
}
#endif