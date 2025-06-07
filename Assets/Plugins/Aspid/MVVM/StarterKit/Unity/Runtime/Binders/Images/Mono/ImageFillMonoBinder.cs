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
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Image/Image Binder - Fill")]
    [AddPropertyContextMenu(typeof(Image), "m_FillAmount")]
    [AddComponentContextMenu(typeof(Image),"Add Image Binder/Image Binder - Fill")]
    public partial class ImageFillMonoBinder : ComponentMonoBinder<Image>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);
        
        [BinderLog]
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.fillAmount = Mathf.Clamp01(value);
        }

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}