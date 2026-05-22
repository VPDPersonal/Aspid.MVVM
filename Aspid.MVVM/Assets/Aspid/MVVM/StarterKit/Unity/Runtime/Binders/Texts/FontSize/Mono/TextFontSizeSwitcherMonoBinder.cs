#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{TMP_Text}"/> that switches the <see cref="TMP_Text.fontSize"/>
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – FontSize Switcher")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontSize", SubPath = "Switcher")]
    public sealed class TextFontSizeSwitcherMonoBinder : SwitcherFloatMonoBinder<TMP_Text>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.fontSize"/>.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.fontSize = value;
    }
}
#endif