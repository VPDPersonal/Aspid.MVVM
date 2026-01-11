#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - InputType")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType")]
    public partial class InputFieldInputTypeMonoBinder : ComponentMonoBinder<TMP_InputField>, IBinder<TMP_InputField.InputType>
    {
        [BinderLog]
        public void SetValue(TMP_InputField.InputType value)
        {
            CachedComponent.inputType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif