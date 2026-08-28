#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{T1, T2}">SwitcherBinder&lt;TMP_InputField, TMP_InputField.InputType&gt;</see> that switches
    /// <see cref="TMP_InputField.inputType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-InputType-1.1.0.xml" path="doc//member[@name='InputFieldInputTypeSwitcherBinder']/*" />
    [Serializable]
    public sealed class InputFieldInputTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <param name="target">The <see cref="TMP_InputField"/> to bind.</param>
        /// <param name="trueValue">The input type applied when the bound boolean is <see langword="true"/>.</param>
        /// <param name="falseValue">The input type applied when the bound boolean is <see langword="false"/>.</param>
        /// <param name="converter">
        /// An optional converter applied to the selected value before it is forwarded to the target.
        /// Pass <see langword="null"/> to forward the value unchanged.
        /// </param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
        public InputFieldInputTypeSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.InputType trueValue,
            TMP_InputField.InputType falseValue, 
            IConverter<TMP_InputField.InputType, TMP_InputField.InputType> converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.inputType"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(TMP_InputField.InputType value)
        {
            Target.inputType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif