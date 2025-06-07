using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(Image), "m_FillAmount")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder - Fill Switcher")]
    [AddComponentContextMenu(typeof(Image),"Add Image Binder/Image Binder - Fill Switcher")]
    public sealed class ImageFillSwitcherMonoBinder : SwitcherMonoBinder<Image, float>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        protected override void SetValue(float value) 
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.fillAmount = Mathf.Clamp01(value);
        }
    }
}