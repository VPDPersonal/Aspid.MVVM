using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{Image}"/> that switches the <see cref="Image.fillAmount"/> property
    /// between two values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Switcher")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "Switcher")]
    public sealed class ImageFillSwitcherMonoBinder : SwitcherFloatMonoBinder<Image>
    {
        /// <summary>
        /// Called when applying the selected value to the <see cref="Image.fillAmount"/> property.
        /// Clamps the value to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = Mathf.Clamp01(value);
    }
}