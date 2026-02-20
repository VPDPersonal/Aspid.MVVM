#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/EnumGroup/Text Binder – Alignment EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_Text), serializePropertyNames: "m_textAlignment", SubPath = "EnumGroup")]
    public sealed class TextAlignmentEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Text, TextAlignmentOptions>
    {
        protected override void SetValue(TMP_Text element, TextAlignmentOptions value) =>
            element.alignment = value;
    }
}
#endif