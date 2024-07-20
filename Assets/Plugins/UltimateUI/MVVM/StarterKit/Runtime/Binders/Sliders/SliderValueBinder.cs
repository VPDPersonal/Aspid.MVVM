using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.StarterKit.Converters.Number;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Value")]
    public partial class SliderValueBinder : SliderBinderBase, IBinderNumber
    {
        [Header("Parameter")]
        [SerializeField] private bool _isConvert;
        [SerializeField] private FloatConverter _converter;
        
        [BinderLog]
        public void SetValue(int value) =>
            CachedSlider.value = ConvertValue(value);
        
        [BinderLog]
        public void SetValue(long value) =>
            CachedSlider.value = ConvertValue(value);
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedSlider.value = ConvertValue(value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        protected float ConvertValue(float value) =>
            _isConvert ? _converter.Convert(value) : value;
    }
}