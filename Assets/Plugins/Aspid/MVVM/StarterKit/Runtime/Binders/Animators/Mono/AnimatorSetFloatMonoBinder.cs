using UnityEngine;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("MVVM/Binders/Animator/Animator Binder - Set Float")]
    public partial class AnimatorSetFloatMonoBinder : AnimatorSetParameterMonoBinder<float>, INumberBinder
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<float, float> _converter;
#else
        private IConverterFloat _converter;
#endif
        
        protected sealed override void SetParameter(float value)
        {
            value = _converter?.Convert(value) ?? value;
            if (Mathf.Approximately(value, CachedComponent.GetFloat(ParameterName))) return;
            
            CachedComponent.SetFloat(ParameterName, value);
        }

        [BinderLog]
        public void SetValue(int value) =>
            base.SetValue(value);

        [BinderLog]
        public void SetValue(long value) =>
            base.SetValue(value);

        [BinderLog]
        public void SetValue(double value) =>
            base.SetValue((float)value);
    }
}