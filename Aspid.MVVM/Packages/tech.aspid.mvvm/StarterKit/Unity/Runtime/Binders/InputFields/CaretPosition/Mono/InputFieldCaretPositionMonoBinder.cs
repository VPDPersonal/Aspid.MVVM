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
    /// Where the cursor sits. A field the ViewModel just filled leaves the caret at the start, which puts the
    /// next keystroke in front of the text the user is meant to append; sending the length puts it at the end.
    /// Clamped to the text that is actually there — Unity accepts an index past the end and then draws the
    /// caret nowhere.
    /// <para/>
    /// The field keeps a caret position only while it is focused. Writing one to a field the user has not selected
    /// does nothing — TMP reports zero — so this binder belongs next to whatever gives the field focus.
    /// </remarks>
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
