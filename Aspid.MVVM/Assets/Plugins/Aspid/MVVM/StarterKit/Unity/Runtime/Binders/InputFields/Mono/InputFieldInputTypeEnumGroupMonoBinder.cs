#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - InputType EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType", SubPath = "EnumGroup")]
    public sealed class InputFieldInputTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        protected override void SetValue(TMP_InputField element, TMP_InputField.InputType value) 
        {
            element.inputType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif