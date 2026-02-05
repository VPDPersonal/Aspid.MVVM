#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class InputFieldInputTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.InputType>
    {
        public InputFieldInputTypeSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.InputType trueValue,
            TMP_InputField.InputType falseValue, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        protected override void SetValue(TMP_InputField.InputType value)
        {
            Target.inputType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif