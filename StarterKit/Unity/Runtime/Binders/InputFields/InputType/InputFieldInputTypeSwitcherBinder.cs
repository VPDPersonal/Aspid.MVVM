#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_InputField, TMP_InputField.InputType}"/> that switches
    /// <see cref="TMP_InputField.inputType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-InputType-1.1.0.xml" path="doc//member[@name='InputFieldInputTypeSwitcherBinder']/*" />
    [Serializable]
    public sealed class InputFieldInputTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InputFieldInputTypeSwitcherBinder"/>.
        /// </summary>
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="trueValue">The input type applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The input type applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/>.</param>
        public InputFieldInputTypeSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.InputType trueValue,
            TMP_InputField.InputType falseValue, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.inputType"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.InputType value)
        {
            Target.inputType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif