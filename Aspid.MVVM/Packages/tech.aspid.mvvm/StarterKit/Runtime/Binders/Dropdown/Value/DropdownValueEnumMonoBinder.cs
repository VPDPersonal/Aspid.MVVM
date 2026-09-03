using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="TMP_Dropdown.SetValueWithoutNotify"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Value", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value Enum")]
    public sealed class DropdownValueEnumMonoBinder : EnumMonoBinder<TMP_Dropdown, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.SetValueWithoutNotify(value);
    }
}
