#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TMP_Text, TMP_FontAsset}"/> that sets the <see cref="TMP_Text.font"/>
    /// property on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/EnumGroup/Text Binder – Font EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset", SubPath = "EnumGroup")]
    public sealed class TextFontEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.font"/> of the specified element.
        /// </summary>
        protected override void SetValue(TMP_Text element, TMP_FontAsset value) =>
            element.font = value;
    }
}
#endif