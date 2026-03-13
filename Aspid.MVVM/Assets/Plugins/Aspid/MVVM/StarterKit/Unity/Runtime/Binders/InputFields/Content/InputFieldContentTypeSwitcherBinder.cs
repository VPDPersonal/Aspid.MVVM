#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_InputField, TMP_InputField.ContentType}"/> that switches
    /// <see cref="TMP_InputField.contentType"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-Content-1.1.0.xml" path="doc//member[@name='InputFieldContentTypeSwitcherBinder']/*" />
    [Serializable]
    public sealed class InputFieldContentTypeSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <inheritdoc/>
        public InputFieldContentTypeSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.ContentType trueValue,
            TMP_InputField.ContentType falseValue,
            BindMode mode = BindMode.OneWay) 
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.contentType"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.ContentType value)
        {
            Target.contentType = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif