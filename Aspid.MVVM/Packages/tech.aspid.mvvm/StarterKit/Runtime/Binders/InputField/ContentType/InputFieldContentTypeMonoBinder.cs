using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="TMP_InputField.contentType"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – ContentType")]
    public class InputFieldContentTypeMonoBinder : ComponentMonoBinder<TMP_InputField, TMP_InputField.ContentType>
    {
        /// <inheritdoc/>
        protected sealed override TMP_InputField.ContentType Property
        {
            get => CachedComponent.contentType;
            set
            {
                CachedComponent.contentType = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}
