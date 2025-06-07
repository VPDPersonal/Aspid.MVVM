using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddPropertyContextMenu(typeof(CanvasGroup), "m_Alpha")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - Alpha")]
    [AddComponentContextMenu(typeof(CanvasGroup),"Add CanvasGroup Binder/CanvasGroup Binder - Alpha")]
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