using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.StarterKit.Converters.Number;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Fill")]
    public partial class ImageFillBinder : ImageBinderBase, IBinder<float>
    {
        [Header("Parameter")]
        [SerializeField] private bool _isConvert;
        [SerializeField] private FloatConverter _converter;
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedImage.fillAmount = ConvertValue(value);
        
        protected float ConvertValue(float value) =>
            _isConvert ? _converter.Convert(value) : value;
    }
}