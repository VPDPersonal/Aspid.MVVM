#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION && ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization Switcher")]
    public partial class TextLocalizationSwitcherBinder : TextLocalizationBinderBase, IBinder<bool>
    {
        [Header("Keys")]
        [SerializeField] private string _trueKey;
        [SerializeField] private string _falseKey;

        protected string TrueKey => _trueKey;

        protected string FalseKey => _falseKey;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = GetKey(value);

        protected string GetKey(bool value) =>
            value ? TrueKey : FalseKey;
    }
}
#endif