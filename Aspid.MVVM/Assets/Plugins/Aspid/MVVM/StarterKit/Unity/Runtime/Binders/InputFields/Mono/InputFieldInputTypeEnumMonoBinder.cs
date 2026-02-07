#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - InputType Enum")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType", SubPath = "Enum")]
    public sealed class InputFieldInputTypeEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        protected override void SetValue(TMP_InputField.InputType value)
        {
            CachedComponent.inputType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif