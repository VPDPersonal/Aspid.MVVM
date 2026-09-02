#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}"/> that binds <see cref="TMP_InputField.readOnly"/>.
    /// </summary>
    /// <remarks>
    /// Unlike clearing <c>interactable</c>, this keeps the field looking normal and its text selectable.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ReadOnly")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – ReadOnly")]
    public class InputFieldReadOnlyMonoBinder : ComponentMonoBinder<TMP_InputField, bool>
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
