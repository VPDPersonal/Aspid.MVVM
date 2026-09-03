using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Dropdown.value"/> on each element.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="TMP_Dropdown.SetValueWithoutNotify"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Value", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value EnumGroup")]
    public sealed class DropdownValueEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Dropdown, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Dropdown element, int value) =>
            element.SetValueWithoutNotify(value);
    }
}
