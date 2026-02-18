#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class InputFieldContentTypeBinder : TargetBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        protected sealed override TMP_InputField.ContentType Property
        {
            get => Target.contentType;
            set
            {
                Target.contentType = value;
                Target.ForceLabelUpdate();
            }
        }

        public InputFieldContentTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif