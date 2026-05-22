#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TMP_InputField, TMP_InputField.CharacterValidation}"/> that gets and sets
    /// <see cref="TMP_InputField.characterValidation"/>.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when first bound, the current
    /// <see cref="TMP_InputField.characterValidation"/> value is forwarded to the ViewModel.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - CharacterValidation")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_CharacterValidation")]
    public class InputFieldCharacterValidationMonoBinder : ComponentMonoBinder<TMP_InputField, TMP_InputField.CharacterValidation>
    {
        /// <inheritdoc/>
        protected sealed override TMP_InputField.CharacterValidation Property
        {
            get => CachedComponent.characterValidation;
            set
            {
                CachedComponent.characterValidation = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}
#endif