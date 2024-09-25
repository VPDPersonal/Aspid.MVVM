#if ASPID_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Texts.Localization
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization Switcher")]
    public sealed class TextLocalizationSwitcherMonoBinder : SwitcherMonoBinder<LocalizeStringEvent, string>
    {
        protected override void SetValue(string value) =>
            CachedComponent.StringReference.TableEntryReference = value;
    }
}
#endif