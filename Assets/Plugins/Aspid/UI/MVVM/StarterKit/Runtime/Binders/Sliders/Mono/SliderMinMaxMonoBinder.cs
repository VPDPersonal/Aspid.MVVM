using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Min Max")]
    public partial class SliderMinMaxMonoBinder : ComponentMonoBinder<Slider>, IBinder<Vector2>, INumberBinder
    {
        [Header("Parameters")]
        [SerializeField] private SliderValueMode _mode = SliderValueMode.Range;
        
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Vector2, Vector2> _converter;
#else
        [SerializeReference] private IConverterVector2ToVector2 _converter;
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