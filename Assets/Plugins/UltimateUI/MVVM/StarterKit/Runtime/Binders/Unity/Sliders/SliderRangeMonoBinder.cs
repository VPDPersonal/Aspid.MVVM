using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Range")]
    public partial class SliderRangeMonoBinder : ComponentMonoBinder<UnityEngine.UI.Slider>, IBinder<Vector2>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
        protected IConverterVector2ToVector2 Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(Vector2 value)
        {
            value = Converter?.Convert(value) ?? value;
            
            CachedComponent.minValue = value.x;
            CachedComponent.maxValue = value.y;
        }
    }
}