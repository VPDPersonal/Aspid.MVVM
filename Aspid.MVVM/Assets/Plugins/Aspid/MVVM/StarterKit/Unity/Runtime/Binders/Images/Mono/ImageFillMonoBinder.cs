using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
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