#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.caretPosition"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to the text length: an out-of-range index draws no caret.
    /// <para/>
    /// The field keeps a caret position only while focused; writing one to an unfocused field does nothing
    /// (TMP reports zero).
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – CaretPosition")]
    public class InputFieldCaretPositionMonoBinder : ComponentIntMonoBinder<TMP_InputField>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.caretPosition;
            set => CachedComponent.caretPosition = Mathf.Clamp(value, 0, CachedComponent.text?.Length ?? 0);
        }
    }
}
#endif
