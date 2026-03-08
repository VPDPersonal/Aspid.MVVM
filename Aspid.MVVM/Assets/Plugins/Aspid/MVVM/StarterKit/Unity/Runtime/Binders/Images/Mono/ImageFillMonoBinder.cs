using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="Image.fillAmount"/> property on an <see cref="Image"/> component
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder – Fill")]
    [AddBinderContextMenu(typeof(Image), serializePropertyNames: "m_FillAmount")]
    public class ImageFillMonoBinder : ComponentFloatMonoBinder<Image>
    {
        protected sealed override float Property
        {
            get => CachedComponent.fillAmount;
            set => CachedComponent.fillAmount = value;
        }
        
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}