using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{Image}"/> that sets the <see cref="Image.fillAmount"/> property.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current fill amount value
    /// is sent back to the ViewModel.
    /// The bound value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.
    /// </remarks>
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
        /// Called when converting the bound value before applying it to the <see cref="Image.fillAmount"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call the base implementation to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}