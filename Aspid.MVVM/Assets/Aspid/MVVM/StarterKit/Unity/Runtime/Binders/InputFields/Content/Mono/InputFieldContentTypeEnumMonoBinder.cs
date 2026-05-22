#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TMP_InputField, TMP_InputField.ContentType}"/> that sets
    /// <see cref="TMP_InputField.contentType"/> based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - ContentType Enum")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType", SubPath = "Enum")]
    public sealed class InputFieldContentTypeEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="TMP_InputField.contentType"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.ContentType value)
        {
            CachedComponent.contentType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
#endif