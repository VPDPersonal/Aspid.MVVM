#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class InputFieldCharacterValidationSwitcherBinder : SwitcherBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        public InputFieldCharacterValidationSwitcherBinder(
            TMP_InputField target,
            TMP_InputField.CharacterValidation trueValue,
            TMP_InputField.CharacterValidation falseValue,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        protected override void SetValue(TMP_InputField.CharacterValidation value)
        {
            Target.characterValidation = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif