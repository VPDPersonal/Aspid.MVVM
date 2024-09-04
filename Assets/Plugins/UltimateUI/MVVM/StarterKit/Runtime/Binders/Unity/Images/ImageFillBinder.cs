using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill")]
    public partial class ImageFillBinder : ComponentMonoBinder<Image>, IBinder<float>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
        protected IConverter<float, float> Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.fillAmount = Converter?.Convert(value) ?? value;
    }

}