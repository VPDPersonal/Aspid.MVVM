#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetIntBinder{TMP_InputField}"/> that binds <see cref="TMP_InputField.caretPosition"/>.
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
    [Serializable]
    public class InputFieldCaretPositionBinder : TargetIntBinder<TMP_InputField>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => Target.caretPosition;
            set => Target.caretPosition = Mathf.Clamp(value, 0, Target.text?.Length ?? 0);
        }

        /// <inheritdoc/>
        public InputFieldCaretPositionBinder(
            TMP_InputField target,
            IConverter<int, int>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
#endif
