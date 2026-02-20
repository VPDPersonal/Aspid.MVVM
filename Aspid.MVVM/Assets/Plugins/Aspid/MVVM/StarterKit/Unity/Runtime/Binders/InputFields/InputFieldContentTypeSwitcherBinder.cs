#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class InputFieldContentTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        public InputFieldContentTypeSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.ContentType trueValue,
            TMP_InputField.ContentType falseValue,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        protected override void SetValue(TMP_InputField.ContentType value)
        {
            Target.contentType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif