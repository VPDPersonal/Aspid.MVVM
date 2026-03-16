#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TMP_Text, TMP_FontAsset}"/> that sets the <see cref="TMP_Text.font"/>
    /// property based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Font Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_fontAsset", SubPath = "Enum")]
    public sealed class TextFontEnumMonoBinder : EnumMonoBinder<TMP_Text, TMP_FontAsset>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.font"/>.
        /// </summary>
        protected override void SetValue(TMP_FontAsset value) =>
            CachedComponent.font = value;
    }
}
#endif