using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{CanvasGroup}"/> that binds the <see cref="CanvasGroup.alpha"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current alpha value
    /// is sent back to the ViewModel.
    /// The bound value is clamped to [0, 1] before being applied to <see cref="CanvasGroup.alpha"/>.
    /// </remarks>
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_Alpha")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – Alpha")]
    public class CanvasGroupAlphaMonoBinder : ComponentFloatMonoBinder<CanvasGroup>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.alpha;
            set => CachedComponent.alpha = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="CanvasGroup.alpha"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}