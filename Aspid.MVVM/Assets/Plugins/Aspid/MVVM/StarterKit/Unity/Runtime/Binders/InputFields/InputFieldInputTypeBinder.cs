#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class InputFieldInputTypeBinder : TargetBinder<TMP_InputField, TMP_InputField.InputType>
    {
        protected sealed override TMP_InputField.InputType Property
        {
            get => Target.inputType;
            set
            {
                Target.inputType = value;
                Target.ForceLabelUpdate();
            }
        }

        public InputFieldInputTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif