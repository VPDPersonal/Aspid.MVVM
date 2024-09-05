#if UNITY_2023_1_OR_NEWER || ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

namespace UltimateUI.MVVM.StarterKit.Binders.Mono.Texts
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Switcher")]
    public sealed class TextSwitcherMonoBinder : Mono.SwitcherMonoBinder<TMP_Text, string>
    {
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
#endif