#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using System;
using TMPro;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetBinder{TMP_InputField, bool}"/> that binds <see cref="TMP_InputField.readOnly"/>.
    /// </summary>
    /// <remarks>
    /// Whether the field can be edited. Unlike clearing <c>interactable</c>, this keeps the field looking normal
    /// and its text selectable — the difference between "not now" and "not yours".
    /// </remarks>
    [Serializable]
    public class InputFieldReadOnlyBinder : TargetBinder<TMP_InputField, bool>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => Target.readOnly;
            set => Target.readOnly = value;
        }

        /// <inheritdoc/>
        public InputFieldReadOnlyBinder(
            TMP_InputField target,
            IConverter<bool, bool>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, converter, mode) { }
    }
}
#endif
