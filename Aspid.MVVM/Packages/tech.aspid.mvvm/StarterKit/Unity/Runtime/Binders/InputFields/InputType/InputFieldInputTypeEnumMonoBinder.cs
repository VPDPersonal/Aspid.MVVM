using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;TMP_InputField, TMP_InputField.InputType&gt;</see> that sets
    /// <see cref="TMP_InputField.inputType"/> based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – InputType Enum")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType", SubPath = "Enum")]
    public sealed class InputFieldInputTypeEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="TMP_InputField.inputType"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.InputType value)
        {
            CachedComponent.inputType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}