using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="TMP_InputField.caretPosition"/>.
    /// </summary>
    /// <remarks>
    /// Clamped to the text length. TMP keeps a caret position only while the field is focused.
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
