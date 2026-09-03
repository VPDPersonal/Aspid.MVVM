using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_InputField.lineType"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – LineType Enum")]
    public sealed class InputFieldLineTypeEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField.LineType value)
        {
            CachedComponent.lineType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
