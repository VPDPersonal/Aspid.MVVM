using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;TMP_InputField, TMP_InputField.InputType&gt;</see> that gets and sets
    /// <see cref="TMP_InputField.inputType"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – InputType")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_InputType")]
    public class InputFieldInputTypeMonoBinder : ComponentMonoBinder<TMP_InputField, TMP_InputField.InputType>
    {
        /// <inheritdoc/>
        protected sealed override TMP_InputField.InputType Property
        {
            get => CachedComponent.inputType;
            set
            {
                CachedComponent.inputType = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}