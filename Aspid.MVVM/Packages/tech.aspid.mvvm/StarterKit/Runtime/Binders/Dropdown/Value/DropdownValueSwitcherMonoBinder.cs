using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="TMP_Dropdown.SetValueWithoutNotify"/>.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Value", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value Switcher")]
    public sealed class DropdownValueSwitcherMonoBinder : SwitcherMonoBinder<TMP_Dropdown, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.SetValueWithoutNotify(value);
    }
}
