#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField, TMP_InputField.InputType}"/> that gets and sets
    /// <see cref="TMP_InputField.inputType"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-InputType-1.1.0.xml" path="doc//member[@name='InputFieldInputTypeBinder']/*" />
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

        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldInputTypeBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/>.</param>
        public InputFieldInputTypeBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }
    }
}
#endif