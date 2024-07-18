#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Texts.Localization
{
    public partial class TextLocalizationBinder : TextLocalizationBinderBase, ITargetBinding<string>
    {
#if !ULTIMATE_UI_MVVM_STARTER_KIT_BINDER_LOG_GENERATOR_DISABLED
        [Unity.BindingLog]
#endif
        public void SetValue(string value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif