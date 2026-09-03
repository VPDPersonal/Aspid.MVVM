using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_InputField.contentType"/>
    /// on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – ContentType EnumGroup")]
    public sealed class InputFieldContentTypeEnumGroupMonoBinder
        : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField element, TMP_InputField.ContentType value)
        {
            element.contentType = value;
            element.ForceLabelUpdate();
        }
    }
}
