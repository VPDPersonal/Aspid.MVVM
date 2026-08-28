#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent,TProperty}">ComponentMonoBinder&lt;TMP_InputField, TMP_InputField.ContentType&gt;</see> that gets and sets
    /// <see cref="TMP_InputField.contentType"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – ContentType")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_ContentType")]
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
#endif