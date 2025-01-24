using UnityEngine;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Alpha")]
    public partial class CanvasGroupAlphaMonoBinder : ComponentMonoBinder<CanvasGroup>, INumberBinder
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
            CachedComponent.alpha = Mathf.Clamp01(value);
        }
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
    }
}