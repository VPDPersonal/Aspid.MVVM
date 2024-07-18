#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UltimateUI.MVVM.Unity;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Texts.Localization
{
    public class TextLocalizationBinderBase : MonoBinding
    {
        private LocalizeStringEvent _localizeStringEvent;

        protected LocalizeStringEvent CachedLocalizeStringEvent => _localizeStringEvent ? _localizeStringEvent :
            _localizeStringEvent = GetComponent<LocalizeStringEvent>();
    }
}
#endif