#if ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    public class TextLocalizationBinder : TextLocalizationBinderBase, IBinder<string>
    {
        public void SetValue(string value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif