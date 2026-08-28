#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget,TProperty}">TargetBinder&lt;TMP_InputField, TMP_InputField.CharacterValidation&gt;</see>
    /// that gets and sets <see cref="TMP_InputField.characterValidation"/>.
    /// </summary>
    /// <include file="XmlExampleDoc-InputField-CharacterValidation-1.1.0.xml" path="doc//member[@name='InputFieldCharacterValidationBinder']/*" />
    [Serializable]
    public class InputFieldCharacterValidationBinder : TargetBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <param name="target">The <see cref="TMP_InputField"/> whose <see cref="TMP_InputField.characterValidation"/> is bound.</param>
        /// <param name="mode">The binding mode. Must not be <see cref="BindMode.TwoWay"/> — the validation mode raises no change event to listen to.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public InputFieldCharacterValidationBinder(TMP_InputField target, IConverter<TMP_InputField.CharacterValidation, TMP_InputField.CharacterValidation> converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <inheritdoc/>
        protected sealed override TMP_InputField.CharacterValidation Property
        {
            get => Target.characterValidation;
            set
            {
                Target.characterValidation = value;
                Target.ForceLabelUpdate();
            }
        }
    }
}
#endif
