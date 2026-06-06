#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_InputField, TMP_InputField.LineType}"/> that switches
    /// <see cref="TMP_InputField.lineType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-LineType-1.1.0.xml" path="doc//member[@name='InputFieldLineTypeSwitcherBinder']/*" />
    [Serializable]
    public sealed class InputFieldLineTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        public InputFieldLineTypeSwitcherBinder(
            TMP_InputField target, 
            TMP_InputField.LineType trueValue,
            TMP_InputField.LineType falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.lineType"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.LineType value)
        {
            Target.lineType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif