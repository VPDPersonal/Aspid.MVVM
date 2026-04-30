using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{CanvasGroup}"/> that sets the <see cref="CanvasGroup.alpha"/>
    /// property to a value resolved from the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha Enum")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "Enum")]
    public sealed class CanvasGroupAlphaEnumMonoBinder : EnumFloatMonoBinder<CanvasGroup>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value.
        /// Sets <see cref="CanvasGroup.alpha"/> clamped to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.alpha = Mathf.Clamp01(value);
    }
}