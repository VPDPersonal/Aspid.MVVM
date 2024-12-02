#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization Enum")]
    public sealed class TextLocalizationEnumMonoBinder : EnumMonoBinder<LocalizeStringEvent, string>
    {
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif