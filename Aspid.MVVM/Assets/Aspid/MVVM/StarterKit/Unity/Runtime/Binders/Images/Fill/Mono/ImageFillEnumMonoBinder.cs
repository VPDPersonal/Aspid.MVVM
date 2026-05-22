using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{Image}"/> that sets the <see cref="Image.fillAmount"/> property
    /// based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill Enum")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount", SubPath = "Enum")]
    public sealed class ImageFillEnumMonoBinder : EnumFloatMonoBinder<Image>
    {
        /// <summary>
        /// Called when the bound enum resolves to a value for the current element.
        /// Sets <see cref="Image.fillAmount"/> clamped to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = Mathf.Clamp01(value);
    }
}