using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_InputField.lineType"/>.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_InputField), serializePropertyNames: "m_LineType", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/InputField/InputField Binder – LineType Switcher")]
    public sealed class InputFieldLineTypeSwitcherMonoBinder
        : SwitcherMonoBinder<TMP_InputField, TMP_InputField.LineType>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_InputField.LineType value)
        {
            CachedComponent.lineType = value;
            CachedComponent.ForceLabelUpdate();
        }
    }
}
