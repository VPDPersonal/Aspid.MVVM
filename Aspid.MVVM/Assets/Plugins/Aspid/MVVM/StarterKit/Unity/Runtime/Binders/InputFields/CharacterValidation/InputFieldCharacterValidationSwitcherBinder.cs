#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherBinder{TMP_InputField, TMP_InputField.CharacterValidation}"/> that switches
    /// <see cref="TMP_InputField.characterValidation"/> between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-CharacterValidation-1.1.0.xml" path="doc//member[@name='InputFieldCharacterValidationSwitcherBinder']/*" />
    [Serializable]
    public sealed class InputFieldCharacterValidationSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        public InputFieldCharacterValidationSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.CharacterValidation trueValue,
            TMP_InputField.CharacterValidation falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when applying the selected value to <see cref="TMP_InputField.characterValidation"/>.
        /// Sets the value and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.CharacterValidation value)
        {
            Target.characterValidation = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif