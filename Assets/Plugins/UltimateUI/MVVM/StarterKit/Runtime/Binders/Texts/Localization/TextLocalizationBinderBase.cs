#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UltimateUI.MVVM.Unity;
using UnityEngine.Localization.Components;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    public class TextLocalizationBinderBase : MonoBinder
    {
        private LocalizeStringEvent _localizeStringEvent;

        protected LocalizeStringEvent CachedLocalizeStringEvent => _localizeStringEvent ? _localizeStringEvent :
            _localizeStringEvent = GetComponent<LocalizeStringEvent>();
    }
}
#endif