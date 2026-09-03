using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_InputField.lineType"/>
    /// on each element.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – LineType EnumGroup")]
    public sealed class InputFieldLineTypeEnumGroupMonoBinder
        : EnumGroupMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField element, TMP_InputField.LineType value)
        {
            element.lineType = value;
            element.ForceLabelUpdate();
        }
    }
}
