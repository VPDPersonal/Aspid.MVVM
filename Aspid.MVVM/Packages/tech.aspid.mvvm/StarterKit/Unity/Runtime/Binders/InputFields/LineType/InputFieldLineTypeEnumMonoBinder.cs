using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{T1, T2}">EnumMonoBinder&lt;TMP_InputField, TMP_InputField.LineType&gt;</see> that sets
    /// <see cref="TMP_InputField.lineType"/> based on the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – LineType Enum")]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "Enum")]
    public sealed class InputFieldLineTypeEnumMonoBinder : EnumMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="TMP_InputField.lineType"/> to <paramref name="value"/> and forces a label update.
        /// </summary>
        protected override void SetValue(TMP_InputField.LineType value)
        {
            CachedComponent.lineType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}