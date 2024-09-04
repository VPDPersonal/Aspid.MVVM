using UnityEngine;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.CanvasGroups
{
    [AddComponentMenu("UI/Binders/Canvas Group/Canvas Group Binder - Alpha")]
    public partial class CanvasGroupAlphaBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>, IBinder<float>
    {
        [field: Header("Converter")]
        [field: SerializeReference]
        protected IConverterFloatToFloat Converter { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value) =>
            SetValue(value ? 1 : 0);
        
        [BinderLog]
        public void SetValue(float value)
        {
            value = Converter?.Convert(value) ?? value;
            CachedComponent.alpha = Mathf.Clamp(value, 0, 1);
        }
    }
}