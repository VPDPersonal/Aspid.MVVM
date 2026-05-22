#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField}"/> that implements <see cref="IBinder{TValue}"/> for
    /// <see cref="TMP_InputField.CharacterValidation"/> and sets <see cref="TMP_InputField.characterValidation"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-CharacterValidation-1.1.0.xml" path="doc//member[@name='InputFieldCharacterValidationBinder']/*" />
    [Serializable]
    public class InputFieldCharacterValidationBinder : TargetBinder<TMP_InputField>, IBinder<TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        public InputFieldCharacterValidationBinder(TMP_InputField target, BindMode mode = BindMode.OneWay) 
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Sets <see cref="TMP_InputField.characterValidation"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        public void SetValue(TMP_InputField.CharacterValidation value)
        {
            Target.characterValidation = value;
            Target.ForceLabelUpdate();
        }
    }
}
#endif