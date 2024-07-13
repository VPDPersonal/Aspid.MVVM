#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    public partial class TextLocalizationSwitcherBinder : TextLocalizationBinderBase, IBinder<bool>
    {
        [Header("Keys")]
        [SerializeField] private string _trueKey;
        [SerializeField] private string _falseKey;

        protected string TrueKey => _trueKey;

        protected string FalseKey => _falseKey;
        
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [BinderLog]
#endif
        public void SetValue(bool value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = GetKey(value);

        protected string GetKey(bool value) =>
            value ? TrueKey : FalseKey;
    }
}
#endif