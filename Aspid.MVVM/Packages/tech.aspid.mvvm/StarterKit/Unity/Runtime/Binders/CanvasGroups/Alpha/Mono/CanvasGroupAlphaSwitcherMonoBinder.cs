using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{CanvasGroup}"/> that switches the <see cref="CanvasGroup.alpha"/>
    /// property between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha Switcher")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha", SubPath = "Switcher")]
    public sealed class CanvasGroupAlphaSwitcherMonoBinder : SwitcherFloatMonoBinder<CanvasGroup>
    {
        /// <summary>
        /// Called when applying the selected <see cref="float"/> value to the <see cref="CanvasGroup.alpha"/> property.
        /// Clamps the value to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.alpha = Mathf.Clamp01(value);
    }
}