#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION && ULTIMATE_UI_UNITY_LOCALIZATION_INTEGRATION
using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Localization
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Localization")]
    public partial class TextLocalizationBinder : TextLocalizationBinderBase, IBinder<string>
    {
        [BinderLog]
        public void SetValue(string value) =>
            CachedLocalizeStringEvent.StringReference.TableEntryReference = value;
    }
}
#endif