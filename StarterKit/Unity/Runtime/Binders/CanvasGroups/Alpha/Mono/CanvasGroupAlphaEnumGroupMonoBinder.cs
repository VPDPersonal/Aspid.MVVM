using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.alpha"/>
    /// property on each <see cref="CanvasGroup"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha EnumGroup")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "EnumGroup")]
    public sealed class CanvasGroupAlphaEnumGroupMonoBinder : EnumGroupFloatMonoBinder<CanvasGroup>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="CanvasGroup.alpha"/> on <paramref name="element"/> clamped to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(CanvasGroup element, float value) =>
            element.alpha = Mathf.Clamp01(value);
    }
}