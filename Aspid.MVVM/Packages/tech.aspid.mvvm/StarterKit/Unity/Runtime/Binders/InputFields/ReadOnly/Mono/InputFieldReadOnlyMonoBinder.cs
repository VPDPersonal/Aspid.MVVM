#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentBoolMonoBinder<TMP_InputField>"/> that binds <see cref="TMP_InputField.readOnly"/>.
    /// </summary>
    /// <remarks>
    /// Whether the field can be edited. Unlike clearing <c>interactable</c>, this keeps the field looking normal
    /// and its text selectable — the difference between "not now" and "not yours".
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ReadOnly")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – ReadOnly")]
    public class InputFieldReadOnlyMonoBinder : ComponentBoolMonoBinder<TMP_InputField>
    {
        /// <inheritdoc/>
        protected sealed override bool Property
        {
            get => CachedComponent.readOnly;
            set => CachedComponent.readOnly = value;
        }
    }
}
#endif
