#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class InputFieldLineTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.LineType>
    {
        public InputFieldLineTypeSwitcherBinder(
            TMP_InputField target, 
            TMP_InputField.LineType trueValue,
            TMP_InputField.LineType falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        protected override void SetValue(TMP_InputField.LineType value)
        {
            Target.lineType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif