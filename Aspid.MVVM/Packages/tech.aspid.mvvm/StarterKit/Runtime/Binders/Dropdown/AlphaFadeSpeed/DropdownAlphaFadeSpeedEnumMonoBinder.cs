using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Dropdown.alphaFadeSpeed"/>.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_AlphaFadeSpeed", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed Enum")]
    public sealed class DropdownAlphaFadeSpeedEnumMonoBinder : EnumMonoBinder<TMP_Dropdown, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            CachedComponent.alphaFadeSpeed = this.NonNegative(value);
    }
}
