#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TMP_InputField, TMP_InputField.ContentType}"/> that sets
    /// <see cref="TMP_InputField.contentType"/> on each element in the group based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder - ContentType EnumGroup")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType", SubPath = "EnumGroup")]
    public sealed class InputFieldContentTypeEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// Sets <see cref="TMP_InputField.contentType"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField element, TMP_InputField.ContentType value) 
        {
            element.contentType = value;
            element.ForceLabelUpdate();
        }
    }
}
#endif