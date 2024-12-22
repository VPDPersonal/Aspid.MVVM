using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Slider/Slider Binder - MinMax Switcher")]
    public sealed class SliderMinMaxSwitcherMonoBinder : SwitcherMonoBinder<Slider, Vector2>
    {
        [SerializeField] private SliderValueMode _mode = SliderValueMode.Range;
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector2, Vector2> _converter;
#else
        private IConverterVector2 _converter;
#endif

        protected override void SetValue(Vector2 value)
        {
            value = _converter?.Convert(value) ?? value;
            CachedComponent.SetMinMax(value, _mode);
        }
    }
}