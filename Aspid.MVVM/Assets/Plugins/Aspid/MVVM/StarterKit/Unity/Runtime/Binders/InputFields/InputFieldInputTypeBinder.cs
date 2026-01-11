#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class InputFieldInputTypeBinder : TargetBinder<TMP_InputField>, IBinder<TMP_InputField.InputType>
    {
        public InputFieldInputTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        public void SetValue(TMP_InputField.InputType value)
        {
            Target.inputType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif