using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    [AddBinderContextMenu(
        type: typeof(TMP_Dropdown),
        serializePropertyNames: "m_Value", 
        SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value Enum")]
    public sealed class DropdownValueEnumMonoBinder : EnumMonoBinder<TMP_Dropdown, int>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(int value)
        {
            if (_notifyMode.ShouldNotify(IsBound)) CachedComponent.value = value;
            else CachedComponent.SetValueWithoutNotify(value);
        }
    }
}
