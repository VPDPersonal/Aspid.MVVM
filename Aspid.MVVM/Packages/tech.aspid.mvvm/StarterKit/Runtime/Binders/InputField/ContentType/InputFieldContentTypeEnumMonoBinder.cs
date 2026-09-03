using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_InputField.contentType"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – ContentType Enum")]
    public sealed class InputFieldContentTypeEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField.ContentType value)
        {
            CachedComponent.contentType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
