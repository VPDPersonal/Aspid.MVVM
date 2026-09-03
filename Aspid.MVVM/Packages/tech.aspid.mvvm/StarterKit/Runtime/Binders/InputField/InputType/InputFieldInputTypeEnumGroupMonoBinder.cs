using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_InputField.inputType"/>
    /// on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – InputType EnumGroup")]
    public sealed class InputFieldInputTypeEnumGroupMonoBinder
        : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField element, TMP_InputField.InputType value)
        {
            element.inputType = value;
            element.ForceLabelUpdate();
        }
    }
}
