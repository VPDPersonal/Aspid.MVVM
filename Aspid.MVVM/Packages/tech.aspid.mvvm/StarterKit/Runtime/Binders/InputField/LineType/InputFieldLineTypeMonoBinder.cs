using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that binds <see cref="TMP_InputField.lineType"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – LineType")]
    public class InputFieldLineTypeMonoBinder : ComponentMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        protected sealed override TMP_InputField.LineType Property
        {
            get => CachedComponent.lineType;
            set
            {
                CachedComponent.lineType = value;
                CachedComponent.ForceLabelUpdate();
            }
        }
    }
}
