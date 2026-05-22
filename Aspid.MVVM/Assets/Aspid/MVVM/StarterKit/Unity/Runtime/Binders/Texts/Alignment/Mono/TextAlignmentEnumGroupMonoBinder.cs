#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TMP_Text, TextAlignmentOptions}"/> that sets the <see cref="TMP_Text.alignment"/>
    /// on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/EnumGroup/Text Binder – Alignment EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "EnumGroup")]
    public sealed class TextAlignmentEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_Text.alignment"/> of the specified element.
        /// </summary>
        protected override void SetValue(TMP_Text element, TextAlignmentOptions value) =>
            element.alignment = value;
    }
}
#endif