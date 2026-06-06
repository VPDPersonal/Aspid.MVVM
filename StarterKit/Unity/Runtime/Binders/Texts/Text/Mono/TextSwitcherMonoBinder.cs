#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherStringMonoBinder{TMP_Text}"/> that switches the <see cref="TMP_Text.text"/>
    /// between two string values based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Text Switcher")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_text", SubPath = "Switcher")]
    public sealed class TextSwitcherMonoBinder : SwitcherStringMonoBinder<TMP_Text>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.text"/>.
        /// </summary>
        protected override void SetValue(string value) =>
            CachedComponent.text = value;
    }
}
#endif