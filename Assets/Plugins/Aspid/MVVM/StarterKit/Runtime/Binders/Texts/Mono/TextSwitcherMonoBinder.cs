#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Text/Text Binder - Switcher")]
    public sealed class TextSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, string>
    {
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
#endif