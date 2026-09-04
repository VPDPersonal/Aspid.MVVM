using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="Toggle.isOn"/> on each element.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="Toggle.SetIsOnWithoutNotify"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn EnumGroup")]
    public sealed class ToggleIsOnEnumGroupMonoBinder : EnumGroupMonoBinder<Toggle, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(Toggle element, bool value) =>
            element.SetIsOnWithoutNotify(value);
    }
}
