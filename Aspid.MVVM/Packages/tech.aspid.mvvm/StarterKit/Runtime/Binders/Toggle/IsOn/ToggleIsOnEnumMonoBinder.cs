using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Toggle.isOn"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="NotifyMode"/> sets whether writes raise <see cref="Toggle.onValueChanged"/>.
    /// </remarks>
    [AddBinderContextMenu(
        type: typeof(Toggle),
        serializePropertyNames: "m_IsOn",
        SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn Enum")]
    public sealed class ToggleIsOnEnumMonoBinder : EnumMonoBinder<Toggle, bool>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(bool value)
        {
            if (_notifyMode.ShouldNotify(IsBound)) CachedComponent.isOn = value;
            else CachedComponent.SetIsOnWithoutNotify(value);
        }
    }
}
