using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Dropdown.alphaFadeSpeed"/>
    /// on each element.
    /// </summary>
    /// <remarks>
    /// A negative value is raised to zero.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_AlphaFadeSpeed", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed EnumGroup")]
    public sealed class DropdownAlphaFadeSpeedEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Dropdown, float>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Dropdown element, float value) =>
            element.alphaFadeSpeed = this.NonNegative(value);
    }
}
