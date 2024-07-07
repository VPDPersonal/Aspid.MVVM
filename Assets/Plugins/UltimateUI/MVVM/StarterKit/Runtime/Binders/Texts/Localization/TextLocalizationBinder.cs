#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    public partial class TextLocalizationBinder : TextLocalizationBinderBase, IBinder<string>
    {
        [BinderLog]
        public void SetValue(string value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif