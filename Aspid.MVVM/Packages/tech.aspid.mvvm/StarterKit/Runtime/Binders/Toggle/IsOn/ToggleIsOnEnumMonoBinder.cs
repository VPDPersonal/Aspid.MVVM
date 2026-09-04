using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="Toggle.isOn"/>.
    /// </summary>
    /// <remarks>
    /// Writes use <see cref="Toggle.SetIsOnWithoutNotify"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn Enum")]
    public sealed class ToggleIsOnEnumMonoBinder : EnumMonoBinder<Toggle, bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            CachedComponent.SetIsOnWithoutNotify(value);
    }
}
