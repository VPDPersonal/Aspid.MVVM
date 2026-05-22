#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_InputField, TMP_InputField.InputType}"/> that gets and sets
    /// <see cref="TMP_InputField.inputType"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when first bound, the current
    /// <see cref="TMP_InputField.inputType"/> value is forwarded to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - InputType")]
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
#endif