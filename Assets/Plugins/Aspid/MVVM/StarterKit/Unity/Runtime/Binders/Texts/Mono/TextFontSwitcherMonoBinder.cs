#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder - Font Switcher")]
    [AddPropertyContextMenu(typeof(TMP_Text), "m_text")]
    [AddComponentContextMenu(typeof(TMP_Text),"Add Text Binder/Text Binder - Font Switcher")]
    public sealed class TextFontSwitcherMonoBinder : SwitcherMonoBinder<TMP_Text, TMP_FontAsset>
    {
        protected override void SetValue(TMP_FontAsset value) =>
            CachedComponent.font = value;
    }
}
#endif