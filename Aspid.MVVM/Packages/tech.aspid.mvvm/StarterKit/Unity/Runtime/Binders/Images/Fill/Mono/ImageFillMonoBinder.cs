using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Image}"/> that sets the <see cref="Image.fillAmount"/> property.
    /// </summary>
    /// <remarks>The bound value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.</remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount")]
    public class ImageFillMonoBinder : ComponentFloatMonoBinder<Image>
    {
        protected sealed override float Property
        {
            get => CachedComponent.fillAmount;
            set => CachedComponent.fillAmount = value;
        }
        
        /// <summary>
        /// Clamps <paramref name="value"/> to 0..1 before it reaches <see cref="Image.fillAmount"/>.
        /// </summary>
        /// <remarks>Override calls must invoke the base implementation to preserve the clamping.</remarks>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            this.SafeClamp01(base.GetConvertedValue(value));
    }
}