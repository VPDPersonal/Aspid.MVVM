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
        /// Sets <see cref="Image.fillAmount"/> to <paramref name="value"/>, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            CachedComponent.fillAmount = BinderMath.SafeClamp01(value);
    }
}