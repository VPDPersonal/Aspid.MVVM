#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{T1, T2}">SwitcherBinder&lt;TMP_InputField, TMP_InputField.LineType&gt;</see> that switches
    /// <see cref="TMP_InputField.lineType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-LineType-1.1.0.xml" path="doc//member[@name='InputFieldLineTypeSwitcherBinder']/*" />
    [Serializable]
    public sealed class InputFieldLineTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="mode"/> is neither <see cref="BindMode.OneWay"/> nor <see cref="BindMode.OneTime"/>.</exception>
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
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(TMP_InputField.LineType value)
        {
            Target.lineType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif