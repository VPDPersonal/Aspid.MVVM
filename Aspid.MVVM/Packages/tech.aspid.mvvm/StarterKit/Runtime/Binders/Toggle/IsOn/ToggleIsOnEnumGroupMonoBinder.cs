using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Toggle.isOn"/> on each element.
    /// </summary>
    /// <remarks>
    /// <see cref="NotifyMode"/> sets whether writes raise <see cref="Toggle.onValueChanged"/>.
    /// </remarks>
    [AddBinderContextMenu(
        type: typeof(Toggle),
        serializePropertyNames: "m_IsOn",
        SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn EnumGroup")]
    public sealed class ToggleIsOnEnumGroupMonoBinder : EnumGroupMonoBinder<Toggle, bool>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        /// <inheritdoc/>
        protected override void SetValue(Toggle element, bool value)
        {
            if (_notifyMode.ShouldNotify(IsBound)) element.isOn = value;
            else element.SetIsOnWithoutNotify(value);
        }
    }
}
