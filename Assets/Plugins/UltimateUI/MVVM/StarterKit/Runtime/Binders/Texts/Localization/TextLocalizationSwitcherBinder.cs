#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    public class TextLocalizationSwitcherBinder : TextLocalizationBinderBase, IBinder<bool>
    {
        [Header("Keys")]
        [SerializeField] private string _trueKey;
        [SerializeField] private string _falseKey;

        protected string TrueKey => _trueKey;

        protected string FalseKey => _falseKey;
        
        public void SetValue(bool value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = GetKey(value);

        protected string GetKey(bool value) =>
            value ? TrueKey : FalseKey;
    }
}
#endif