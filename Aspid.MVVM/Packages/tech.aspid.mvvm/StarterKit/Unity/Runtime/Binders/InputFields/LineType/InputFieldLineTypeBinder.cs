#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField, TMP_InputField.LineType}"/> that gets and sets
    /// <see cref="TMP_InputField.lineType"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-LineType-1.1.0.xml" path="doc//member[@name='InputFieldLineTypeBinder']/*" />
    [Serializable]
    public class InputFieldLineTypeBinder : TargetBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        protected override TMP_InputField.LineType Property
        {
            get => Target.lineType;
            set
            {
                Target.lineType = value;
                Target.ForceLabelUpdate();
            }
        }

        /// <inheritdoc/>
        public InputFieldLineTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif