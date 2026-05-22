#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TMP_Text, TextAlignmentOptions}"/> that sets the <see cref="TMP_Text.alignment"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/Text Binder – Alignment Enum")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "Enum")]
    public sealed class TextAlignmentEnumMonoBinder : EnumMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.alignment"/>.
        /// </summary>
        protected override void SetValue(TextAlignmentOptions value) =>
            CachedComponent.alignment = value;
    }
}
#endif