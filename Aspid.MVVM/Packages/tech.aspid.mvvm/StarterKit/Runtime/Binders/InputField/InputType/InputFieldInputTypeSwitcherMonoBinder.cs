using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_InputField.inputType"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – InputType Switcher")]
    public sealed class InputFieldInputTypeSwitcherMonoBinder
        : SwitcherMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField.InputType value)
        {
            CachedComponent.inputType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
