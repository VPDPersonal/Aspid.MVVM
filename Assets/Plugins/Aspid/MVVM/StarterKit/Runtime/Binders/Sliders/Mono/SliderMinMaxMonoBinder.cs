using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Slider/Slider Binder - MinMax")]
    public partial class SliderMinMaxMonoBinder : ComponentMonoBinder<Slider>, IBinder<Vector2>, INumberBinder
    {
        [Header("Parameter")]
        [SerializeField] private SliderValueMode _mode = SliderValueMode.Range;
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _converter;
#else
        private IConverterVector2 _converter;
#endif
        
        [BinderLog]
        public void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetMinMax(value, _mode);
        }
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}