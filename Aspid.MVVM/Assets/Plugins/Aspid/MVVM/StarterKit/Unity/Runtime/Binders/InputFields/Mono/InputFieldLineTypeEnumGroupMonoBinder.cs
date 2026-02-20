#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - LineType EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "EnumGroup")]
    public sealed class InputFieldLineTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        protected override void SetValue(TMP_InputField element, TMP_InputField.LineType value) 
        {
            element.lineType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif