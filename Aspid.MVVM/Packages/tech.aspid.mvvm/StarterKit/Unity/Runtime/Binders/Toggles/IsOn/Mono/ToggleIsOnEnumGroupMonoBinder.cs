using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{Toggle, Boolean}"/> that sets <see cref="Toggle.isOn"/> on each toggle in
    /// the group based on the bound enum ViewModel value — one enum member selects one toggle and clears the rest.
    /// </summary>
    /// <remarks>
    /// The toggle is set without notification, so driving it from the ViewModel is not read back as a click.
    /// A one-way variant needs no echo guard of its own — it never subscribes — but <c>Toggle.onValueChanged</c>
    /// fires for a programmatic write too, and any other binder listening on the same toggle would see it.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Toggle/Toggle Binder – IsOn EnumGroup")]
    [AddBinderContextMenu(typeof(Toggle), serializePropertyNames: "m_IsOn", SubPath = "EnumGroup")]
    public sealed class ToggleIsOnEnumGroupMonoBinder : EnumGroupMonoBinder<Toggle, bool>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the specified element.
        /// </summary>
        /// <param name="element">The component this entry of the group writes to.</param>
        /// <param name="value">The value the bound enum resolved to for this element.</param>
        protected override void SetValue(Toggle element, bool value) =>
            element.SetIsOnWithoutNotify(value);
    }
}
