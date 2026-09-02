using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{Toggle, Boolean}"/> that sets <see cref="Toggle.isOn"/>
    /// based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The toggle is set without notification, so driving it from the ViewModel is not read back as a click.
    /// A one-way variant needs no echo guard of its own — it never subscribes — but <c>Toggle.onValueChanged</c>
    /// fires for a programmatic write too, and any other binder listening on the same toggle would see it.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn Enum")]
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn", SubPath = "Enum")]
    public sealed class ToggleIsOnEnumMonoBinder : EnumMonoBinder<Toggle, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(bool value) =>
            CachedComponent.SetIsOnWithoutNotify(value);
    }
}
