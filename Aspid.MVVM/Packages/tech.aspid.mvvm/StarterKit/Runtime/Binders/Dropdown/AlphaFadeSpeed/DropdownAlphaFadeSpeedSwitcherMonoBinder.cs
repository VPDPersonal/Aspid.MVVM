using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_Dropdown.alphaFadeSpeed"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_AlphaFadeSpeed", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed Switcher")]
    public sealed class DropdownAlphaFadeSpeedSwitcherMonoBinder : SwitcherMonoBinder<TMP_Dropdown, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.alphaFadeSpeed = this.NonNegative(value);
    }
}
