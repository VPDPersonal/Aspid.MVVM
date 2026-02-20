#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class InputFieldLineTypeBinder : TargetBinder<TMP_InputField, TMP_InputField.LineType>
    {
        protected override TMP_InputField.LineType Property
        {
            get => Target.lineType;
            set
            {
                Target.lineType = value;
                Target.ForceLabelUpdate();
            }
        }

        public InputFieldLineTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif